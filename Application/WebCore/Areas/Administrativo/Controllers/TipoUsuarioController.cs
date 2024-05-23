using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.DomainServicesInterfaces;
using Domain.Dtos.RegraSistema.AdicionarRegraPerfil.Input;
using Domain.Dtos.RegraSistema.AdicionarTodasPerfil.Input;
using Domain.Dtos.RegraSistema.RemoverPermissaoPerfil.Input;
using Domain.Dtos.RegraSistema.RemoverTodasPerfil.Input;
using Domain.Dtos.RegraSistema.RetornaRegrasNegadasTipoUsuario.Input;
using Domain.Dtos.RegraSistema.RetornaRegrasTipoUsuario.Input;
using Domain.Dtos.RegraSistema.UpdateDiretivasPerfilTipoUsuario.Input;
using Domain.Dtos.TipoUsuario.Criar.Input;
using Domain.Dtos.TipoUsuario.Editar.Input;
using Domain.Dtos.TipoUsuario.Listar.Output;
using Domain.Dtos.TipoUsuario.Retorna.Input;
using Domain.Dtos.TipoUsuario.Visualizar.Input;
using Domain.Dtos.TipoUsuario.Visualizar.Output;
using Domain.Dtos.Usuario.ListarPorTipoUsuario.Input;
using Domain.Enums;
using UtilService.Util;
using WebCore.Areas.AreaManager;
using WebCore.DTO.SimpleError;
using WebCore.Services.LoginAuthentication;
using WebCore.Services.PopUp;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[Area("Administrativo")]
[CheckNivelPermissao(TipoRegraSistema.CadastroTipoUsuario)]
public class TipoUsuarioController : Controller
{
    private readonly ILoginAuthenticationService _loginAuthenticationService;
    private readonly ITipoUsuarioService _tipousuarioService;
    private readonly IRegraSistemaService _regraSistemaCore;
    private readonly ITipoUsuarioCheckerService _tipoUsuarioCheckerCore;

    /// <inheritdoc />
    public TipoUsuarioController(
        ILoginAuthenticationService loginAuthenticationService,
        ITipoUsuarioService tipousuarioService,
        IRegraSistemaService regraSistemaCore,
        ITipoUsuarioCheckerService tipoUsuarioCheckerCore)
    {
        _loginAuthenticationService = loginAuthenticationService;
        _tipousuarioService = tipousuarioService;
        _regraSistemaCore = regraSistemaCore;
        _tipoUsuarioCheckerCore = tipoUsuarioCheckerCore;
    }

    /// <summary>
    /// Página de edição de tipo de usuário
    /// </summary>
    /// <param name="codTipoUsuario"></param>
    /// <returns></returns>
    public async Task<IActionResult> Editar(int codTipoUsuario)
    {
        try
        {
            var tipoUsuario = await _tipoUsuarioCheckerCore.RetornarAsync(new TipoUsuarioTipoUsuarioRetornarInModel { Codigo = codTipoUsuario });

            if (_tipousuarioService.ListaTiposUsuarioEdicaoBloqueada().ToList().Contains(tipoUsuario.Codigo))
            {
                return RedirectToAction("Index", "TipoUsuario", new { Area = "Administrativo" });
            }

            var dado = new TipoUsuarioEditarTipoUsuarioInModel
            {
                Codigo = tipoUsuario.Codigo,
                Descricao = tipoUsuario.Descricao
            };

            return View(dado);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "TipoUsuario", new { Area = "Administrativo" }) ?? string.Empty);
        }

        return View(new TipoUsuarioEditarTipoUsuarioInModel());
    }

    /// <summary>
    /// Edita um determinado tipo de usuário/perfil
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Editar(TipoUsuarioEditarTipoUsuarioInModel model)
    {
        try
        {
            if (_tipousuarioService.ListaTiposUsuarioEdicaoBloqueada().ToList().Contains(model.Codigo))
            {
                return RedirectToAction("Index", "TipoUsuario", new { Area = "Administrativo" });
            }
            model.CodUsuarioAlteracao = await _loginAuthenticationService.CodUsuarioLogadoAsync();
            if (ModelState.IsValid)
            {
                await _tipousuarioService.EditarTipoUsuarioAsync(model);
                this.AlertaMsgRedirect(
                    "Alterações realizadas com sucesso!",
                    Url.Action("Configuracoes", new { codTipoUsuario = model.Codigo }) ?? string.Empty);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        return View(model);
    }

    /// <summary>
    /// Lista de todos os usuários que estão vinculados ao tipo de usuário/perfil
    /// </summary>
    /// <param name="codTipoUsuario"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> ListarUsuariosPorTipo(int codTipoUsuario)
    {
        try
        {
            var ret = await _tipousuarioService.ListarUsuariosPorTipoAsync(
                new UsuarioListarUsuariosPorTipoInModel
                {
                    CodTipoUsuario = codTipoUsuario,
                    CodUsuarioConsulta = await _loginAuthenticationService.CodUsuarioLogadoAsync()
                });

            return Json(ret.ListaUsuario);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }
    }

    /// <summary>
    /// Redefine todos os usuários de um determinado tipo para que fiquem todos dentro das permissões do tipo de usuário removendo toda e qualquer permissão customizada
    /// </summary>
    /// <param name="codTipoUsuario"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> ForcarTodosUsuariosPerfil(int codTipoUsuario)
    {
        try
        {
            await _regraSistemaCore.RedefinirRegrasSistemaPadraoTipoUsuarioAsync(
                new RegraSistemaRedefinirRegrasSistemaPadraoTipoUsuarioInModel
                {
                    CodTipoUsuario = codTipoUsuario,
                    CodUsuarioAlteracao = await _loginAuthenticationService.CodUsuarioLogadoAsync()
                });

            return Json(true);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }
    }

    /// <summary>
    /// Retorna todas as regras de sistema que não foram aplicadas a um determinado perfil/tipo usuário
    /// </summary>
    /// <param name="codTipoUsuario"></param>
    /// <param name="nomeRegra"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> ListarPermissoesInativas(int codTipoUsuario, string nomeRegra)
    {
        try
        {

            var regras = (await _regraSistemaCore.RegrasSistemaNegadasTipoUsuarioAsync(
                new RegrasSistemaNegadasTipoUsuarioInModel
                {
                    CodTipoUsuario = codTipoUsuario
                })).ListaRegras;


            if (!nomeRegra.IsNullOrWhiteSpace())
            {
                regras = regras.Where(o => o.RegraSistemaDescricao.ToLower().Contains(nomeRegra.ToLower())).ToList();
            }

            return Json(regras);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }
    }

    /// <summary>
    /// Retorna todas as regras de sistema de um determinado tipo de usuário/perfil
    /// </summary>
    /// <param name="codTipoUsuario"></param>
    /// <param name="nomeRegra"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> ListarPermissoesAtivas(int codTipoUsuario, string nomeRegra)
    {
        try
        {
            var regras = (await _regraSistemaCore.RetornaRegrasSistemaTipoUsuarioAsync(
                new RegraSistemaRetornaRegrasSistemaTipoUsuarioInModel
                {
                    CodTipoUsuario = codTipoUsuario
                })).ListaRegras;


            if (!nomeRegra.IsNullOrWhiteSpace())
            {
                regras = regras.Where(o => o.RegraSistemaDescricao.ToLower().Contains(nomeRegra.ToLower())).ToList();
            }

            return Json(regras);

        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }
    }

    /// <summary>
    /// Listagem de todos os tipos de usuários/perfil
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        try
        {
            var tiposUsuario = await _tipousuarioService.ListarAsync();

            return View(tiposUsuario);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal", new { Area = "Administrativo" }) ?? string.Empty);

        }
        return View(new TipoUsuarioListarOutModel());
    }

    /// <summary>
    /// Retorna dados detalhados de um determinado tipo de usuário/perfil
    /// </summary>
    /// <param name="codTipoUsuario"></param>
    /// <returns></returns>
    public async Task<IActionResult> Configuracoes(int codTipoUsuario)
    {
        try
        {
            var model = await _tipousuarioService.VisualizarAsync(new TipoUsuarioVisualizarInModel { Codigo = codTipoUsuario, CodUsuarioConsulta = await _loginAuthenticationService.CodUsuarioLogadoAsync() });
            //Utilizado para aviso em tela que referencia a alteração de um tipo de usuário que é o do próprio usuário logado.               
            ViewData["UsuarioMesmoTipo"] = (await _loginAuthenticationService.UsuarioLogadoAsync()).CodTipoUsuario == model.Codigo;
            if (!model.Editavel)
                this.AlertaMsg("Esta tela não pode realizar alterações neste tipo de usuário. Sua utilização limita-se a consulta.");

            return View(model);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "TipoUsuario", new { Area = "Administrativo" }) ?? string.Empty);
        }

        return View(new TipoUsuarioVisualizarOutModel());
    }

    /// <summary>
    /// Cria um novo tipo de usuário/perfil
    /// </summary>
    /// <returns></returns>
    public IActionResult Adicionar()
    {
        return View(new TipoUsuarioAdicionarInModel());
    }

    /// <summary>
    /// Cria um novo tipo de usuário/perfil
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Adicionar(TipoUsuarioAdicionarInModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                model.CodUsuarioInclusao = await _loginAuthenticationService.CodUsuarioLogadoAsync();
                var tipoUsuarioCriado = await _tipousuarioService.AdicionarAsync(model);
                var msg = "Tipo de usuário cadastrado com sucesso!";
                this.AlertaMsgRedirect(msg, Url.Action("Configuracoes", "TipoUsuario", new { CodTipoUsuario = tipoUsuarioCriado.Codigo }) ?? string.Empty);

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
    /// Listagem os tipos de usuários/perfil
    /// </summary>
    /// <param name="descricao"></param>
    /// <param name="codigoRef"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    public async Task<JsonResult> PesquisaTipoUsuarioDescricao(string descricao, string codigoRef)
    {
        try
        {
            descricao = descricao.ToTrimUpper();
            var dadospesquisa = await _tipousuarioService.ListarAsync();
            var dados = dadospesquisa.ListaTiposUsuario
                .Where(o => o.Descricao.ToUpper() == descricao)
                .Select(o => new
                {
                    o.Codigo,
                    o.Descricao,
                    Url = Url.Action("Configuracoes", "TipoUsuario", new { Area = "Administrativo", codTipoUsuario = o.Codigo })
                });

            if (!codigoRef.IsNullOrWhiteSpace())
            {
                if (!int.TryParse(codigoRef, out var cod))
                {
                    throw new Exception("Erro na conversão.");
                }

                dados = dados.Where(o => o.Codigo != cod);
            }
            return Json(dados);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }

    }

    /// <summary>
    /// Adiciona uma regra de sistema para um perfil/tipo de usuário
    /// </summary>
    /// <param name="codTipoUsuario"></param>
    /// <param name="codRegraSistema"></param>
    /// <param name="aplicaRegraRetroativa"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> Atribuir(int codTipoUsuario, int codRegraSistema, bool aplicaRegraRetroativa = true)
    {
        try
        {
            await _regraSistemaCore.AdicionarRegraSistemaPerfilUsuarioAsync(
                new RegraSistemaAdicionarRegraSistemaPerfilUsuarioInModel
                {
                    CodTipoUsuario = codTipoUsuario,
                    CodRegraSistema = codRegraSistema,
                    CodUsuarioAlteracao = await _loginAuthenticationService.CodUsuarioLogadoAsync(),
                    AplicaRegraRetroativa = aplicaRegraRetroativa
                });

            return Json(true);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }
    }

    /// <summary>
    /// Adiciona todas as regras de sistema para um perfil/tipo de usuário
    /// </summary>
    /// <param name="codTipoUsuario"></param>
    /// <param name="aplicaRegraRetroativa"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> AtribuirTodas(int codTipoUsuario, bool aplicaRegraRetroativa = true)
    {
        try
        {
            await _regraSistemaCore.AdicionarTodasRegraSistemaPerfilUsuarioAsync(
                new RegraSistemaAdicionarTodasRegraSistemaPerfilUsuarioInModel
                {
                    CodTipoUsuario = codTipoUsuario,
                    CodUsuarioAlteracao = await _loginAuthenticationService.CodUsuarioLogadoAsync(),
                    AplicaRegraRetroativa = aplicaRegraRetroativa
                });

            return Json(true);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }
    }

    /// <summary>
    /// Remove todas as regras de sistema de um tipo/perfil usuário
    /// </summary>
    /// <param name="codTipoUsuario"></param>
    /// <param name="aplicaRegraRetroativa"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> RemoverTodas(int codTipoUsuario, bool aplicaRegraRetroativa = true)
    {
        try
        {
            await _regraSistemaCore.RemoverTodasRegrasSistemaPerfilUsuarioAsync(
               new RegraSistemaRemoverTodasRegrasSistemaPerfilUsuarioInModel
               {
                   CodTipoUsuario = codTipoUsuario,
                   CodUsuarioAlteracao = await _loginAuthenticationService.CodUsuarioLogadoAsync(),
                   AplicaRegraRetroativa = aplicaRegraRetroativa
               });

            return Json(true);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }
    }

    /// <summary>
    /// Remove uma regra de sistema de um perfil de usuário
    /// </summary>
    /// <param name="codTipoUsuario"></param>
    /// <param name="codRegraSistema"></param>
    /// <param name="aplicaRegraRetroativa"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> Remover(int codTipoUsuario, int codRegraSistema, bool aplicaRegraRetroativa = true)
    {
        try
        {
            await _regraSistemaCore.RemoverRegraSistemaTipoUsuarioAsync(
                new RegraSistemaRemoverRegraSistemaTipoUsuarioInModel
                {
                    CodTipoUsuario = codTipoUsuario, 
                    CodRegraSistema = codRegraSistema, 
                    AplicaRegraRetroativa = aplicaRegraRetroativa, 
                    CodUsuarioAlteracao = await _loginAuthenticationService.CodUsuarioLogadoAsync()
                });

            return Json(true);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }

    }

    /// <summary>
    /// Partial de PermissaoDiferenciadaUsuario
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult WindowPermissaoDiferenciadaUsuario()
    {
        return PartialView("~/Areas/Administrativo/Views/TipoUsuario/Partial/WindowPermissaoDiferenciadaUsuario.cshtml");
    }
}