using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WebCore.Areas.AreaManager;
using WebCore.Services.PopUp;
using Domain.DomainServicesInterfaces;
using WebCore.Services.LoginAuthentication;
using Domain.Dtos.Disciplina.Input;
using Domain.Dtos.Assistido.Input;
using ApplicationServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao]
[Area("Administrativo")]
public class DisciplinaController : Controller
{
    private readonly ILoginAuthenticationService _loginService;
    private readonly IDisciplinaService _disciplinaService;

    /// <inheritdoc />
    public DisciplinaController(
        ILoginAuthenticationService loginService,
        IDisciplinaService disciplinaService)
    {
        _loginService = loginService;
        _disciplinaService = disciplinaService;
    }


    /// <summary>
    /// Cria um novo cadastro de disciplina
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Adicionar()
    {
        return View(new DisciplinaAdicionarInModel
        {
            IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync()
        });

        //return View();
    }

    /// <summary>
    /// Cria um novo cadastro de disciplina
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Adicionar(DisciplinaAdicionarInModel model)
    {
        try
        {
            model.IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync();

            if (ModelState.IsValid)
            {
                var retorno = await _disciplinaService.AdicionarAsync(model);

                const string msg = "Registro cadastrado com sucesso!";
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
    /// Carrega a tela que cria um novo cadastro de atendimento
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Editar(int codDisciplina)
    {
        try
        {
            var retorno = await _disciplinaService.RetornarAsync(new DisciplinaRetornarInModel { Disc_ID_Disciplina = codDisciplina });

            var model = new DisciplinaEditarInModel(retorno)
            {
                IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync()
            };

            return View(model);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("index", "Turma", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new DisciplinaEditarInModel());
        }
    }

    /// <summary>
    /// Cria um novo cadastro de atendimento
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Editar(DisciplinaEditarInModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var retorno = await _disciplinaService.EditarAsync(model);

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
}

