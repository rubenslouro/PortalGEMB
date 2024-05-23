using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.DomainServicesInterfaces;
using Domain.Dtos.RegraSistema.AdicionarRegra.Input;
using Domain.Dtos.RegraSistema.AdicionarTodas.Input;
using Domain.Dtos.RegraSistema.RemoverRegra.Input;
using Domain.Dtos.RegraSistema.RemoverTodas.Input;
using Domain.Dtos.RegraSistema.RetornaRegrasNegadasUsuario.Input;
using Domain.Dtos.RegraSistema.RetornaRegrasUsuario.Input;
using Domain.Dtos.Usuario.Afastar.Input;
using Domain.Dtos.Usuario.Criar.Input;
using Domain.Dtos.Usuario.Editar.Input;
using Domain.Dtos.Usuario.Listar.Input;
using Domain.Dtos.Usuario.Listar.Output;
using Domain.Dtos.Usuario.Readmitir.Input;
using Domain.Dtos.Usuario.RetornaDetalhado.Input;
using Domain.Dtos.Usuario.RetornaDetalhado.Output;
using Domain.Dtos.Usuario.RetornaParaEdicao.Input;
using Domain.Enums;
using UtilService.Util;
using WebCore.Areas.AreaManager;
using WebCore.DTO.SimpleError;
using WebCore.Services.LoginAuthentication;
using WebCore.Services.PopUp;

namespace WebCore.Areas.Administrativo.Controllers;

/// <inheritdoc />
[CheckNivelPermissao(TipoRegraSistema.CadastroUsuario)]
[Area("Administrativo")]
public class UsuarioController : Controller
{
    private readonly IUsuarioService _usuarioService;
    private readonly ILoginAuthenticationService _loginAuthenticationService;
    private readonly IRegraSistemaService _regraSistemaCore;
    private readonly IMapper _mapper;
    private readonly ITipoUsuarioService _tipousuarioService;

    /// <inheritdoc />
    public UsuarioController(
        IUsuarioService usuarioService,
        ILoginAuthenticationService loginAuthenticationService,
        IRegraSistemaService regraSistemaCore,
        IMapper mapper,
        ITipoUsuarioService tipousuarioService)
    {
        _usuarioService = usuarioService;
        _loginAuthenticationService = loginAuthenticationService;
        _regraSistemaCore = regraSistemaCore;
        _mapper = mapper;
        _tipousuarioService = tipousuarioService;
    }


    /// <summary>
    /// Retorna o usuário de forma detalhada
    /// </summary>
    /// <param name="codUsuario"></param>
    /// <returns></returns>
    public async Task<IActionResult> Configuracoes(int codUsuario)
    {
        try
        {
            var model = await _usuarioService.RetornaDetalhadoAsync(new UsuarioRetornaDetalhadoInModel { CodUsuario = codUsuario, CodUsuarioSolicitacao = await _loginAuthenticationService.CodUsuarioLogadoAsync() });

            if (!model.Editavel)
                this.AlertaMsg("Esta tela não pode realizar alterações neste tipo de usuário. Sua utilização limita-se a consulta.");
            return View(model);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Usuario", _usuarioService.ListarAsync(new UsuarioListarInModel { Codigo = await _loginAuthenticationService.CodUsuarioLogadoAsync() })) ?? string.Empty);
        }

        return View(new UsuarioRetornaDetalhadoOutModel());
    }

    /// <summary>
    /// Lista de todos os usuários
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Index()
    {
        try
        {
            var dados = await _usuarioService.ListarAsync(new UsuarioListarInModel { Codigo = await _loginAuthenticationService.CodUsuarioLogadoAsync() });
            return View(dados);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("Index", "Principal") ?? string.Empty);
        }
        return View(new UsuarioListarOutModel());
    }

    /// <summary>
    /// Retornas todas as regras de sistema vinculadas a um determinado usuário
    /// </summary>
    /// <param name="codUsuario"></param>
    /// <param name="nomeRegra"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> ListarPermissoesAtivas(int codUsuario, string nomeRegra)
    {
        try
        {
            var regras = (await _regraSistemaCore.RetornaRegrasSistemaUsuarioAsync(
                new RegraSistemaRetornaRegrasSistemaUsuarioInModel
                {
                    CodUsuario = codUsuario
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
    /// Exibe todas as regras de sistema que não foram aplicadas a um determinado usuário do sistema
    /// </summary>
    /// <param name="codUsuario"></param>
    /// <param name="nomeRegra"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> ListarPermissoesInativas(int codUsuario, string nomeRegra)
    {
        try
        {
            var regras = (await _regraSistemaCore.RegrasSistemaNegadasUsuarioAsync(new RegrasSistemaNegadasUsuarioInModel
            {
                CodUsuario = codUsuario
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
    /// Remove todas as regras de sistema de um usuário
    /// </summary>
    /// <param name="codUsuario"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> RemoverTodas(int codUsuario)
    {
        try
        {
            await _regraSistemaCore.RegrasSistemaRemoverTodasUsuarioAsync(
                new RegraSistemaRemoverTodasUsuarioInModel
                {
                    CodUsuario = codUsuario,
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
    /// Remove uma determinada regra de sistema para um usuário
    /// </summary>
    /// <param name="codUsuario"></param>
    /// <param name="codRegraSistema"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> Remover(int codUsuario, int codRegraSistema)
    {
        try
        {
            await _regraSistemaCore.RemoverRegraSistemaUsuarioAsync(new RegraSistemaRemoverRegraSistemaUsuarioInModel
            {
                CodUsuario = codUsuario,
                CodRegraSistema = codRegraSistema,
                CodUsuarioAltecao = await _loginAuthenticationService.CodUsuarioLogadoAsync()

            });

            return Json(true);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }

    }

    /// <summary>
    /// Adiciona todas as regras de sistema a um usuário com exeção das regras cadastro de usuário e cadastro de tipos de usuário
    /// </summary>
    /// <param name="codUsuario"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> AtribuirTodas(int codUsuario)
    {
        try
        {
            await _regraSistemaCore.AdicionarTodasRegrasSistemaUsuarioAsync(new RegraSistemaAdicionarTodasRegrasSistemaUsuarioInModel
            {
                CodUsuario = codUsuario,
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
    /// Adiciona uma regra de sistema para um usuário específico
    /// </summary>
    /// <param name="codUsuario"></param>
    /// <param name="codRegraSistema"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> Atribuir(int codUsuario, int codRegraSistema)
    {
        try
        {
            await _regraSistemaCore.RegraSistemaAdicionarUsuarioAsync(
                new RegraSistemaAdicionarUsuarioInModel
                {
                    CodUsuario = codUsuario,
                    CodRegraSistema = codRegraSistema,
                    CodUsuarioInclusao = await _loginAuthenticationService.CodUsuarioLogadoAsync()
                });

            return Json(true);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }
    }

    /// <summary>
    /// Listagem de todos os tipos de usuários/perfil sem o perfil Master
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Adicionar()
    {
        var tiposUsuario = await _tipousuarioService.ListarTodosSemMasterAsync();
        ViewData["TipoUsuario"] = new SelectList(tiposUsuario.ListaTiposUsuario, "Codigo", "Descricao");
        var usuarioAdicionarInModel = new UsuarioAdicionarInModel { CodUsuarioCadastro = await _loginAuthenticationService.CodUsuarioLogadoAsync() };
        return View(usuarioAdicionarInModel);
    }

    /// <summary>
    /// Lista de todos os usuários que estão vinculados ao tipo de usuário/perfil (sem os do tipo Master)
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Adicionar(UsuarioAdicionarInModel model)
    {
        try
        {
            var tiposUsuario = await _tipousuarioService.ListarTodosSemMasterAsync();
            ViewData["TipoUsuario"] = new SelectList(tiposUsuario.ListaTiposUsuario, "Codigo", "Descricao");

            if (ModelState.IsValid)
            {
                model.CodUsuarioCadastro = await _loginAuthenticationService.CodUsuarioLogadoAsync();
                var usuCriado = await _loginAuthenticationService.AdicionarAsync(model);
                var msg = "Usuário cadastrado com sucesso!";
                this.AlertaMsgRedirect(msg, Url.Action("Configuracoes", "Usuario", new { CodUsuario = usuCriado.Codigo }) ?? string.Empty);

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
    /// Método usado principalmente em aplicação .NET MVC com foco em criar um modelo para edição de usuário
    /// </summary>
    /// <param name="codUsuario"></param>
    /// <returns></returns>
    public async Task<IActionResult> Editar(int codUsuario)
    {
        try
        {
            var tiposUsuario = await _tipousuarioService.ListarTodosSemMasterAsync();
            ViewData["TipoUsuario"] = new SelectList(tiposUsuario.ListaTiposUsuario, "Codigo", "Descricao");

            var usuarioEdicao = await _usuarioService.RetornaParaEdicaoAsync(new UsuarioRetornaParaEdicaoInModel
            {
                CodUsuario = codUsuario,
                CodUsuarioAlteracao = await _loginAuthenticationService.CodUsuarioLogadoAsync()
            });
            var usuarioEditarInModel = _mapper.Map<UsuarioEditarInModel>(usuarioEdicao);

            usuarioEditarInModel.CodUsuarioAcao = await _loginAuthenticationService.CodUsuarioLogadoAsync();

            return View(usuarioEditarInModel);
        }
        catch (Exception ex)
        {
            this.AlertaMsgErroRedirect(ex.Message, Url.Action("index", "Usuario", new { Area = "Administrativo" }) ?? string.Empty);
            return View(new UsuarioEditarInModel());
        }
    }

    /// <summary>
    /// Edita um usuário existente
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Editar(UsuarioEditarInModel model)
    {
        try
        {
            var tiposUsuario = await _tipousuarioService.ListarTodosSemMasterAsync();
            ViewData["TipoUsuario"] = new SelectList(tiposUsuario.ListaTiposUsuario, "Codigo", "Descricao");

            if (ModelState.IsValid)
            {
                model.CodUsuarioAcao = await _loginAuthenticationService.CodUsuarioLogadoAsync();
                var usuEditado = await _usuarioService.EditarAsync(model);
                this.AlertaMsgRedirect("Alterações realizadas com sucesso!", Url.Action("Configuracoes", new { codUsuario = usuEditado.Codigo }) ?? string.Empty);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
        }

        return View(model);
    }

    /// <summary>
    /// Reativa um usuário para utilização do sistema, desta forma o usuário poderá realizar login novamente na plataforma
    /// </summary>
    /// <param name="codUsuario"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> Afastar(int codUsuario)
    {
        try
        {
            var codUsuarioLogado = await _loginAuthenticationService.CodUsuarioLogadoAsync();
            await _usuarioService.AfastarAsync(new UsuarioAfastarInModel
            {
                CodUsuario = codUsuario,
                CodUsuarioResponsavel = codUsuarioLogado
            });

            return Json(true);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }

    }

    /// <summary>
    /// Reativa um usuário para utilização do sistema, desta forma o usuário poderá realizar login novamente na plataforma
    /// </summary>
    /// <param name="codUsuario"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<JsonResult> Ativar(int codUsuario)
    {
        try
        {
            var codUsuarioLogado = await _loginAuthenticationService.CodUsuarioLogadoAsync();
            await _usuarioService.ReativarAsync(new UsuarioReativarInModel
            {
                CodUsuario = codUsuario,
                CodUsuarioResponsavel = codUsuarioLogado
            });

            return Json(true);
        }
        catch (Exception ex)
        {
            return Json(new SimpleError(ex));
        }

    }

    /// <summary>
    /// Método utilizado para realizar a autopesquisa de campo
    /// </summary>
    /// <param name="descricao"></param>
    /// <param name="codigoRef"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    public async Task<JsonResult> PesquisaUsuarioEmail(string descricao, string codigoRef)
    {
        try
        {
            descricao = descricao.ToTrimLower();

            var dados = (await _usuarioService.ListarAsync(new UsuarioListarInModel { Codigo = await _loginAuthenticationService.CodUsuarioLogadoAsync() }))
                .Usuarios.Where(o => o.Email.ToLower() == descricao)
                .Select(o => new { o.Codigo, Descricao = o.Nome, Url = Url.Action("Configuracoes", "Usuario", new { Area = "Administrativo", codUsuario = o.Codigo }) });
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
    /// Método utilizado para realizar a autopesquisa de campo
    /// </summary>
    /// <param name="descricao"></param>
    /// <param name="codigoRef"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpPost]
    public async Task<JsonResult> PesquisaUsuarioNome(string descricao, string codigoRef)
    {
        try
        {
            descricao = descricao.ToTrimUpper();

            var dados = (await _usuarioService.ListarAsync(new UsuarioListarInModel
            {
                Codigo = await _loginAuthenticationService.CodUsuarioLogadoAsync()
            })).Usuarios.Where(o => o.Nome.ToUpper() == descricao).Select(o => new { o.Codigo, Descricao = o.Nome, Url = Url.Action("Configuracoes", "Usuario", new { Area = "Administrativo", codUsuario = o.Codigo }) });

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
    /// Patial WindowAvisoAfastamento
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult WindowAvisoAfastamento()
    {
        return PartialView("~/Areas/Administrativo/Views/Usuario/Partial/WindowAvisoAfastamento.cshtml");
    }
    /// <summary>
    /// Partial WindowAvisoAtivacao
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult WindowAvisoAtivacao()
    {
        return PartialView("~/Areas/Administrativo/Views/Usuario/Partial/WindowAvisoAtivacao.cshtml");
    }
}