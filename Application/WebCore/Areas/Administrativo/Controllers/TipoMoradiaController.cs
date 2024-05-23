using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WebCore.Areas.AreaManager;
using WebCore.Services.PopUp;
using Domain.DomainServicesInterfaces;
using WebCore.Services.LoginAuthentication;
using Domain.Dtos.TipoMoradia.Input;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao]
[Area("Administrativo")]
public class TipoMoradiaController : Controller
{
    private readonly ILoginAuthenticationService _loginService;
    private readonly ITipoMoradiaService _tipoMoradiaService;

    /// <inheritdoc />
    public TipoMoradiaController(
        ILoginAuthenticationService loginService,
        ITipoMoradiaService tipoMoradiaService)
    {
        _loginService = loginService;
        _tipoMoradiaService = tipoMoradiaService;
    }

    /// <summary>
    /// Carrega a tela que cria um novo cadastro de atendimento
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Editar(int codTipoMoradia)
    {
        try
        {
            var retorno = await _tipoMoradiaService.RetornarAsync(new TipoMoradiaRetornarInModel { TpMo_ID_TipoMoradia = codTipoMoradia });

            var model = new TipoMoradiaEditarInModel
            {
                TpMo_ID_TipoMoradia = retorno.TpMo_ID_TipoMoradia,
                TpMo_NM_Descricao = retorno.TpMo_NM_Descricao
            };

            return View(model);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("index", "TipoEnunciado", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new TipoMoradiaEditarInModel());
        }
    }

    /// <summary>
    /// Cria um novo cadastro de atendimento
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Editar(TipoMoradiaEditarInModel model)
    {
        try
        {
            //model.CodUsuarioAlteracao = await _loginAuthenticationService.CodUsuarioLogadoAsync();

            if (ModelState.IsValid)
            {
                var retorno = await _tipoMoradiaService.EditarAsync(model);

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

