using Domain.DomainServicesInterfaces;
using Domain.Dtos.Assistido.Input;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilService.Util;
using WebCore.Areas.AreaManager;
using WebCore.DTO.SimpleError;
using WebCore.Services.LoginAuthentication;
using WebCore.Services.PopUp;
using static System.Net.Mime.MediaTypeNames;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao]
[Area("Administrativo")]
public class AssistidoController : Controller
{
    private readonly ILoginAuthenticationService _loginService;
    private readonly IAssistidoService _assistidoService;
    private readonly ITipoAtividadeRemuneradaService _tipoatividaderemuneradaService;
    private readonly ITipoDependenteService _tipodependenteService;
    private readonly ITipoEscolaridadeService _tipoescolaridadeService;
    private readonly ITipoEstadoCivilService _tipoestadocivilService;
    private readonly ITipoMoradiaService _tipomoradiaService;
    private readonly IWebHostEnvironment _hostingEnvironment;

    /// <inheritdoc />
    public AssistidoController(
        ILoginAuthenticationService loginService,
        IAssistidoService AssistidoService,
        ITipoAtividadeRemuneradaService tipoatividaderemuneradaService,
        ITipoDependenteService tipodependenteService,
        ITipoEscolaridadeService tipoescolaridadeService,
        ITipoEstadoCivilService tipoestadocivilService,
        ITipoMoradiaService tipomoradiaService,
        IWebHostEnvironment hostingEnvironment)
    {
        _loginService = loginService;
        _assistidoService = AssistidoService;
        _tipoatividaderemuneradaService = tipoatividaderemuneradaService;
        _tipodependenteService = tipodependenteService;
        _tipoescolaridadeService = tipoescolaridadeService;
        _tipoestadocivilService = tipoestadocivilService;
        _tipomoradiaService = tipomoradiaService;
        _hostingEnvironment = hostingEnvironment;
    }

    /// <summary>
    /// Lista de todos os assistidos
    /// </summary>
    /// <returns></returns>

    public async Task<IActionResult> Index()
    {
        try
        {
            var dados = await _assistidoService.ListarAsync();

            return View(dados);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal", new { Area = "Administrativo" }) ?? string.Empty);

        }

        return View(new List<Assistido>());
    }

    /// <summary>
    /// Cria um novo cadastro de assistido
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Adicionar()
    {
        CarregarSessaoEnum();

        ViewData["Escolaridade"] = new SelectList(await _tipoescolaridadeService.ListarAsync(), "TpEs_ID_TipoEscolaridade", "TpEs_NM_Descricao");
        ViewData["EstadoCivil"] = new SelectList(await _tipoestadocivilService.ListarAsync(), "TpEC_ID_TipoEstadoCivil", "TpEC_NM_Descricao");
        ViewData["PossuiAtividadeRemunerada"] = new SelectList(await _tipoatividaderemuneradaService.ListarAsync(), "TpAR_ID_TipoAtividadeRemunerada", "TpAR_NM_Descricao");
        ViewData["PossuiDependente"] = new SelectList(await _tipodependenteService.ListarAsync(), "TpDe_ID_TipoDependente", "TpDe_NM_Descricao");
        ViewData["TipoMoradia"] = new SelectList(await _tipomoradiaService.ListarAsync(), "TpMo_ID_TipoMoradia", "TpMo_NM_Descricao");

        return View(new AssistidoAdicionarInModel
        {
            IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync()
        }); 
        
        //return View();
    }

    /// <summary>
    /// Cria um novo cadastro de assistido
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Adicionar(AssistidoAdicionarInModel model)
    {
        try
        {
            CarregarSessaoEnum();

            ViewData["Escolaridade"] = new SelectList(await _tipoescolaridadeService.ListarAsync(), "TpEs_ID_TipoEscolaridade", "TpEs_NM_Descricao");
            ViewData["EstadoCivil"] = new SelectList(await _tipoestadocivilService.ListarAsync(), "TpEC_ID_TipoEstadoCivil", "TpEC_NM_Descricao");
            ViewData["PossuiAtividadeRemunerada"] = new SelectList(await _tipoatividaderemuneradaService.ListarAsync(), "TpAR_ID_TipoAtividadeRemunerada", "TpAR_NM_Descricao");
            ViewData["PossuiDependente"] = new SelectList(await _tipodependenteService.ListarAsync(), "TpDe_ID_TipoDependente", "TpDe_NM_Descricao");
            ViewData["TipoMoradia"] = new SelectList(await _tipomoradiaService.ListarAsync(), "TpMo_ID_TipoMoradia", "TpMo_NM_Descricao");

            model.IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync();

            if (ModelState.IsValid)
            {
                var retorno = await _assistidoService.AdicionarAsync(model);

                const string msg = "Registro cadastrado com sucesso!";
                this.AlertaMsgRedirect(msg, Url.Action("Visualizar", "Assistido", new { codAssistido = retorno.Assi_ID_Assistido }) ?? string.Empty);

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
    /// Editar assistido
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<IActionResult> Editar(int codAssistido)
    {
        try
        {
            CarregarSessaoEnum();

            ViewData["Escolaridade"] = new SelectList(await _tipoescolaridadeService.ListarAsync(), "TpEs_ID_TipoEscolaridade", "TpEs_NM_Descricao");
            ViewData["EstadoCivil"] = new SelectList(await _tipoestadocivilService.ListarAsync(), "TpEC_ID_TipoEstadoCivil", "TpEC_NM_Descricao");
            ViewData["PossuiAtividadeRemunerada"] = new SelectList(await _tipoatividaderemuneradaService.ListarAsync(), "TpAR_ID_TipoAtividadeRemunerada", "TpAR_NM_Descricao");
            ViewData["PossuiDependente"] = new SelectList(await _tipodependenteService.ListarAsync(), "TpDe_ID_TipoDependente", "TpDe_NM_Descricao");
            ViewData["TipoMoradia"] = new SelectList(await _tipomoradiaService.ListarAsync(), "TpMo_ID_TipoMoradia", "TpMo_NM_Descricao");

            var retorno = await _assistidoService.RetornarAsync(new AssistidoRetornarInModel { Assi_ID_Assistido = codAssistido });

            // Validar o carregamento da imagem
            if (retorno.Assi_MM_Imagem == "data:image/jpeg;base64,")
            {
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "appimages", "logo.png");
                byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
                retorno.Assi_MM_Imagem = "data:image/jpeg;base64," + Convert.ToBase64String(imageArray).ToString();
            }

            var model = new AssistidoEditarInModel(retorno)
            {
                IDUsuarioCadastro = await _loginService.CodUsuarioLogadoAsync()
            };

            return View(model);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("index", "Assistido", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new AssistidoEditarInModel());
        }
    }

    /// <summary>
    /// Editar assistido
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Editar(AssistidoEditarInModel model)
    {
        try
        {
            CarregarSessaoEnum();

            ViewData["Escolaridade"] = new SelectList(await _tipoescolaridadeService.ListarAsync(), "TpEs_ID_TipoEscolaridade", "TpEs_NM_Descricao");
            ViewData["EstadoCivil"] = new SelectList(await _tipoestadocivilService.ListarAsync(), "TpEC_ID_TipoEstadoCivil", "TpEC_NM_Descricao");
            ViewData["PossuiAtividadeRemunerada"] = new SelectList(await _tipoatividaderemuneradaService.ListarAsync(), "TpAR_ID_TipoAtividadeRemunerada", "TpAR_NM_Descricao");
            ViewData["PossuiDependente"] = new SelectList(await _tipodependenteService.ListarAsync(), "TpDe_ID_TipoDependente", "TpDe_NM_Descricao");
            ViewData["TipoMoradia"] = new SelectList(await _tipomoradiaService.ListarAsync(), "TpMo_ID_TipoMoradia", "TpMo_NM_Descricao");

            if (ModelState.IsValid)
            {
                var retorno = await _assistidoService.EditarAsync(model);

                const string msg = "Registro editado com sucesso!";
                this.AlertaMsgRedirect(msg, Url.Action("Visualizar", "Assistido", new { codAssistido = retorno.Assi_ID_Assistido }) ?? string.Empty);

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
    /// Visualizar assistido
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Visualizar(int codAssistido)
    {
        try
        {
            var retorno = await _assistidoService.RetornarAsync(new AssistidoRetornarInModel { Assi_ID_Assistido = codAssistido });

            var retVisualizar = new Assistido_Visualizar();

            retVisualizar.Assi_ID_Assistido = retorno.Assi_ID_Assistido;

            //retVisualizar.Assi_MM_Imagem = retorno.Assi_MM_Imagem != "data:image/jpeg;base64," ? "abrir" : "fechar";
            //retVisualizar.Assi_MM_Imagem = retorno.Assi_MM_Imagem != "data:image/jpeg;base64," ? retorno.Assi_MM_Imagem : Encoding.ASCII.GetBytes(Convert.ToBase64String(System.IO.File.ReadAllBytes("~/appimages/logo.jpg"))).ToString();

            if (retorno.Assi_MM_Imagem != "data:image/jpeg;base64,")
                retVisualizar.Assi_MM_Imagem = retorno.Assi_MM_Imagem;
            else
            {
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "appimages", "logo.png");
                byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
                retVisualizar.Assi_MM_Imagem = "data:image/jpeg;base64," + Convert.ToBase64String(imageArray).ToString();
            }

            retVisualizar.Assi_NM_Nome = retorno.Assi_NM_Nome;
            retVisualizar.Assi_NR_RG = retorno.Assi_NR_RG;
            retVisualizar.Assi_NR_CPF = retorno.Assi_NR_CPF;
            retVisualizar.Assi_DT_Nascimento = retorno.Assi_DT_Nascimento;
            retVisualizar.Assi_NR_Idade = retorno.Assi_NR_Idade;
            retVisualizar.Assi_NR_Telefone = retorno.Assi_NR_Telefone;
            retVisualizar.Assi_NM_Mae = retorno.Assi_NM_Mae;
            retVisualizar.Assi_NM_Profissao = retorno.Assi_NM_Profissao;
            retVisualizar.Assi_NM_Endereco = retorno.Assi_NM_Endereco;
            retVisualizar.Assi_NM_Profissao = retorno.Assi_NM_Profissao;
            retVisualizar.Assi_NM_Profissao = retorno.Assi_NM_Profissao;

            retVisualizar.Assi_CD_Sexo = EnumTipoSexoExtensions.ObterDescricao(retorno.Assi_CD_Sexo);
            retVisualizar.Assi_CD_DeficienteFisico = EnumTipoSimNaoExtensions.ObterDescricao(retorno.Assi_CD_DeficienteFisico);
            retVisualizar.Assi_CD_DeficienteMental = EnumTipoSimNaoExtensions.ObterDescricao(retorno.Assi_CD_DeficienteMental);
            retVisualizar.Assi_CD_ImpossibilidadeTrabalho = EnumTipoSimNaoExtensions.ObterDescricao(retorno.Assi_CD_ImpossibilidadeTrabalho);

            retVisualizar.Assi_CD_Score = retorno.Assi_CD_Score;
            retVisualizar.Assi_NR_Score = retorno.Assi_NR_Score;
            retVisualizar.Assi_TX_Observacao = retorno.Assi_TX_Observacao;

            if (!(retorno.Assi_ID_AtividadeRemunerada is null))
            {
                var retAtividadeRemunerada = await _tipoatividaderemuneradaService.RetornarDadosBasicosAsync(retorno.Assi_ID_AtividadeRemunerada);
                retVisualizar.Assi_TX_AtividadeRemunerada = retAtividadeRemunerada.TpAR_NM_Descricao;
            }

            if (!(retorno.Assi_ID_Dependente is null))
            {
                var retDependente = await _tipodependenteService.RetornarDadosBasicosAsync(retorno.Assi_ID_Moradia);
                retVisualizar.Assi_TX_Dependente = retDependente.TpDe_NM_Descricao;
            }

            if (!(retorno.Assi_ID_Escolaridade is null))
            {
                var retEscolaridade = await _tipoescolaridadeService.RetornarDadosBasicosAsync(retorno.Assi_ID_Moradia);
                retVisualizar.Assi_TX_Escolaridade = retEscolaridade.TpEs_NM_Descricao;
            }

            if (!(retorno.Assi_ID_EstadoCivil is null))
            {
                var retEstadoCivil = await _tipoestadocivilService.RetornarDadosBasicosAsync(retorno.Assi_ID_Moradia);
                retVisualizar.Assi_TX_EstadoCivil = retEstadoCivil.TpEC_NM_Descricao;
            }

            if (!(retorno.Assi_ID_Moradia is null))
            {
                var retMoradia = await _tipomoradiaService.RetornarDadosBasicosAsync(retorno.Assi_ID_Moradia);
                retVisualizar.Assi_TX_Moradia = retMoradia.TpMo_NM_Descricao;
            }

            return View(retVisualizar);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("index", "Assistido", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new Assistido());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public void CarregarSessaoEnum()
    {
        var simnao = new List<SelectListItem>();
        foreach (EnumTipoSimNao categorias in Enum.GetValues(typeof(EnumTipoSimNao)))
        {
            var item = new SelectListItem
            {
                Value = categorias.ObterValorDefault(),
                Text = categorias.ObterDescricao()
            };
            simnao.Add(item);
        }

        var sexo = new List<SelectListItem>();
        foreach (EnumTipoSexo categorias in Enum.GetValues(typeof(EnumTipoSexo)))
        {
            var item = new SelectListItem
            {
                Value = categorias.ObterValorDefault(),
                Text = categorias.ObterDescricao()
            };
            sexo.Add(item);
        }

        ViewData["Sexo"] = new SelectList(sexo, "Value", "Text");
        ViewData["PossuiDeficienciaFisica"] = new SelectList(simnao, "Value", "Text");
        ViewData["PossuiDeficienciaMental"] = new SelectList(simnao, "Value", "Text");
        ViewData["PossuiProblemaSaude"] = new SelectList(simnao, "Value", "Text");
    }

    /// <summary>
    /// Listagem assistido por nome
    /// </summary>
    /// <param name="descricao"></param>
    /// <param name="codigoRef"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    public async Task<JsonResult> PesquisaPessoaPorNome(string descricao, string codigoRef)
    {
        try
        {
            descricao = descricao.ToTrimUpper();

            var dadospesquisa = await _assistidoService.ListarAsync();
            var dados = dadospesquisa.Where(o => o.Assi_NM_Nome.ToUpper() == descricao.ToTrimUpper())
                                     .Select(o => new
                                     {
                                         o.Assi_ID_Assistido,
                                         Descricao = o.Assi_NM_Nome,
                                         Url = Url.Action("Visualizar", "Assistido", new { Area = "Administrativo", Assi_ID_Assistido = o.Assi_ID_Assistido })
                                     });

            if (!codigoRef.IsNullOrWhiteSpace())
            {
                if (!int.TryParse(codigoRef, out var cod))
                {
                    throw new Exception("Erro na conversão.");
                }

                dados = dados.Where(o => o.Assi_ID_Assistido != cod);
            }
            return Json(dados);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }
    }

    /// <summary>
    /// Listagem Pessoas por Cpf
    /// </summary>
    /// <param name="descricao"></param>
    /// <param name="codigoRef"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    public async Task<JsonResult> PesquisaPessoaPorCpf(string descricao, string codigoRef)
    {
        try
        {
            descricao = descricao.ApenasNumeros();
            var dadospesquisa = await _assistidoService.ListarAsync();
            var dados = dadospesquisa.Where(o => o.Assi_NR_CPF == descricao)
                                     .Select(o => new
                                     {
                                         o.Assi_ID_Assistido,
                                         Descricao = o.Assi_NM_Nome,
                                         Url = Url.Action("Visualizar", "Assistido", new { Area = "Administrativo", Assi_ID_Assistido = o.Assi_ID_Assistido })
                                     });

            if (!codigoRef.IsNullOrWhiteSpace())
            {
                if (!int.TryParse(codigoRef, out var cod))
                {
                    throw new Exception("Erro na conversão.");
                }

                dados = dados.Where(o => o.Assi_ID_Assistido != cod);
            }
            return Json(dados);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }
    }

    /// <summary>
    /// Listagem Pessoas por Telefone
    /// </summary>
    /// <param name="descricao"></param>
    /// <param name="codigoRef"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    public async Task<JsonResult> PesquisaPessoaPorTelefone(string descricao, string codigoRef)
    {
        try
        {
            descricao = descricao.ApenasNumeros();
            var dadospesquisa = await _assistidoService.ListarAsync();
            var dados = dadospesquisa.Where(o => o.Assi_NR_Telefone == descricao)
                                     .Select(o => new
                                     {
                                         o.Assi_ID_Assistido,
                                         Descricao = o.Assi_NM_Nome,
                                         Url = Url.Action("Visualizar", "Assistido", new { Area = "Administrativo", codAssistido = o.Assi_ID_Assistido })
                                     });

            if (!codigoRef.IsNullOrWhiteSpace())
            {
                if (!int.TryParse(codigoRef, out var cod))
                {
                    throw new Exception("Erro na conversão.");
                }

                dados = dados.Where(o => o.Assi_ID_Assistido != cod);
            }
            return Json(dados);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }
    }
}

