using Domain.DomainServicesInterfaces;
using Domain.Dtos.Disciplina.Input;
using Domain.Dtos.Turma.Input;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCore.Areas.AreaManager;
using WebCore.Services.LoginAuthentication;
using WebCore.Services.PopUp;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao]
[Area("Administrativo")]
public class TurmaController : Controller
{
    private readonly ILoginAuthenticationService _loginService;
    private readonly IDisciplinaService _disciplinaService;
    private readonly ITurmaService _turmaService;

    /// <inheritdoc />
    public TurmaController(
        ILoginAuthenticationService loginService,
        IDisciplinaService disciplinaService,
        ITurmaService turmaService)
    {
        _loginService = loginService;
        _disciplinaService = disciplinaService;
        _turmaService = turmaService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            TurmaDisciplina listaTurmaDisciplina = new TurmaDisciplina();
            List<TurmaIndex> listaTurmas = new List<TurmaIndex>();

            var retturma = await _turmaService.ListarAsync();

            for (int i = 0; i < retturma.Count; i++)
            {
                var disciplina = await _disciplinaService.RetornarAsync(new DisciplinaRetornarInModel { Disc_ID_Disciplina = retturma[i].Turm_ID_Disciplina });

                TurmaIndex turma = new TurmaIndex();
                turma.Turm_ID_Turma = retturma[i].Turm_ID_Turma;
                turma.Turm_ID_Disciplina = retturma[i].Turm_ID_Disciplina;
                turma.Turm_NM_Disciplina = disciplina.Disc_NM_Nome;
                turma.Turm_TX_Descricao = retturma[i].Turm_TX_Descricao;
                turma.Turm_DT_Inicio = retturma[i].Turm_DT_Inicio.ToString("dd/MM/yyyy");
                turma.Turm_DT_Final = retturma[i].Turm_DT_Final.ToString("dd/MM/yyyy");
                turma.Turm_CD_PeriodoLetivo = retturma[i].Turm_CD_PeriodoLetivo;
                turma.Turm_NR_AnoLetivo = retturma[i].Turm_NR_AnoLetivo;
                turma.Turm_TX_Observacao = retturma[i].Turm_TX_Observacao;

                listaTurmas.Add(turma);
            }

            listaTurmaDisciplina.Turma = listaTurmas;

            var retdisciplina = await _disciplinaService.ListarAsync();
            listaTurmaDisciplina.Disciplina = retdisciplina;

            return View(listaTurmaDisciplina);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal", new { Area = "Administrativo" }) ?? string.Empty);
        }

        return View(new List<TurmaDisciplina>());
    }

    /// <summary>
    /// Carrega a tela que cria um novo cadastro de turmas
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Adicionar(int codDisciplina)
    {
        try
        {
            ViewData["Disciplina"] = new SelectList(await _disciplinaService.ListarAsync(), "Disc_ID_Disciplina", "Disc_NM_Nome");

            return View(new TurmaAdicionarInModel
            {
                IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync()
            });
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal", new { Area = "Administrativo" }) ?? string.Empty);
        }

        return View(new TurmaAdicionarInModel());
    }

    /// <summary>
    /// Cria um novo cadastro de turma
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Adicionar(TurmaAdicionarInModel model)
    {
        try
        {
            ViewData["Disciplina"] = new SelectList(await _disciplinaService.ListarAsync(), "Disc_ID_Disciplina", "Disc_NM_Nome");

            var disciplina = await _disciplinaService.RetornarAsync(new DisciplinaRetornarInModel { Disc_ID_Disciplina = model.Turm_ID_Disciplina });
            if (disciplina != null && disciplina.Turmas.Count >= 0)
                model.Turm_NR_Turma = disciplina.Turmas.Count + 1;

            model.IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync();

            //model.ValidateModel();

            if (ModelState.IsValid)
            {
                var retorno = await _turmaService.AdicionarAsync(model);

                const string msg = "Registro cadastrado com sucesso!";
                this.AlertaMsgRedirect(msg, Url.Action("Visualizar", "Turma", new { codTurma = retorno.Turm_ID_Turma }) ?? string.Empty);

                return View(model);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        return View(model);
    }

    /// <summary>
    /// Carrega a tela que cria um novo cadastro de turma
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Editar(int codTurma)
    {
        try
        {
            ViewData["Disciplina"] = new SelectList(await _disciplinaService.ListarAsync(), "Disc_ID_Disciplina", "Disc_NM_Nome");

            var retorno = await _turmaService.RetornarAsync(new TurmaRetornarInModel { Turm_ID_Turma = codTurma });

            var model = new TurmaEditarInModel(retorno)
            {
                IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync()
            };

            return View(model);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("index", "Turma", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new TurmaEditarInModel());
        }
    }

    /// <summary>
    /// Cria um novo cadastro de turma
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Editar(TurmaEditarInModel model)
    {
        try
        {
            ViewData["Disciplina"] = new SelectList(await _disciplinaService.ListarAsync(), "Disc_ID_Disciplina", "Disc_NM_Nome");

            if (ModelState.IsValid)
            {
                var retorno = await _turmaService.EditarAsync(model);

                const string msg = "Registro alterado com sucesso!";
                this.AlertaMsgRedirect(msg, Url.Action("Index", "Turma", new { Area = "Administrativo" }) ?? string.Empty);

                return View(model);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }
        return View(model);
    }

    /// <summary>
    /// Visualizar Turma
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Visualizar(int codTurma)
    {
        try
        {
            var retorno = await _turmaService.RetornarAsync(new TurmaRetornarInModel { Turm_ID_Turma = codTurma });

            var retdisciplina = await _disciplinaService.RetornarAsync(new DisciplinaRetornarInModel { Disc_ID_Disciplina = retorno.Turm_ID_Disciplina });

            TurmaIndex turma = new TurmaIndex();
            turma.Turm_ID_Turma = retorno.Turm_ID_Turma;
            turma.Turm_ID_Disciplina = retorno.Turm_ID_Disciplina;
            turma.Turm_NM_Disciplina = retdisciplina.Disc_NM_Nome;
            turma.Turm_TX_Descricao = retorno.Turm_TX_Descricao;
            turma.Turm_DT_Inicio = retorno.Turm_DT_Inicio.ToString("dd/MM/yyyy");
            turma.Turm_DT_Final = retorno.Turm_DT_Final.ToString("dd/MM/yyyy");
            turma.Turm_CD_PeriodoLetivo = retorno.Turm_CD_PeriodoLetivo;
            turma.Turm_NR_AnoLetivo = retorno.Turm_NR_AnoLetivo;
            turma.Turm_TX_Observacao = retorno.Turm_TX_Observacao;

            return View(turma);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Turma", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new Turma());
        }
    }
}

