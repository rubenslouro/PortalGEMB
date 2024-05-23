using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Domain.DomainServicesInterfaces;
using Domain.Dtos.ConfiguracaoGeral.Input;
using Domain.Dtos.ConfiguracaoGeral.Output;
using Domain.Enums;
using WebCore.Areas.AreaManager;
using WebCore.Services.LoginAuthentication;
using WebCore.Services.PopUp;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao(TipoRegraSistema.ConfiguracaoGeral)]
[Area("Administrativo")]
public class ConfiguracaoGeralController : Controller
{
    private readonly IConfiguracaoGeralService _configuracaoGeralCore;
    private readonly ILoginAuthenticationService _loginAuthenticationService;

    /// <inheritdoc />
    public ConfiguracaoGeralController(
        IConfiguracaoGeralService configuracaoGeralCore,
        ILoginAuthenticationService loginAuthenticationService)
    {
        _configuracaoGeralCore = configuracaoGeralCore;
        _loginAuthenticationService = loginAuthenticationService;
    }

    /// <summary>
    /// Retorna dados detalhados sobre as configurações do sistema
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        try
        {
            var configuracaoGeralRetornaOutModel = await _configuracaoGeralCore.RetornaDetalhadoAsync(
                new ConfiguracaoGeralRetornaDetalhadoInModel 
                { 
                    CodUsuarioSolicitacao = await _loginAuthenticationService.CodUsuarioLogadoAsync()
                });
            return View(configuracaoGeralRetornaOutModel);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal", new { Area = "Administrativo" }) ?? string.Empty);       
        }
        return View(new ConfiguracaoGeralRetornaDetalhadoOutModel());
    }

    /// <summary>
    /// Utilizado apenas na aplicação MVC com a finalidade de criar um modelo para carregar a tela de configuração
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Editar()
    {
        try
        {
            var configuracaoGeralEditarOutModel = await _configuracaoGeralCore.RetornarAsync();
            var model = new ConfiguracaoGeralEditarConfiguracaoInModel(configuracaoGeralEditarOutModel, await _loginAuthenticationService.CodUsuarioLogadoAsync());
      
            return View(model);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal", new { Area = "Administrativo" }) ?? string.Empty);
        }
        return View(new ConfiguracaoGeralEditarConfiguracaoInModel());
    }

    /// <summary>
    /// Edita as configurações existentes no sistema
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Editar(ConfiguracaoGeralEditarConfiguracaoInModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                model.CodUsuarioAcao = await _loginAuthenticationService.CodUsuarioLogadoAsync();
                await _configuracaoGeralCore.EditarConfiguracaoAsync(model);
                this.AlertaMsgRedirect("Alterações realizadas com sucesso!", Url.Action("Index", "ConfiguracaoGeral", new { Area = "Administrativo" }) ?? string.Empty);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        return View(model);
    }
        
}