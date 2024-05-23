using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Domain.DomainServicesInterfaces;
using Domain.Dtos.Usuario.AlterarSenha.Input;
using Domain.Dtos.Usuario.Criar.Input;
using Domain.Dtos.Usuario.Criar.Output;
using UtilService.Util;
using WebCore.DTO.LoginWeb.Input;
using WebCore.DTO.LoginWeb.Output;
using Domain.Interfaces.Exception;

namespace WebCore.Services.LoginAuthentication;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class LoginAuthenticationService : ILoginAuthenticationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRegraSistemaService _regraSistemaService;
    private readonly ICriptografiaService _criptografiaService;
    private readonly IUsuarioService _usuarioService;
    private readonly IExceptions _exceptions;
    private readonly IConfiguration _configuration;
    private readonly IUsuarioCheckerService _usuarioCheckerCore;
        
    /// <summary>
    /// Contrutor
    /// </summary>
    /// <param name="regraSistemaService"></param>
    /// <param name="criptografiaService"></param>
    /// <param name="usuarioService"></param>
    /// <param name="exceptions"></param>
    /// <param name="configuration"></param>
    /// <param name="usuarioCheckerCore"></param>
    /// <param name="httpContextAccessor"></param>
    public LoginAuthenticationService(
        IRegraSistemaService regraSistemaService,
        ICriptografiaService criptografiaService,
        IUsuarioService usuarioService,
        IExceptions exceptions,
        IConfiguration configuration,
        IUsuarioCheckerService usuarioCheckerCore,
        IHttpContextAccessor httpContextAccessor)
    {
        _regraSistemaService = regraSistemaService;
        _criptografiaService = criptografiaService;
        _usuarioService = usuarioService;
        _exceptions = exceptions;
        _configuration = configuration;
        _usuarioCheckerCore = usuarioCheckerCore;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="NullReferenceException"></exception>
    public async Task<LoginOutModel> LoginAsync(LoginInModel model)
    {
        model.CheckIfModelIsValid();
        await _regraSistemaService.InstalarRegrasSistemaAsync();//Todo: tirar isso daqui e botar em um local lógico.
        model.Senha = _criptografiaService.EncryptString(model.Senha);

        var usuario = await _usuarioService.ReturnByEmailAsync(model.Email);

        if (usuario == null ||
            usuario.DataAfastamento.HasValue ||
            usuario.Senha != model.Senha)
            throw new Exception(_exceptions.NaoEPermitidoContinuarLoginInvalido);

        var timeInMinutesForTokenDuration = int.Parse(_configuration["LoginConfig:TimeInMinutesForTokenDuration"] ?? throw new NullReferenceException("LoginConfig:TimeInMinutesForTokenDuration"));

        var ret = new LoginOutModel(usuario, DateTime.UtcNow.AddMinutes(timeInMinutesForTokenDuration));

        var encryptLogin = _criptografiaService.EncryptString(JsonConvert.SerializeObject(ret));
        _httpContextAccessor.HttpContext?.Response.Cookies.Append("dataExpiracao", ret.DataExpiracaoSessao.ToString("dd/MM/yyyy HH:mm:ss"), new CookieOptions { Expires = new DateTimeOffset(ret.DataExpiracaoSessao) });
        _httpContextAccessor.HttpContext?.Response.Cookies.Append("secretKey", encryptLogin, new CookieOptions { Expires = new DateTimeOffset(ret.DataExpiracaoSessao) });
           
        return ret;
    }

    /// <inheritdoc />
    public async Task<bool> LogadoAsync()
    {
        return (await UsuarioLogadoAsync()) != null!;
    }

    /// <inheritdoc />
    public async Task<LoginOutModel> UsuarioLogadoAsync()
    {
        return await UsuarioLogadoByCookieAsync();
    }

    /// <inheritdoc />
    public async Task<int> CodUsuarioLogadoAsync()
    {
        return (await UsuarioLogadoByCookieAsync()).Codigo;
    }

    /// <inheritdoc />
    public void Logout()
    {
        DestruirCookies();
        DestruirSessions();
    }
    
    /// <inheritdoc />
    public async Task AlterarSenhaAsync(UsuarioAlterarSenhaInModel model)
    {
        model.CheckIfModelIsValid();
        model.SenhaNova = _criptografiaService.EncryptString(model.SenhaNova);
        model.SenhaAntiga = _criptografiaService.EncryptString(model.SenhaAntiga);
        model.SenhaNovaConfirmacao = _criptografiaService.EncryptString(model.SenhaNovaConfirmacao);

        await _usuarioService.AlterarSenhaAsync(model);
    }

    /// <inheritdoc />
    public async Task<UsuarioAdicionarOutModel> AdicionarAsync(UsuarioAdicionarInModel model) 
    {
        model.CheckIfModelIsValid();
        
        return await _usuarioService.AdicionarAsync(model);
    }
    #region Métodos privados

    private async Task<LoginOutModel> UsuarioLogadoByCookieAsync()
    {
        try
        {
            var stringJsonCriptedByCookie = _httpContextAccessor.HttpContext?.Request.Cookies["secretKey"];

            if (stringJsonCriptedByCookie.IsNullOrWhiteSpace())
            {
                Logout();
                return null!;
            }

            var stringJsonDecriptedByCookie = _criptografiaService.DecryptString(stringJsonCriptedByCookie);
            var loginByCookie = JsonConvert.DeserializeObject<LoginOutModel>(stringJsonDecriptedByCookie);

            if (loginByCookie == null)
            {
                Logout();
                return null!;
            }

            if (loginByCookie.DataExpiracaoSessao < DateTime.UtcNow)
            {
                Logout();
                return null!;
            }

            await _usuarioCheckerCore.CriticarUsuarioInativoAsync(loginByCookie.Codigo);

            loginByCookie.RefreshDataExpiracaoSessao(DateTime.UtcNow.AddMinutes(20));
            var encryptLogin = _criptografiaService.EncryptString(JsonConvert.SerializeObject(loginByCookie));

            var cookie = new CookieOptions
            {
                Expires = loginByCookie.DataExpiracaoSessao
            };

            _httpContextAccessor.HttpContext?.Response.Cookies.Append("dataExpiracao", loginByCookie.DataExpiracaoSessao.ToString("dd/MM/yyyy HH:mm:ss"), cookie);
            _httpContextAccessor.HttpContext?.Response.Cookies.Append("secretKey", encryptLogin, cookie);

            return loginByCookie;
        }
        catch (Exception)
        {
            Logout();
            return null!;
        }
    }

    private void DestruirSessions()
    {
        _httpContextAccessor.HttpContext?.Session.Clear();
    }
    private void DestruirCookies()
    {
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("dataExpiracao");
        _httpContextAccessor.HttpContext?.Response.Cookies.Delete("secretKey");
    }

    #endregion
}