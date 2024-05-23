using ApplicationServices;
using Domain.DomainServicesInterfaces;
using Domain.Dtos.Assistido.Input;
using Domain.Dtos.Atendimento.Input;
using Domain.Dtos.TipoAtendimento.Input;
using Domain.Dtos.Usuario.AlterarSenha.Input;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Areas.AreaManager;
using WebCore.DTO.SimpleError;
using WebCore.Services.LoginAuthentication;
using WebCore.Services.PopUp;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao]
[Area("Administrativo")]
public class PrincipalController : Controller
{
    private readonly ILoginAuthenticationService _loginAuthenticationService;
    private readonly IAtendimentoService _atendimentoService;
    private readonly IAssistidoService _assistidoService;
    private readonly IAtendimento_TipoAtendimentoService _atendimento_tipoatendimentoService;
    private readonly ITipoAtendimentoService _tipoatendimentoService;

    /// <inheritdoc />
    public PrincipalController(          
        ILoginAuthenticationService loginAuthenticationService,
        IAtendimentoService AtendimentoService,
        IAssistidoService assistidoService,
        IAtendimento_TipoAtendimentoService atendimento_tipoatendimentoService,
        ITipoAtendimentoService tipoatendimentoService)
    {
        _loginAuthenticationService = loginAuthenticationService;
        _atendimentoService = AtendimentoService;
        _assistidoService = assistidoService;
        _atendimento_tipoatendimentoService = atendimento_tipoatendimentoService;
        _tipoatendimentoService = tipoatendimentoService;
    }

    /// <summary>
    /// Página inical da aplicação após logado
    /// </summary>
    /// <returns></returns>
    public async Task<ActionResult> Index()
    {
        ViewData["UsuarioLogado"] = await _loginAuthenticationService.UsuarioLogadoAsync();

        // Retornar quantidade de atendimentos realizados nos últimos 7 dias
        var retatendimento = await _atendimentoService.RetornarQtdAtendimentoAsync();

        //CarregarGrafico_Sexo();
        #region CarregarGrafico_Sexo

        var sexo = new List<string>();
        var qtdatendimentosexo = new List<int>();

        var qtdatenFeminino = 0;
        var qtdatenmasculino = 0;

        // Iniciar a montagem do gráfico de atendimento por sexo
        for (int i = 0; i < retatendimento.Count; i++)
        {
            var retassistido = await _assistidoService.RetornarAsync(new AssistidoRetornarInModel { Assi_ID_Assistido = retatendimento[i].Aten_ID_Assistido });

            if (retassistido.Assi_CD_Sexo == "F") { qtdatenFeminino++; }
            else if (retassistido.Assi_CD_Sexo == "M") { qtdatenmasculino++; }
        }

        sexo.Add("Feminino");
        sexo.Add("Masculino");
        qtdatendimentosexo.Add(qtdatenFeminino);
        qtdatendimentosexo.Add(qtdatenmasculino);

        if ((qtdatenFeminino + qtdatenmasculino) < retatendimento.Count)
        {
            sexo.Add("");
            qtdatendimentosexo.Add(retatendimento.Count - (qtdatenFeminino + qtdatenmasculino));
        }

        // Gravar as informações nas variáveis para a página
        ViewBag.Sexo = sexo;
        ViewBag.QtdAtenSexo = qtdatendimentosexo;

        #endregion

        //CarregarGrafico_TipoAtendimento();
        #region CarregarGrafico_TipoAtendimento

        var tipoatendimento = new List<string>();
        var qtdtipoatendimento = new List<int>();

        int[] arraytipoatendimento = new int[50];
        int linha = 0;

        for (int i = 0; i < retatendimento.Count; i++)
        {
            var retAtendimento_TipoAtendimento = await _atendimento_tipoatendimentoService.RetornarListaDadosAsync(retatendimento[i].Aten_ID_Atendimento);

            for (int j = 0; j < retAtendimento_TipoAtendimento.Count; j++)
            {
                arraytipoatendimento[linha] += retAtendimento_TipoAtendimento[j].AtTA_ID_TipoAtendimento;
                linha++;
            }
        }

        var arrayAgrupadosRepeticaoCount = arraytipoatendimento.GroupBy(x => x).Select(a => new { Item = a.Key, Quant = a.Count() }).ToArray();

        for (int i = 0; i < arrayAgrupadosRepeticaoCount.Length; i++)
        {
            if (arrayAgrupadosRepeticaoCount[i].Item != 0)
            {
                var retTipoAtendimento = await _tipoatendimentoService.RetornarAsync(new TipoAtendimentoRetornarInModel { TpAt_ID_TipoAtendimento = arrayAgrupadosRepeticaoCount[i].Item });

                tipoatendimento.Add(retTipoAtendimento.TpAt_NM_Descricao);
                qtdtipoatendimento.Add(arrayAgrupadosRepeticaoCount[i].Quant);
            }
        }

        ViewBag.TipoAtendimento = tipoatendimento;
        ViewBag.QtdTipoAtendimento = qtdtipoatendimento;

        #endregion

        return View();
    }

    /// <summary>
    /// Página do leitor de QRCode
    /// </summary>
    /// <returns></returns>
    public ActionResult QrReader()
    {
        return View();
    }

    /// <summary>
    /// Página de alteração de senha de usuário
    /// </summary>
    /// <returns></returns>
    public  async Task<ActionResult> AlterarSenha()
    {
            
        try
        {
            var usuarioLogado =  await _loginAuthenticationService.UsuarioLogadoAsync();
            ViewData["usuario"] = usuarioLogado;
            var mudarSenhaUsuarioModel = new UsuarioAlterarSenhaInModel { CodUsuario = usuarioLogado.Codigo };
                
            return View(mudarSenhaUsuarioModel);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal") ?? string.Empty);
        }
        return View();
    }

    /// <summary>
    /// Post da página de alteração de senha de usuário
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AlterarSenha(UsuarioAlterarSenhaInModel model)
    {
        try
        {
            var usuarioLogado = await _loginAuthenticationService.UsuarioLogadoAsync();
            ViewData["usuario"] = usuarioLogado;
            if (ModelState.IsValid)
            {
                model.CodUsuario = usuarioLogado.Codigo;
                await _loginAuthenticationService.AlterarSenhaAsync(model);

                var mensagem = "Senha do usuário foi alterada com sucesso! ";
                this.AlertaMsgRedirect(mensagem, Url.Action("Index", "Principal") ?? string.Empty);

                ModelState.Clear();
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
    //[HttpPost]
    //public async Task<JsonResult> DadosGrafico_SexoxAtendimento()
    //{
    //    try
    //    {
    //        var retorno = await _assistidoService.RetornarAsync(new AssistidoRetornarInModel { Assi_ID_Assistido = codAssistido });

    //        return Json(new { AssistidoNome = retorno.Assi_NM_Nome }); ;
    //    }
    //    catch (Exception ex)
    //    {
    //        return Json(new SimpleError(ex));
    //    }
    //}

    private async Task CarregarGrafico_Sexo()
    {
        var sexo = new List<string>();
        var qtdatendimento = new List<int>();
        var qtdatenFeminino = 0;
        var qtdatenmasculino = 0;

        var retatendimento = await _atendimentoService.RetornarQtdAtendimentoAsync();

        for (int i = 0; i < retatendimento.Count; i++)
        {
            var retassistido = await _assistidoService.RetornarAsync(new AssistidoRetornarInModel { Assi_ID_Assistido = retatendimento[i].Aten_ID_Assistido });

            if (retassistido.Assi_CD_Sexo == "F") { qtdatenFeminino++; }
            else if (retassistido.Assi_CD_Sexo == "M") { qtdatenmasculino++; }
        }

        sexo.Add("Feminino");
        sexo.Add("Masculino");
        qtdatendimento.Add(qtdatenFeminino);
        qtdatendimento.Add(qtdatenmasculino);

        if ((qtdatenFeminino + qtdatenmasculino) < retatendimento.Count) 
        {
            sexo.Add("");
            qtdatendimento.Add(retatendimento.Count - (qtdatenFeminino + qtdatenmasculino));
        }

        ViewBag.Sexo = sexo;
        ViewBag.QtdAtendimento = qtdatendimento;
    }


    public async Task gerargrafico2()
    {
        var sexo = new List<string>();
        var qtdatendimento = new List<int>();

        sexo.Add("Feminino");
        sexo.Add("Masculino");
        qtdatendimento.Add(0);
        qtdatendimento.Add(0);

        var retorno = await _atendimentoService.QtdAtendimento_SexoAsync();
        var retlista = await _atendimentoService.RetornarQtdAtendimentoAsync();

        //dentro de um for...levantar todas as informações de atendimento realizadas na semana
        qtdatendimento[0] = 5;
        qtdatendimento[1] = 25;

        ViewBag.Sexo = sexo;
        ViewBag.QtdAtendimento = qtdatendimento;
    }

}