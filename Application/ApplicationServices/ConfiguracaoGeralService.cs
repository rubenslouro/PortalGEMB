using Domain.DomainServicesInterfaces;
using Domain.Dtos.ConfiguracaoGeral.Input;
using Domain.Dtos.ConfiguracaoGeral.Output;
using Domain.Dtos.LogGenerico.Output;
using Domain.Dtos.LogGenerico.Input;
using Domain.Dtos.Permissao.Input;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Exception;
using Domain.Interfaces.Repository;
using UtilService.Util;

namespace ApplicationServices;

/// <inheritdoc />
public class ConfiguracaoGeralService : IConfiguracaoGeralService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogGenericoService _logGenericoService;
    private readonly IPermissaoService _permissaoService;
    private readonly IExceptions _exception;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="unitOfWork"></param>
    /// <param name="logGenericoService"></param>
    /// <param name="permissaoService"></param>
    /// <param name="exception"></param>
    public ConfiguracaoGeralService(
        IUnitOfWork unitOfWork,
        ILogGenericoService logGenericoService,
        IPermissaoService permissaoService,
        IExceptions exception)
    {
        _unitOfWork = unitOfWork;
        _logGenericoService = logGenericoService;
        _permissaoService = permissaoService;
        _exception = exception;
    }

    /// <inheritdoc />
    /// Método não testável
    public async Task<LogGenericoListarLogOutModel> ListarLogAsync(ConfiguracaoGeralListarLogInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();

        var dados = await _logGenericoService.ListarAsync(new LogGenericoListarInModel
        {
            Referencia = "1",//Sempre haverá um registro, pode ficar tranquilo. Se houver mais de um é pq está errado.
            Tabela = "ConfiguracaoGeral"
        });

        return dados;
    }

    /// <inheritdoc />
    public async Task<ConfiguracaoGeralRetornaOutModel?> RetornarAsync()
    {
        var dado = await _unitOfWork.ConfiguracaoGeralRepository.FirstAsync();
        if (dado == null)
            return null;

        return new ConfiguracaoGeralRetornaOutModel(dado);
    }

    /// <inheritdoc />
    public async Task<ConfiguracaoGeralRetornaDetalhadoOutModel?> RetornaDetalhadoAsync(ConfiguracaoGeralRetornaDetalhadoInModel model)
    {
        model.CheckIfModelIsValid();
        var dado = await _unitOfWork.ConfiguracaoGeralRepository.FirstAsync();

        if (dado == null) return null;

        var permiteLog = await _permissaoService.AvaliarNivelAsync(new PermissaoAvaliarNivelInModel
        {
            CodRegraSistema = TipoRegraSistema.LogConfiguracaoGeral.GetIntValue(),
            CodUsuario = model.CodUsuarioSolicitacao
        });
        var log = permiteLog ? await ListarLogAsync(new ConfiguracaoGeralListarLogInModel { CodUsuarioSolicitacaoLog = model.CodUsuarioSolicitacao }) : null;

        return new ConfiguracaoGeralRetornaDetalhadoOutModel(dado, permiteLog, log);
    }

    /// <inheritdoc />
    public async Task<ConfiguracaoGeralEditarConfiguracaoOutModel> EditarConfiguracaoAsync(ConfiguracaoGeralEditarConfiguracaoInModel model)
    {
        model.TrimStringProperties();
        model.CheckIfModelIsValid();
        if (!await _unitOfWork.ConfiguracaoGeralRepository.AnyAsync())
        {
            throw new Exception(_exception.ConfiguracaoGeralNaoEncontradaNoBancoDeDados);
        }

        model.UrlSite = model.UrlSite.ToTrimLower();

        var dadoAntesAlteracao = await _unitOfWork.ConfiguracaoGeralRepository.FirstAsync(true);

        if (dadoAntesAlteracao == null)
            throw new Exception("Não foram encontrados dados de configuração geral.");

        var dadoAlterado = await _unitOfWork.ConfiguracaoGeralRepository.FirstAsync();

        dadoAlterado!.UrlSite = model.UrlSite;

        await _unitOfWork.SaveAsync();

        await _logGenericoService.AdicionarAsync(
            dadoAntesAlteracao,
            dadoAlterado,
            dadoAlterado.Codigo.ToString(),
            model.CodUsuarioAcao);

        var dadoRet = new ConfiguracaoGeralEditarConfiguracaoOutModel(dadoAlterado);

        return dadoRet;
    }

    /// <inheritdoc />
    public async Task IncluirPrimeiraConfiguracaoSistemaAsync(ConfiguracaoGeral configuracaoGeral)
    {
        if (configuracaoGeral == null)
            throw new Exception(_exception.ConfiguracaoGeralNaoEncontradaNoBancoDeDados);

        await _unitOfWork.ConfiguracaoGeralRepository.AddAsync(configuracaoGeral);
        await _unitOfWork.SaveAsync();

    }

}