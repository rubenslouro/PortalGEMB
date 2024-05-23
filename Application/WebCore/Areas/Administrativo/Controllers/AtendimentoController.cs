using ApplicationServices;
using Domain.DomainServicesInterfaces;
using Domain.Dtos.Assistido.Input;
using Domain.Dtos.Atendimento.Input;
using Domain.Dtos.AtendimentoTipoAtendimento.Input;
using Domain.Dtos.TipoAtendimento.Input;
using Domain.Dtos.TipoAtividadeRemunerada.Input;
using Domain.Entities;
using Domain.Enums;
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
public class AtendimentoController : Controller
{
    private readonly ILoginAuthenticationService _loginService;
    private readonly IAssistidoService _assistidoService;
    private readonly IAtendimentoService _atendimentoService;
    private readonly ITipoAtendimentoService _tipoatendimentoService;
    private readonly IAtendimento_TipoAtendimentoService _atendimento_tipoatendimentoService;

    /// <inheritdoc />
    public AtendimentoController(
        ILoginAuthenticationService loginService,
        IAssistidoService AssistidoService,
        IAtendimentoService AtendimentoService,
        ITipoAtendimentoService tipoatendimentoService,
        IAtendimento_TipoAtendimentoService atendimento_tipoatendimentoService)
    {
        _loginService = loginService;
        _assistidoService = AssistidoService;
        _atendimentoService = AtendimentoService;
        _tipoatendimentoService = tipoatendimentoService;
        _atendimento_tipoatendimentoService = atendimento_tipoatendimentoService;
    }

    public IActionResult Historico()
    {
        return View();
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var retorno = await _atendimentoService.ListarAsync();

            List<AtendimentoHistorico> listaAtendimento = new List<AtendimentoHistorico>();

            for (int i = 0; i < retorno.Count; i++)
            {
                AtendimentoHistorico atendimento = new AtendimentoHistorico();

                atendimento.Atendimento = retorno[i];

                // Retornar os dados do assistido para exibir em tela
                var retassistido = await _assistidoService.RetornarAsync(new AssistidoRetornarInModel { Assi_ID_Assistido = retorno[i].Aten_ID_Assistido });
                atendimento.Assistido = retassistido;

                // Retornar os dados da lista do tipo de atendimento para o atendimento de um assistido
                var tipoAtendimento = new List<TipoAtendimento>();

                var retAtendimento_TipoAtendimento = await _atendimento_tipoatendimentoService.RetornarListaDadosAsync(atendimento.Atendimento.Aten_ID_Atendimento);

                for (int j = 0; j < retAtendimento_TipoAtendimento.Count; j++)
                {
                    var retTipoAtendimento = await _tipoatendimentoService.RetornarAsync(new TipoAtendimentoRetornarInModel { TpAt_ID_TipoAtendimento = retAtendimento_TipoAtendimento[j].AtTA_ID_TipoAtendimento });

                    var item = new TipoAtendimento
                    {
                        TpAt_ID_TipoAtendimento = retTipoAtendimento.TpAt_ID_TipoAtendimento,
                        TpAt_NM_Descricao = retTipoAtendimento.TpAt_NM_Descricao
                    };
                    tipoAtendimento.Add(item);
                }
                atendimento.TipoAtendimento = tipoAtendimento;

                listaAtendimento.Add(atendimento);
            }

            return View(listaAtendimento);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal", new { Area = "Administrativo" }) ?? string.Empty);
        }

        return View(new List<Atendimento>());
    }

    /// <summary>
    /// Carrega a tela que cria um novo cadastro de atendimento
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Adicionar(int codAssistido)
    {
        try
        {
            ViewData["TipoAtendimento"] = new SelectList(await _tipoatendimentoService.ListarAsync(), "TpAt_ID_TipoAtendimento", "TpAt_NM_Descricao");

            var allTipoAtendimento = await _tipoatendimentoService.ListarAsync(); //returns List<Genre>
            
            var checkBoxListItems = new List<CheckBoxList>();
            foreach (var tipoAtendimento in allTipoAtendimento)
            {
                checkBoxListItems.Add(new CheckBoxList()
                {
                    Value = tipoAtendimento.TpAt_ID_TipoAtendimento,
                    Text = tipoAtendimento.TpAt_NM_Descricao,
                    IsChecked = false 
                });
            }

            if (codAssistido > 0)
            {
                var retorno = await _assistidoService.RetornarAsync(new AssistidoRetornarInModel { Assi_ID_Assistido = codAssistido });

                return View(new AtendimentoAdicionarInModel
                {
                    IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync(),
                    Aten_ID_Assistido = codAssistido,
                    Aten_NM_Nome = retorno.Assi_NM_Nome,
                    ChechBoxList = checkBoxListItems
                });
            }
            else
            {
                return View(new AtendimentoAdicionarInModel
                {
                    IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync(),
                    ChechBoxList = checkBoxListItems
                });
            }
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal", new { Area = "Administrativo" }) ?? string.Empty);
        }

        return View(new AtendimentoAdicionarInModel());
    }

    /// <summary>
    /// Cria um novo cadastro de atendimento
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Adicionar(AtendimentoAdicionarInModel model)
    {
        try
        {
            ViewData["TipoAtendimento"] = new SelectList(await _tipoatendimentoService.ListarAsync(), "TpAt_ID_TipoAtendimento", "TpAt_NM_Descricao");

            model.IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync();

            if (ModelState.IsValid)
            {
                var retorno = await _atendimentoService.AdicionarAsync(model);

                const string msg = "Registro cadastrado com sucesso!";
                this.AlertaMsgRedirect(msg, Url.Action("Visualizar", "Atendimento", new { codAtendimento = retorno.Aten_ID_Atendimento }) ?? string.Empty);

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
    [HttpPost]
    public async Task<JsonResult> Recuperar(int codAssistido)
    {
        try
        {
            var retorno = await _assistidoService.RetornarAsync(new AssistidoRetornarInModel { Assi_ID_Assistido = codAssistido });

            return Json(new { AssistidoNome = retorno.Assi_NM_Nome }); ;
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }
    }

    /// <summary>
    /// Visualizar atendimento
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Visualizar(int codAtendimento)
    {
        try
        {
            AtendimentoHistorico atendimento = new AtendimentoHistorico();

            var retorno = await _atendimentoService.RetornarAsync(new AtendimentoRetornarInModel { codAtendimento = codAtendimento });
            atendimento.Atendimento = retorno;

            // Retornar os dados do assistido para exibir em tela
            var retassistido = await _assistidoService.RetornarAsync(new AssistidoRetornarInModel { Assi_ID_Assistido = retorno.Aten_ID_Assistido });
            atendimento.Assistido = retassistido;

            // Retornar os dados da lista do tipo de atendimento para o atendimento de um assistido
            var tipoAtendimento = new List<TipoAtendimento>();

            var retAtendimento_TipoAtendimento = await _atendimento_tipoatendimentoService.RetornarListaDadosAsync(atendimento.Atendimento.Aten_ID_Atendimento);

            for (int i = 0; i < retAtendimento_TipoAtendimento.Count; i++)
            {
                var retTipoAtendimento = await _tipoatendimentoService.RetornarAsync(new TipoAtendimentoRetornarInModel { TpAt_ID_TipoAtendimento = retAtendimento_TipoAtendimento[i].AtTA_ID_TipoAtendimento });

                var item = new TipoAtendimento
                {
                    TpAt_ID_TipoAtendimento = retTipoAtendimento.TpAt_ID_TipoAtendimento,
                    TpAt_NM_Descricao = retTipoAtendimento.TpAt_NM_Descricao
                };
                tipoAtendimento.Add(item);
            }
            atendimento.TipoAtendimento = tipoAtendimento;

            return View(atendimento);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Atendimento", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new Atendimento());
        }
    }
}

