using Domain.DomainServicesInterfaces;
using Domain.Dtos.ConfiguracaoGeral.Input;
using Domain.Entities;
using Domain.Interfaces.Exception;
using UtilService.Util;

namespace ApplicationServices;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class ApplicationInstallService : IApplicationInstallService
{
    private readonly IConfiguracaoGeralService _configuracaoGeralService;
    private readonly IExceptions _exceptions;
    private readonly IUsuarioService _usuarioService;
    private readonly ITipoUsuarioService _tipoUsuarioService;
    private readonly IRegraSistemaService _regraSistemaService;
    private readonly ICriptografiaService _critografiaService;
    private readonly IUfService _ufService;
    private readonly IDisciplinaService _disciplinaService;
    private readonly ITurmaService _turmaService;
    private readonly ITipoAtendimentoService _tipoAtendimentoService;
    private readonly ITipoAtividadeRemuneradaService _tipoAtividadeRemuneradaService;
    private readonly ITipoDependenteService _tipoDependenteService;
    private readonly ITipoEscolaridadeService _tipoEscolaridadeService;
    private readonly ITipoEstadoCivilService _tipoEstadoCivilService;
    private readonly ITipoMoradiaService _tipoMoradiaService;

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="configuracaoGeralService"></param>
    /// <param name="exceptions"></param>
    /// <param name="usuarioService"></param>
    /// <param name="tipoUsuarioService"></param>
    /// <param name="regraSistemaService"></param>
    /// <param name="critografiaService"></param>
    /// <param name="ufService"></param>
    /// <param name="disciplinaService"></param>
    /// <param name="turmaService"></param>
    /// <param name="tipoAtendimentoService"></param>
    /// <param name="tipoAtividadeRemuneradaService"></param>
    /// <param name="tipoDependenteService"></param>
    /// <param name="tipoEscolaridadeService"></param>
    /// <param name="tipoEstadoCivilService"></param>
    /// <param name="tipoMoradiaService"></param>
    public ApplicationInstallService(
        IConfiguracaoGeralService configuracaoGeralService,
        IExceptions exceptions,
        IUsuarioService usuarioService,
        ITipoUsuarioService tipoUsuarioService,
        IRegraSistemaService regraSistemaService,
        ICriptografiaService critografiaService,
        IUfService ufService,
        IDisciplinaService disciplinaService,
        ITurmaService turmaService,
        ITipoAtendimentoService tipoAtendimentoService,
        ITipoAtividadeRemuneradaService tipoAtividadeRemuneradaService,
        ITipoDependenteService tipoDependenteService,
        ITipoEscolaridadeService tipoEscolaridadeService,
        ITipoEstadoCivilService tipoEstadoCivilService,
        ITipoMoradiaService tipoMoradiaService
    )
    {
        _configuracaoGeralService = configuracaoGeralService;
        _exceptions = exceptions;
        _usuarioService = usuarioService;
        _tipoUsuarioService = tipoUsuarioService;
        _regraSistemaService = regraSistemaService;
        _critografiaService = critografiaService;
        _ufService = ufService;
        _disciplinaService = disciplinaService;
        _turmaService = turmaService;
        _tipoAtendimentoService = tipoAtendimentoService;
        _tipoAtividadeRemuneradaService = tipoAtividadeRemuneradaService;
        _tipoDependenteService = tipoDependenteService;
        _tipoEscolaridadeService = tipoEscolaridadeService;
        _tipoEstadoCivilService = tipoEstadoCivilService;
        _tipoMoradiaService = tipoMoradiaService;
    }

    /// <inheritdoc />
    public async Task<bool> InstalarConfiguracaoAsync(ConfiguracaoGeralInstalarConfiguracaoInModel model)
    {
        model.CheckIfModelIsValid();
        var config = await _configuracaoGeralService.RetornarAsync();

        if (config is not null)
        {
            throw new Exception(_exceptions.ATabelaConfiguiracaoGeralJaEstaAlimentada);
        }

        model.UrlSite = model.UrlSite.ToTrimLower();

        var configuracaoGeral = new ConfiguracaoGeral
        {
            UrlSite = model.UrlSite
        };

        await _configuracaoGeralService.IncluirPrimeiraConfiguracaoSistemaAsync(configuracaoGeral);
        
        await AdicionarEstruturaUsuariosAsync(model);

        await _ufService.AdicionarUfAsync();
        await _disciplinaService.AdicionarDisciplinaAsync();
        await _tipoAtendimentoService.AdicionarTipoAtendimentoAsync();
        await _tipoAtividadeRemuneradaService.AdicionarTipoAtividadeRemuneradaAsync();
        await _tipoDependenteService.AdicionarTipoDependenteAsync();
        await _tipoEscolaridadeService.AdicionarTipoEscolaridadeAsync();
        await _tipoEstadoCivilService.AdicionarTipoEstadoCivilAsync();
        await _tipoMoradiaService.AdicionarTipoMoradiaAsync();

        return true;
    }

    #region Métodos privados

    private async Task AdicionarEstruturaUsuariosAsync(ConfiguracaoGeralInstalarConfiguracaoInModel model)
    {
        model.CheckIfModelIsValid();

        if (!await _usuarioService.ExisteUsuarioCadastradoAsync())
        {
            await _tipoUsuarioService.CriarTiposUsuariosBasicosAsync();
            await _regraSistemaService.InstalarRegrasSistemaAsync();

            model.EmailUsuarioMaster = model.EmailUsuarioMaster.ToLower();
            model.SenhaMaster = _critografiaService.EncryptString(model.SenhaMaster);

            var newUsuario = new Usuario
            {
                Nome = "Usuário Master",
                Email = model.EmailUsuarioMaster,
                CodUsuarioCadastro = null,
                DataCadastro = DateTime.UtcNow,
                DataAfastamento = null,
                Senha = model.SenhaMaster,
                CodTipoUsuario = TipoUsuarioService.TipoUsuarioEdicaoBloqueada.Master.GetIntValue()
            };

            await _usuarioService.CriarUsuarioMasterAsync(newUsuario);
        }
    }

    #endregion Métodos privados
}