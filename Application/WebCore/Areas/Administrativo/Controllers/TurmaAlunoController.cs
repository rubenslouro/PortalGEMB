using Domain.DomainServicesInterfaces;
using Domain.Dtos.Assistido.Input;
using Domain.Dtos.TurmaAluno.Input;
using Domain.Dtos.Turma.Input;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCore.Areas.AreaManager;
using WebCore.Services.LoginAuthentication;
using WebCore.Services.PopUp;
using ApplicationServices;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao]
[Area("Administrativo")]
public class TurmaAlunoController : Controller
{
    private readonly ILoginAuthenticationService _loginService;
    private readonly IAssistidoService _assistidoService;
    private readonly IAtendimentoService _atendimentoService;
    private readonly ITurmaService _turmaService;
    private readonly ITurmaAlunoService _turmaalunoService;

    /// <inheritdoc />
    public TurmaAlunoController(
        ILoginAuthenticationService loginService,
        IAssistidoService assistidoService,
        IAtendimentoService atendimentoService,
        ITurmaService turmaService,
        ITurmaAlunoService turmaalunoService)
    {
        _loginService = loginService;
        _assistidoService = assistidoService;
        _atendimentoService = atendimentoService;
        _turmaService = turmaService;
        _turmaalunoService = turmaalunoService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            List<TurmaAlunoIndex> listaTurmaAlunos = new List<TurmaAlunoIndex>();

            var retturmaaluno = await _turmaalunoService.ListarAsync();

            for (int i = 0; i < retturmaaluno.Count; i++)
            {
                var retassistido = await _assistidoService.RetornarAsync(new AssistidoRetornarInModel { Assi_ID_Assistido = retturmaaluno[i].TuAl_ID_Assistido });
                var retturma = await _turmaService.RetornarAsync(new TurmaRetornarInModel { Turm_ID_Turma = retturmaaluno[i].TuAl_ID_Turma });

                TurmaAlunoIndex turmaaluno = new TurmaAlunoIndex();
                turmaaluno.TuAl_ID_Turma = retturmaaluno[i].TuAl_ID_Turma;
                turmaaluno.TuAl_NM_Turma = retturma.Turm_TX_Descricao;
                turmaaluno.TuAl_ID_Assistido = retturmaaluno[i].TuAl_ID_Assistido;
                turmaaluno.TuAl_NM_Assistido = retassistido.Assi_NM_Nome;
                turmaaluno.TuAl_CD_PeriodoLetivo = retturma.Turm_CD_PeriodoLetivo;
                turmaaluno.TuAl_NR_AnoLetivo = retturma.Turm_NR_AnoLetivo;

                listaTurmaAlunos.Add(turmaaluno);
            }

            return View(listaTurmaAlunos);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal", new { Area = "Administrativo" }) ?? string.Empty);
        }

        return View(new List<TurmaAlunoIndex>());
    }

    /// <summary>
    /// Carrega a tela que cria um novo cadastro de turmas
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Adicionar()
    {
        try
        {
            CarregarSessaoEnum();

            ViewData["Assistido"] = new SelectList(await _assistidoService.ListarAsync(), "Assi_ID_Assistido", "Assi_NM_Nome");
            ViewData["Turma"] = new SelectList(await _turmaService.ListarAsync(), "Turm_ID_Turma", "Turm_TX_Descricao");

            var allMatriculados = await _assistidoService.ListarAsync(); //returns List<Genre>

            var checkBoxListItems = new List<CheckBoxList>();
            foreach (var matriculados in allMatriculados)
            {
                checkBoxListItems.Add(new CheckBoxList()
                {
                    Value = matriculados.Assi_ID_Assistido,
                    Text = matriculados.Assi_NM_Nome,
                    IsChecked = false
                });
            }

            return View(new TurmaAlunoAdicionarInModel
            {
                IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync(),
                ChechBoxList = checkBoxListItems
            });
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal", new { Area = "Administrativo" }) ?? string.Empty);
        }

        return View(new TurmaAlunoAdicionarInModel());
    }

    /// <summary>
    /// Cria um novo cadastro de turma
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Adicionar(TurmaAlunoAdicionarInModel model)
    {
        try
        {
            CarregarSessaoEnum();

            ViewData["Assistido"] = new SelectList(await _assistidoService.ListarAsync(), "Assi_ID_Assistido", "Assi_NM_Nome");
            ViewData["Turma"] = new SelectList(await _turmaService.ListarAsync(), "Turm_ID_Turma", "Turm_TX_Descricao");

            model.IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync();

            if (ModelState.IsValid)
            {
                var retorno = await _turmaalunoService.AdicionarAsync(model);

                const string msg = "Registro cadastrado com sucesso!";
                this.AlertaMsgRedirect(msg, Url.Action("Visualizar", "TurmaAluno", new { codAluno = retorno.TuAl_ID_Assistido }) ?? string.Empty);

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
    /// Deletar o relacionamento do aluno com a turma
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Deletar(int codAluno, int codTurma)
    {
        try
        {
            var retorno = await _turmaalunoService.RetornarAsync(new TurmaAlunoRetornarInModel { TuAl_ID_Assistido = codAluno });

            var model = new TurmaAlunoEditarInModel(retorno)
            {
                IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync()
            };

            return View(model);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("index", "TurmaAluno", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new TurmaAlunoEditarInModel());
        }
    }


    /// <summary>
    /// Carrega a tela que cria um novo cadastro de turma
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Editar(int codAluno, string periodoLetivo, int anoLetivo)
    {
        try
        {
            CarregarSessaoEnum();

            ViewData["Assistido"] = new SelectList(await _assistidoService.ListarAsync(), "Assi_ID_Assistido", "Assi_NM_Nome");
            ViewData["Turma"] = new SelectList(await _turmaService.ListarAsync(), "Turm_ID_Turma", "Turm_TX_Descricao");

            var retorno = await _turmaalunoService.RetornarAsync(new TurmaAlunoRetornarInModel { TuAl_ID_Assistido = codAluno });

            var model = new TurmaAlunoEditarInModel(retorno)
            {
                IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync()
            };

            return View(model);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("index", "TurmaAluno", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new TurmaAlunoEditarInModel());
        }
    }

    /// <summary>
    /// Cria um novo cadastro de TurmaAluno
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Editar(TurmaAlunoEditarInModel model)
    {
        try
        {
            CarregarSessaoEnum();

            ViewData["Assistido"] = new SelectList(await _assistidoService.ListarAsync(), "Assi_ID_Assistido", "Assi_NM_Nome");
            ViewData["Turma"] = new SelectList(await _turmaService.ListarAsync(), "Turm_ID_Turma", "Turm_TX_Descricao");

            if (ModelState.IsValid)
            {
                var retorno = await _turmaalunoService.EditarAsync(model);

                const string msg = "Registro alterado com sucesso!";
                this.AlertaMsgRedirect(msg, Url.Action("Index", "TurmaAluno", new { Area = "Administrativo" }) ?? string.Empty);

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
    /// Visualizar TurmaAluno
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Visualizar(int codAluno, string periodoLetivo, int anoLetivo)
    {
        try
        {
            var retorno = await _turmaalunoService.RetornarAsync(new TurmaAlunoRetornarInModel { TuAl_ID_Assistido = codAluno });

            var retassistido = await _assistidoService.RetornarAsync(new AssistidoRetornarInModel { Assi_ID_Assistido = retorno.TuAl_ID_Assistido });
            var retturma = await _turmaService.RetornarAsync(new TurmaRetornarInModel { Turm_ID_Turma = retorno.TuAl_ID_Turma });

            TurmaAlunoIndex turmaaluno = new TurmaAlunoIndex();
            turmaaluno.TuAl_ID_Turma = retorno.TuAl_ID_Turma;
            turmaaluno.TuAl_NM_Turma = retturma.Turm_TX_Descricao;
            turmaaluno.TuAl_ID_Assistido = retorno.TuAl_ID_Assistido;
            turmaaluno.TuAl_NM_Assistido = retassistido.Assi_NM_Nome;

            return View(turmaaluno);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Turma", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new Turma());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public void CarregarSessaoEnum()
    {
        var periodoletivo = new List<SelectListItem>();
        foreach (EnumTipoPeriodoLetivo categorias in Enum.GetValues(typeof(EnumTipoPeriodoLetivo)))
        {
            var item = new SelectListItem
            {
                Value = categorias.ObterValorDefault(),
                Text = categorias.ObterDescricao()
            };
            periodoletivo.Add(item);
        }

        var anoletivo = new List<SelectListItem>();
        for (var i = 0; i < 2; i++)
        {
            DateTime addYear = DateTime.Now.AddYears(i);
            int ano = addYear.Year;

            var item = new SelectListItem
            {
                Value = ano.ToString(),
                Text = ano.ToString()
            };

            anoletivo.Add(item);
        }

        ViewData["PeriodoLetivo"] = new SelectList(periodoletivo, "Value", "Text");
        ViewData["AnoLetivo"] = new SelectList(anoletivo, "Value", "Text");
    }
}

