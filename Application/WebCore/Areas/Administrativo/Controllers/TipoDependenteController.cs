using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WebCore.Areas.AreaManager;
using WebCore.Services.PopUp;
using Domain.DomainServicesInterfaces;
using WebCore.Services.LoginAuthentication;
using Domain.Dtos.TipoDependente.Input;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao]
[Area("Administrativo")]
public class TipoDependenteController : Controller
{
    private readonly ILoginAuthenticationService _loginService;
    private readonly ITipoDependenteService _tipoDependenteService;

    /// <inheritdoc />
    public TipoDependenteController(
        ILoginAuthenticationService loginService,
        ITipoDependenteService tipoDependenteService)
    {
        _loginService = loginService;
        _tipoDependenteService = tipoDependenteService;
    }

    /// <summary>
    /// Carrega a tela que cria um novo cadastro de atendimento
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Editar(int codTipoDependente)
    {
        try
        {
            var retorno = await _tipoDependenteService.RetornarAsync(new TipoDependenteRetornarInModel { TpDe_ID_TipoDependente = codTipoDependente });

            var model = new TipoDependenteEditarInModel
            {
                TpDe_ID_TipoDependente = retorno.TpDe_ID_TipoDependente,
                TpDe_NM_Descricao = retorno.TpDe_NM_Descricao
            };

            return View(model);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("index", "TipoEnunciado", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new TipoDependenteEditarInModel());
        }
    }

    /// <summary>
    /// Cria um novo cadastro de atendimento
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Editar(TipoDependenteEditarInModel model)
    {
        try
        {
            //model.CodUsuarioAlteracao = await _loginAuthenticationService.CodUsuarioLogadoAsync();

            if (ModelState.IsValid)
            {
                var retorno = await _tipoDependenteService.EditarAsync(model);

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

