using Domain.DomainServicesInterfaces;
using Domain.Dtos.LogGenerico.Output;
using Domain.Dtos.LogGenerico.Input;
using Domain.Dtos.Permissao.Input;
using Domain.Dtos.RegraSistema.ListaRegraAusente.Input;
using Domain.Dtos.RegraSistema.ListarRegraExedente.Input;
using Domain.Dtos.RegraSistema.UsuarioRegraCustomizada.Input;
using Domain.Dtos.TipoUsuario.Criar.Input;
using Domain.Dtos.TipoUsuario.Criar.Output;
using Domain.Dtos.TipoUsuario.Editar.Input;
using Domain.Dtos.TipoUsuario.Listar.Output;
using Domain.Dtos.TipoUsuario.ListarLog.Input;
using Domain.Dtos.TipoUsuario.Retorna.Input;
using Domain.Dtos.TipoUsuario.Visualizar.Input;
using Domain.Dtos.TipoUsuario.Visualizar.Output;
using Domain.Dtos.Usuario.ListarPorTipoUsuario.Input;
using Domain.Dtos.Usuario.ListarPorTipoUsuario.Output;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Exception;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class TipoUsuarioService : ITipoUsuarioService
{
    private readonly IPermissaoService _permissaoService;
    private readonly IRegraSistemaService _regraSistemaService;
    private readonly ILogGenericoService _logGenericoService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITipoUsuarioCheckerService _tipoUsuarioCheckerService;
    private readonly IExceptions _exceptions;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="permissaoService"></param>
    /// <param name="regraSistemaService"></param>
    /// <param name="logGenericoService"></param>
    /// <param name="unitOfWork"></param>
    /// <param name="tipoUsuarioCheckerService"></param>
    /// <param name="exceptions"></param>
    public TipoUsuarioService(
        IPermissaoService permissaoService,
        IRegraSistemaService regraSistemaService,
        ILogGenericoService logGenericoService,
        IUnitOfWork unitOfWork,
        ITipoUsuarioCheckerService tipoUsuarioCheckerService,
        IExceptions exceptions
    )
    {
        _permissaoService = permissaoService;
        _regraSistemaService = regraSistemaService;
        _logGenericoService = logGenericoService;
        _unitOfWork = unitOfWork;
        _tipoUsuarioCheckerService = tipoUsuarioCheckerService;
        _exceptions = exceptions;
    }

    /// <inheritdoc />
    public async Task<UsuarioListarUsuariosPorTipoOutModel> ListarUsuariosPorTipoAsync(UsuarioListarUsuariosPorTipoInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        var tipoUsu = await _tipoUsuarioCheckerService.RetornarAsync(new TipoUsuarioTipoUsuarioRetornarInModel { Codigo = model.CodTipoUsuario });

        var dado = await _unitOfWork.UsuarioRepository.FindAsync(o => o.CodTipoUsuario == tipoUsu.Codigo);

        var ret = new UsuarioListarUsuariosPorTipoOutModel
        {
            ListaUsuario = dado.Select(o =>
                new UsuarioListarPorTipoUsuarioItemOutModel
                {
                    Codigo = o.Codigo,
                    Usuario = $"{o.Codigo} - {o.Nome}",
                    Ativo = !o.DataAfastamento.HasValue
                }).ToList()
        };

        foreach (var obj in ret.ListaUsuario)
        {
            obj.RegraCustomizada = await _regraSistemaService.RegrasSistemaCustomizadasUsuario(
                new RegrasSistemaCustomizadasUsuarioInModel
                {
                    CodUsuario = obj.Codigo
                });

            obj.RegrasExedentes = (await _regraSistemaService.RegrasSistemaExcedenteAsync(
                new RegraSistemaRegrasSistemaExcedenteInModel
                {
                    CodUsuario = obj.Codigo
                })).ListaRegras.Select(o => $"{o.Codigo} - {o.RegraSistemaDescricao}").ToList();

            obj.RegrasAusentes = (await _regraSistemaService.ListaRegrasSistemaAusentesAsync(
                new RegraSistemaListaRegrasSistemaAusentesInModel
                {
                    CodUsuario = obj.Codigo
                })).ListaRegras.Select(o => $"{o.Codigo} - {o.RegraSistemaDescricao}").ToList();
        }

        return ret;
    }

    /// <inheritdoc />
    public async Task<LogGenericoListarLogOutModel> ListarLogAsync(TipoUsuarioListarLogInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        var dados = await _logGenericoService.ListarAsync(new LogGenericoListarInModel
        {
            Referencia = model.CodTipoUsuario.ToString(),
            Tabela = "TipoUsuario"
        });

        return dados;
    }

    /// <inheritdoc />
    public async Task<TipoUsuarioVisualizarOutModel> VisualizarAsync(TipoUsuarioVisualizarInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        var permiteAcessoLog = await _permissaoService.AvaliarNivelAsync(new PermissaoAvaliarNivelInModel
        {
            CodRegraSistema = TipoRegraSistema.LogTipoUsuario.GetIntValue(),
            CodUsuario = model.CodUsuarioConsulta
        });

        var tipoUsuario = await _tipoUsuarioCheckerService.RetornarAsync(new TipoUsuarioTipoUsuarioRetornarInModel { Codigo = model.Codigo });

        var tipoUsuarioVisualizarOutModel = new TipoUsuarioVisualizarOutModel
        {
            Codigo = tipoUsuario.Codigo,
            Descricao = tipoUsuario.Descricao,
            Editavel = !ListaTiposUsuarioEdicaoBloqueada().Contains(tipoUsuario.Codigo),
            PermiteLog = permiteAcessoLog
        };

        if (tipoUsuarioVisualizarOutModel.PermiteLog)
            tipoUsuarioVisualizarOutModel.Log = await ListarLogAsync(new TipoUsuarioListarLogInModel { CodTipoUsuario = tipoUsuario.Codigo, CodUsuarioSolicitacaoLog = model.CodUsuarioConsulta });

        return tipoUsuarioVisualizarOutModel;
    }

    /// <inheritdoc />
    public async Task<TipoUsuarioListarOutModel> ListarAsync()
    {
        var dados = await _unitOfWork.TipoUsuarioRepository.AllAsync();
        var tiposUsuario = new TipoUsuarioListarOutModel
        {
            ListaTiposUsuario = dados.Select(t => new TipoUsuarioItemOutModel(t)).ToList()
        };
        return tiposUsuario;
    }

    /// <inheritdoc />
    public async Task<TipoUsuarioListarOutModel> ListarTodosSemMasterAsync()
    {
        var dados = await _unitOfWork.TipoUsuarioRepository.AllAsync();
        var tiposUsuario = new TipoUsuarioListarOutModel
        {
            ListaTiposUsuario = dados.Where(o => o.Codigo != TipoUsuarioEdicaoBloqueada.Master.GetIntValue())
                .Select(t => new TipoUsuarioItemOutModel(t)).ToList()
        };
        return tiposUsuario;
    }

    /// <inheritdoc />
    public async Task<TipoUsuarioAdicionarOutModel> AdicionarAsync(TipoUsuarioAdicionarInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        var dado = await _unitOfWork.TipoUsuarioRepository.FirstAsync(o => o.Descricao == model.Descricao);//_db.TipoUsuario.FirstOrDefaultAsync(o => o.Descricao == model.Descricao);
        if (dado != null)
        {
            throw new Exception(_exceptions.JaExisteUmTipoDeUsuarioCadastradoComEstaDescricao);
        }

        var newTipousuario = new TipoUsuario
        {
            Descricao = model.Descricao
        };

        await _unitOfWork.TipoUsuarioRepository.AddAsync(newTipousuario);
        await _unitOfWork.SaveAsync();
        return new TipoUsuarioAdicionarOutModel(newTipousuario);

    }

    /// <inheritdoc />
    public int[] ListaTiposUsuarioEdicaoBloqueada()
    {
        return EnumOperations.ToIntArray<TipoUsuarioEdicaoBloqueada>();
    }

    /// <inheritdoc />
    public async Task EditarTipoUsuarioAsync(TipoUsuarioEditarTipoUsuarioInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        model.Descricao = model.Descricao.Trim();
        var tipoUsuario = await _tipoUsuarioCheckerService.RetornarAsync(new TipoUsuarioTipoUsuarioRetornarInModel { Codigo = model.Codigo });

        var listaBloqueados = ListaTiposUsuarioEdicaoBloqueada();
        if (listaBloqueados.Contains(model.Codigo))
        {
            throw new Exception(_exceptions.NaoEPermitidoAlterarEsteTipoDeUsuario);
        }

        var dadoTipousuarioMesmaDescricao = await _unitOfWork.TipoUsuarioRepository.FirstAsync(o => o.Codigo != tipoUsuario.Codigo && o.Descricao == model.Descricao);
        if (dadoTipousuarioMesmaDescricao != null)
        {
            throw new Exception(_exceptions.JaExisteUmTipoDeUsuarioCadastradoComEstaDescricao);
        }

        var tipoUsuarioAntesAlteracao = await _unitOfWork.TipoUsuarioRepository.FirstAsync(o => o.Codigo == tipoUsuario.Codigo, true);
        var tipoUsuarioAlterado = await _unitOfWork.TipoUsuarioRepository.FirstAsync(o => o.Codigo == tipoUsuario.Codigo);
        if (tipoUsuarioAlterado == null)
            throw new Exception(_exceptions.ErroDesconhecido);

        tipoUsuarioAlterado.Descricao = model.Descricao;
        _unitOfWork.TipoUsuarioRepository.Update(tipoUsuarioAlterado);

        await _unitOfWork.SaveAsync();
        await _logGenericoService.AdicionarAsync(
            tipoUsuarioAntesAlteracao!,
            tipoUsuarioAlterado,
            tipoUsuarioAlterado.Codigo.ToString(),
            model.CodUsuarioAlteracao);


    }

    /// <inheritdoc />
    public async Task CriarTiposUsuariosBasicosAsync()
    {
        if (await _unitOfWork.TipoUsuarioRepository.AnyAsync())
            return;

        var newTipoUsuarioMaster = new TipoUsuario
        {
            Descricao = "Master"
        };

        var newTipoUsuarioGerente = new TipoUsuario
        {
            Descricao = "Gerente"
        };

        await _unitOfWork.TipoUsuarioRepository.AddAsync(newTipoUsuarioMaster);
        await _unitOfWork.TipoUsuarioRepository.AddAsync(newTipoUsuarioGerente);

        await _unitOfWork.SaveAsync();
    }

    /// <summary>
    /// Enum com os tipos de usuários/perfís que não podem ter alterações
    /// </summary>
    public enum TipoUsuarioEdicaoBloqueada
    {
        /// <summary>
        /// Tipo master
        /// </summary>
        Master = 1,
        /// <summary>
        /// tipo gerente
        /// </summary>
        Gerente = 2
    }
}