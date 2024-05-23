using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WebCore.Areas.AreaManager;
using WebCore.Services.PopUp;
using Domain.DomainServicesInterfaces;
using WebCore.Services.LoginAuthentication;
using Domain.Dtos.TipoEscolaridade.Input;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao]
[Area("Administrativo")]
public class TipoEscolaridadeController : Controller
{
    private readonly ILoginAuthenticationService _loginService;
    private readonly ITipoEscolaridadeService _tipoEscolaridadeService;

    /// <inheritdoc />
    public TipoEscolaridadeController(
        ILoginAuthenticationService loginService,
        ITipoEscolaridadeService tipoEscolaridadeService)
    {
        _loginService = loginService;
        _tipoEscolaridadeService = tipoEscolaridadeService;
    }

    /// <summary>
    /// Carrega a tela que cria um novo cadastro de atendimento
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Editar(int codTipoEscolaridade)
    {
        try
        {
            var retorno = await _tipoEscolaridadeService.RetornarAsync(new TipoEscolaridadeRetornarInModel { TpEs_ID_TipoEscolaridade = codTipoEscolaridade });

            var model = new TipoEscolaridadeEditarInModel
            {
                TpEs_ID_TipoEscolaridade = retorno.TpEs_ID_TipoEscolaridade,
                TpEs_NM_Descricao = retorno.TpEs_NM_Descricao
            };

            return View(model);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("index", "TipoEnunciado", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new TipoEscolaridadeEditarInModel());
        }
    }

    /// <summary>
    /// Cria um novo cadastro de atendimento
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Editar(TipoEscolaridadeEditarInModel model)
    {
        try
        {
            //model.CodUsuarioAlteracao = await _loginAuthenticationService.CodUsuarioLogadoAsync();

            if (ModelState.IsValid)
            {
                var retorno = await _tipoEscolaridadeService.EditarAsync(model);

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

