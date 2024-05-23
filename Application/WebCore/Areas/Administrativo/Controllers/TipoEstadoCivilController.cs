using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WebCore.Areas.AreaManager;
using WebCore.Services.PopUp;
using Domain.DomainServicesInterfaces;
using WebCore.Services.LoginAuthentication;
using Domain.Dtos.TipoEstadoCivil.Input;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao]
[Area("Administrativo")]
public class TipoEstadoCivilController : Controller
{
    private readonly ILoginAuthenticationService _loginService;
    private readonly ITipoEstadoCivilService _tipoEstadoCivilService;

    /// <inheritdoc />
    public TipoEstadoCivilController(
        ILoginAuthenticationService loginService,
        ITipoEstadoCivilService tipoEstadoCivilService)
    {
        _loginService = loginService;
        _tipoEstadoCivilService = tipoEstadoCivilService;
    }

    /// <summary>
    /// Carrega a tela que cria um novo cadastro de atendimento
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Editar(int codTipoEstadoCivil)
    {
        try
        {
            var retorno = await _tipoEstadoCivilService.RetornarAsync(new TipoEstadoCivilRetornarInModel { TpEC_ID_TipoEstadoCivil = codTipoEstadoCivil });

            var model = new TipoEstadoCivilEditarInModel
            {
                TpEC_ID_TipoEstadoCivil = retorno.TpEC_ID_TipoEstadoCivil,
                TpEC_NM_Descricao = retorno.TpEC_NM_Descricao
            };

            return View(model);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("index", "TipoEnunciado", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new TipoEstadoCivilEditarInModel());
        }
    }

    /// <summary>
    /// Cria um novo cadastro de atendimento
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Editar(TipoEstadoCivilEditarInModel model)
    {
        try
        {
            //model.CodUsuarioAlteracao = await _loginAuthenticationService.CodUsuarioLogadoAsync();

            if (ModelState.IsValid)
            {
                var retorno = await _tipoEstadoCivilService.EditarAsync(model);

                const string msg = "Registro alterado com sucesso!";
                this.AlertaMsgRedirect(msg, Url.Action("Index", "TipoEnunciado", new { Area = "Administrativo" }) ?? string.Empty);

                return View(model);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        return View(model);
    }
}

