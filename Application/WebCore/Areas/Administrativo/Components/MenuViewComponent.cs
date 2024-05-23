using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.DomainServicesInterfaces;
using Domain.Dtos.Permissao.Input;
using Domain.Enums;
using WebCore.Services.LoginAuthentication;
using UtilService.Util;

namespace WebCore.Areas.Administrativo.Components;

/// <inheritdoc />
public class MenuViewComponent : ViewComponent
{
    private readonly IPermissaoService _permissaoCore;
    private readonly ILoginAuthenticationService _loginAuthenticationService;

    /// <inheritdoc />
    public MenuViewComponent(
        IPermissaoService permissaoCore,
        ILoginAuthenticationService loginAuthenticationService
    )
    {
        _permissaoCore = permissaoCore;
        _loginAuthenticationService = loginAuthenticationService;
    }

    /// <summary>
    /// Invoke inicial do componente
    /// </summary>
    /// <returns></returns>
    public async Task<IViewComponentResult> InvokeAsync()
    {
        await ConfiguraPermissoesMenuAsync();
        return View();
    }

    private async Task ConfiguraPermissoesMenuAsync()
    {
        ViewBag.CadastroUsuario = await _permissaoCore.AvaliarNivelAsync(new PermissaoAvaliarNivelInModel
        {
            CodUsuario = await _loginAuthenticationService.CodUsuarioLogadoAsync(),
            CodRegraSistema = TipoRegraSistema.CadastroUsuario.GetIntValue()
        });

        ViewBag.CadastroTipoUsuario = await _permissaoCore.AvaliarNivelAsync(new PermissaoAvaliarNivelInModel
        {
            CodUsuario = await _loginAuthenticationService.CodUsuarioLogadoAsync(),
            CodRegraSistema = TipoRegraSistema.CadastroTipoUsuario.GetIntValue()
        });

        ViewBag.ConfiguracaoGeral = await _permissaoCore.AvaliarNivelAsync(new PermissaoAvaliarNivelInModel
        {
            CodUsuario = await _loginAuthenticationService.CodUsuarioLogadoAsync(),
            CodRegraSistema = TipoRegraSistema.ConfiguracaoGeral.GetIntValue()
        });
    }
}