using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WebCore.Areas.AreaManager;
using WebCore.Services.PopUp;
using Domain.DomainServicesInterfaces;
using WebCore.Services.LoginAuthentication;
using Domain.Dtos.TipoAtendimento.Input;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao]
[Area("Administrativo")]
public class TipoAtendimentoController : Controller
{
    private readonly ILoginAuthenticationService _loginService;
    private readonly ITipoAtendimentoService _tipoatendimentoService;

    /// <inheritdoc />
    public TipoAtendimentoController(
        ILoginAuthenticationService loginService,
        ITipoAtendimentoService tipoatendimentoService)
    {
        _loginService = loginService;
        _tipoatendimentoService = tipoatendimentoService;
    }

    /// <summary>
    /// Carrega a tela que cria um novo cadastro de atendimento
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Editar(int codTipoAtendimento)
    {
        try
        {
            var retorno = await _tipoatendimentoService.RetornarAsync(new TipoAtendimentoRetornarInModel { TpAt_ID_TipoAtendimento = codTipoAtendimento });

            var model = new TipoAtendimentoEditarInModel
            {
                TpAt_ID_TipoAtendimento = retorno.TpAt_ID_TipoAtendimento,
                TpAt_NM_Descricao = retorno.TpAt_NM_Descricao
            };

            return View(model);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("index", "TipoEnunciado", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new TipoAtendimentoEditarInModel());
        }
    }

    /// <summary>
    /// Cria um novo cadastro de atendimento
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Editar(TipoAtendimentoEditarInModel model)
    {
        try
        {
            //model.CodUsuarioAlteracao = await _loginAuthenticationService.CodUsuarioLogadoAsync();

            if (ModelState.IsValid)
            {
                var retorno = await _tipoatendimentoService.EditarAsync(model);

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

