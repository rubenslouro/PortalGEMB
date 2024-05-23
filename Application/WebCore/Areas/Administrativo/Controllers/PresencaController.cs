using ApplicationServices;
using ApplicationServices.MessageErrors;
using Domain.DomainServicesInterfaces;
using Domain.Dtos.Assistido.Input;
using Domain.Dtos.Disciplina.Input;
using Domain.Dtos.Presenca.Input;
using Domain.Dtos.Turma.Input;
using Domain.Dtos.TurmaAluno.Input;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCore.Areas.AreaManager;
using WebCore.DTO.SimpleError;
using WebCore.Services.LoginAuthentication;
using WebCore.Services.PopUp;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao]
[Area("Administrativo")]
public class PresencaController : Controller
{
    private readonly ILoginAuthenticationService _loginService;
    private readonly IAssistidoService _assistidoService;
    private readonly ITurmaService _turmaService;
    private readonly ITurmaAlunoService _turmaalunoService;

    /// <inheritdoc />
    public PresencaController(
        ILoginAuthenticationService loginService,
        IAssistidoService assistidoService,
        ITurmaService turmaService,
        ITurmaAlunoService turmaalunoService)
    {
        _loginService = loginService;
        _assistidoService = assistidoService;
        _turmaService = turmaService;
        _turmaalunoService = turmaalunoService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            ViewData["Turma"] = new SelectList(await _turmaService.ListarAsync(), "Turm_ID_Turma", "Turm_TX_Descricao");

            List<Assistido_Presenca> assistido_Presenca = new List<Assistido_Presenca>();

            return View(new PresencaAluno
            {
                IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync(),
                ListaAssistido = assistido_Presenca
            }) ;
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal", new { Area = "Administrativo" }) ?? string.Empty);
        }

        return View(new List<PresencaAluno>());
    }

    /// <summary>
    /// Cria um novo cadastro de disciplina
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Adicionar()
    {
        try
        {
            ViewData["Turma"] = new SelectList(await _turmaService.ListarAsync(), "Turm_ID_Turma", "Turm_TX_Descricao");

            return View(new PresencaAluno
            {
                IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync()
            });
            //return View(new PresencaAdicionarInModel
            //{
            //    IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync()
            //});
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal", new { Area = "Administrativo" }) ?? string.Empty);
        }

        return View(new PresencaAluno());
        //return View(new PresencaAdicionarInModel());
    }


    /// <summary>
    /// Carrega a tela que cria um novo cadastro de atendimento
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> CarregarListaAluno(int codTurma)
    {
        try
        {
            PresencaAluno entrada = new PresencaAluno();
            var ListaPresenca = new List<Assistido_Presenca>();

            var retorno = await _turmaalunoService.RetornarListaAlunosTurmaAsync(codTurma);

            for (int i = 0; i < retorno.Count; i++)
            {
                var retassistido = await _assistidoService.RetornarAsync(new AssistidoRetornarInModel { Assi_ID_Assistido = retorno[i].TuAl_ID_Assistido });

                var item = new Assistido_Presenca
                {
                    Assi_ID_Assistido = retorno[i].TuAl_ID_Assistido,
                    Assi_NM_Nome = retassistido.Assi_NM_Nome
                };

                ListaPresenca.Add(item);
            }

            entrada.PrAl_ID_Turma = codTurma;
            entrada.ListaAssistido = ListaPresenca;

            return Json(new PresencaAluno());
            //return Json(new { AssistidoNome = "Rubens Louro Vieira" });
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }
    }

}
