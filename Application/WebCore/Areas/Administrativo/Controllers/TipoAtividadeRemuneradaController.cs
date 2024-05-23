using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WebCore.Areas.AreaManager;
using WebCore.Services.PopUp;
using Domain.DomainServicesInterfaces;
using WebCore.Services.LoginAuthentication;
using Domain.Dtos.TipoAtividadeRemunerada.Input;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao]
[Area("Administrativo")]
public class TipoAtividadeRemuneradaController : Controller
{
    private readonly ILoginAuthenticationService _loginService;
    private readonly ITipoAtividadeRemuneradaService _tipoAtividadeRemuneradaService;

    /// <inheritdoc />
    public TipoAtividadeRemuneradaController(
        ILoginAuthenticationService loginService,
        ITipoAtividadeRemuneradaService tipoAtividadeRemuneradaService)
    {
        _loginService = loginService;
        _tipoAtividadeRemuneradaService = tipoAtividadeRemuneradaService;
    }

    /// <summary>
    /// Carrega a tela que cria um novo cadastro de atendimento
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Editar(int codTipoAtividadeRemunerada)
    {
        try
        {
            var retorno = await _tipoAtividadeRemuneradaService.RetornarAsync(new TipoAtividadeRemuneradaRetornarInModel { TpAR_ID_TipoAtividadeRemunerada = codTipoAtividadeRemunerada });

            var model = new TipoAtividadeRemuneradaEditarInModel
            {
                TpAR_ID_TipoAtividadeRemunerada = retorno.TpAR_ID_TipoAtividadeRemunerada,
                TpAR_NM_Descricao = retorno.TpAR_NM_Descricao
            };

            return View(model);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("index", "TipoEnunciado", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new TipoAtividadeRemuneradaEditarInModel());
        }
    }

    /// <summary>
    /// Cria um novo cadastro de atendimento
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Editar(TipoAtividadeRemuneradaEditarInModel model)
    {
        try
        {
            //model.CodUsuarioAlteracao = await _loginAuthenticationService.CodUsuarioLogadoAsync();

            if (ModelState.IsValid)
            {
                var retorno = await _tipoAtividadeRemuneradaService.EditarAsync(model);

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

