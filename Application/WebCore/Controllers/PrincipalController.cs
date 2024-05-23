using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Domain.DomainServicesInterfaces;
using Domain.Dtos.Usuario.EsqueciSenha.Input;
using WebCore.DTO.LoginWeb.Input;
using WebCore.DTO.SimpleError;
using WebCore.Models;
using WebCore.Services.LoginAuthentication;
using WebCore.Services.PopUp;
using Domain.Dtos.ConfiguracaoGeral.Input;

namespace WebCore.Controllers;

/// <inheritdoc />
public class PrincipalController : Controller
{
    private readonly ILoginAuthenticationService _loginAuthenticationService;
    private readonly IUsuarioService _usuarioService;
    private readonly IDatabaseService _databaseService;
    private readonly IApplicationInstallService _applicationInstallService;

    /// <inheritdoc />
    public PrincipalController(
        ILoginAuthenticationService loginAuthenticationService,
        IUsuarioService usuarioService,
        IDatabaseService databaseService,
        IApplicationInstallService applicationInstallService)
    {
        _loginAuthenticationService = loginAuthenticationService;
        _usuarioService = usuarioService;
        _databaseService = databaseService;
        _applicationInstallService = applicationInstallService;
    }
    /// <summary>
    /// Método para futuro CSS customizado pelo cliente/usuário
    /// </summary>
    /// <returns></returns>
    public string Css()
    {
        var css = new CustomCss();

        var str = css.CssCreate();
        return str;
    }


    /// <summary>
    /// Data e hora do servidor
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public JsonResult DataHora()
    {
        try
        {

            var dataHora = DateTime.UtcNow.ToShortDateString() + " " + DateTime.UtcNow.ToShortTimeString();
            return Json(new { DataHora = dataHora });
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }
    }

    /// <summary>
    /// Tela de login MVC
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<IActionResult> LoginAdm(string email)
    {
        var model = new LoginInModel
        {
            Email = email
        };

        if (await _databaseService.BancoDadosEstaCriadoAsync() && await _databaseService.BancoDeDadosEstaConfiguradoAsync())
            return View(model);
        if (!await _databaseService.BancoDadosEstaCriadoAsync())
            return NotFound();
        if (!await _databaseService.BancoDeDadosEstaConfiguradoAsync())
            return NotFound();

        return View(model);
    }

    /// <summary>
    /// Método para login 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> LoginAdm(LoginInModel model)
    {
        try
        {
            if (!await _databaseService.BancoDadosEstaCriadoAsync())
                return NotFound();
            if (!await _databaseService.BancoDeDadosEstaConfiguradoAsync())
                return NotFound();

            if (ModelState.IsValid)
            {
                await _loginAuthenticationService.LoginAsync(model);
                return RedirectToAction("Index", "Principal", new { area = "Administrativo" });
            }

        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }
        return View(model);
    }

    /// <summary>
    /// Método para logout
    /// </summary>
    /// <returns></returns>
    public ActionResult LogOut()
    {
        return RedirectToAction("LoginAdm", "Principal");
    }

    /// <summary>
    /// Lembrança de senha
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> LembrarSenha(UsuarioEsqueciSenhaInModel model)
    {
        try
        {
            if (!await _databaseService.BancoDadosEstaCriadoAsync())
                return NotFound();
            if (!await _databaseService.BancoDeDadosEstaConfiguradoAsync())
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _usuarioService.EsqueciSenhaAsync(model);
                  
                }
                catch (Exception)
                {
                    //Mascara o erro propositalmente. Evitar informações sobre usuários.   
                }
                finally
                {
                    this.AlertaMsg("Foi enviado um e-mail com sua senha para o endereço de e-mail informado.");
                    ModelState.Clear();
                    model.Email = string.Empty;
                }
                return View(model);
            }
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("LembrarSenha", "Principal", new UsuarioEsqueciSenhaInModel()) ?? string.Empty);
        }

        return View(new UsuarioEsqueciSenhaInModel());
    }

    /// <summary>
    /// Lembrança de senha
    /// </summary>
    /// <returns></returns>
    public async Task<ActionResult> LembrarSenha()
    {
        if (!await _databaseService.BancoDadosEstaCriadoAsync())
            return NotFound();
        if (!await _databaseService.BancoDeDadosEstaConfiguradoAsync())
            return NotFound();

        return View(new UsuarioEsqueciSenhaInModel());
    }

    /// <summary>
    /// Utilizado durante a instalação do sistema para configurar e criar o banco de dados
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Instalar()
    {
        if (await _databaseService.BancoDadosEstaCriadoAsync() && await _databaseService.BancoDeDadosEstaConfiguradoAsync())
            return NotFound();
        if (!await _databaseService.BancoDadosEstaCriadoAsync())
            return View(new ConfiguracaoGeralConfigurarBancoDadosECriaInModel());
        if (!await _databaseService.BancoDeDadosEstaConfiguradoAsync())
            return NotFound();

        return NotFound();
    }

    /// <summary>
    /// Utilizado durante a instalação do sistema para configurar e criar o banco de dados
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Instalar(ConfiguracaoGeralConfigurarBancoDadosECriaInModel model)
    {
        try
        {
            if (await _databaseService.BancoDadosEstaCriadoAsync() && await _databaseService.BancoDeDadosEstaConfiguradoAsync())
                return NotFound();
            if (await _databaseService.BancoDadosEstaCriadoAsync())
                return NotFound();

            if (ModelState.IsValid)
            {
                await _databaseService.ConfigurarBancoDadosECriaAsync(model);
                this.AlertaMsgRedirect("Configuração e criação do banco de dados realizada! Agora você será direcionado para a tela de configuração do sistema.",
                    Url.Action("ConfigurarDados", "Principal", new ConfiguracaoGeralConfigurarBancoDadosECriaInModel()) ?? string.Empty);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        return View(model);
    }

    /// <summary>
    /// Utilizado durante a instalação do sistema para configurar os parâmetros inicias do sistema
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> ConfigurarDados()
    {
        if (await _databaseService.BancoDadosEstaCriadoAsync() && await _databaseService.BancoDeDadosEstaConfiguradoAsync())
            return NotFound();
        if (!await _databaseService.BancoDadosEstaCriadoAsync())
            return NotFound();
        if (!await _databaseService.BancoDeDadosEstaConfiguradoAsync())
            return View(new ConfiguracaoGeralInstalarConfiguracaoInModel());

        return NotFound();
    }

    /// <summary>
    /// Utilizado durante a instalação do sistema para configurar os parâmetros inicias do sistema
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> ConfigurarDados(ConfiguracaoGeralInstalarConfiguracaoInModel model)
    {
        try
        {
            if (await _databaseService.BancoDadosEstaCriadoAsync() && await _databaseService.BancoDeDadosEstaConfiguradoAsync())
                return NotFound();
            if (!await _databaseService.BancoDadosEstaCriadoAsync())
                return NotFound();

            if (ModelState.IsValid)
            {
                await _applicationInstallService.InstalarConfiguracaoAsync(model);
                this.AlertaMsgRedirect($"A configuração está pronta. Agora você será direcionado para a tela de login para que realize seu primeiro acesso. O usuário criado para administrar o sistema é {model.EmailUsuarioMaster}.",
                    Url.Action("LoginAdm", "Principal") ?? string.Empty);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        return View(model);
    }

}