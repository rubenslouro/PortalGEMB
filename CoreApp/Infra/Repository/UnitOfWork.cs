using Domain.DomainServicesInterfaces;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Repository.AdvancedRepository;

namespace InfraDatabase.Repository;

/// <inheritdoc />
public class UnitOfWork : IUnitOfWork
{
    private readonly CalangoContext _db;

    /// <summary>
    /// Contrutor
    /// </summary>
    /// <param name="db"></param>
    /// <param name="configuracaoGeralRepository"></param>
    /// <param name="logGenericoRepository"></param>
    /// <param name="logUsuarioRegraSistemaRepository"></param>
    /// <param name="regraSistemaRepository"></param>
    /// <param name="tipoUsuarioRepository"></param>
    /// <param name="tipoUsuarioRegraSistemaRepository"></param>
    /// <param name="usuarioRepository"></param>
    /// <param name="usuarioRegraSistemaRepository"></param>
    /// <param name="ufRepository"></param>
    /// <param name="tipoatendimentoRepository"></param>
    /// <param name="tipoatividaderemuneradaRepository"></param>
    /// <param name="tipodependenteRepository"></param>
    /// <param name="tipoescolaridadeRepository"></param>
    /// <param name="tipoestadocivilRepository"></param>
    /// <param name="tipomoradiaRepository"></param>
    /// <param name="assistidoRepository"></param>
    /// <param name="atendimentoRepository"></param>
    /// <param name="atendimento_tipoatendimentoRepository"></param>
    /// <param name="disciplinaRepository"></param>
    /// <param name="turmaalunoRepository"></param>
    /// <param name="turmaRepository"></param>
    public UnitOfWork(
        CalangoContext db,
        IRepository<ConfiguracaoGeral> configuracaoGeralRepository,
        IRepository<LogGenerico> logGenericoRepository,
        IRepository<LogUsuarioRegraSistema> logUsuarioRegraSistemaRepository,
        IRepository<RegraSistema> regraSistemaRepository,
        IRepository<TipoUsuario> tipoUsuarioRepository,
        IRepository<TipoUsuarioRegraSistema> tipoUsuarioRegraSistemaRepository,
        IRepository<Usuario> usuarioRepository,
        IRepository<UsuarioRegraSistema> usuarioRegraSistemaRepository, 
        IRepository<Uf> ufRepository,
        IRepository<TipoAtendimento> tipoatendimentoRepository,
        IRepository<TipoAtividadeRemunerada> tipoatividaderemuneradaRepository,
        IRepository<TipoDependente> tipodependenteRepository,
        IRepository<TipoEscolaridade> tipoescolaridadeRepository,
        IRepository<TipoEstadoCivil> tipoestadocivilRepository,
        IRepository<TipoMoradia> tipomoradiaRepository,
        IAssistidoRepository assistidoRepository,
        IAtendimentoRepository atendimentoRepository,
        IRepository<Atendimento_TipoAtendimento> atendimento_tipoatendimentoRepository,
        IRepository<Disciplina> disciplinaRepository,
        IRepository<TurmaAluno> turmaalunoRepository,
        IRepository<Turma> turmaRepository)
    {
        _db = db;
        ConfiguracaoGeralRepository = configuracaoGeralRepository;
        LogGenericoRepository = logGenericoRepository;
        LogUsuarioRegraSistemaRepository = logUsuarioRegraSistemaRepository;
        RegraSistemaRepository = regraSistemaRepository;
        TipoUsuarioRepository = tipoUsuarioRepository;
        TipoUsuarioRegraSistemaRepository = tipoUsuarioRegraSistemaRepository;
        UsuarioRepository = usuarioRepository;
        UsuarioRegraSistemaRepository = usuarioRegraSistemaRepository;
        UfRepository = ufRepository;
        TipoAtendimentoRepository = tipoatendimentoRepository;
        TipoAtividadeRemuneradaRepository = tipoatividaderemuneradaRepository;
        TipoDependenteRepository = tipodependenteRepository;
        TipoEscolaridadeRepository = tipoescolaridadeRepository;
        TipoEstadoCivilRepository = tipoestadocivilRepository;
        TipoMoradiaRepository = tipomoradiaRepository;
        AssistidoRepository = assistidoRepository;
        AtendimentoRepository = atendimentoRepository;
        Atendimento_TipoAtendimentoRepository = atendimento_tipoatendimentoRepository;
        DisciplinaRepository = disciplinaRepository;
        TurmaAlunoRepository = turmaalunoRepository;
        TurmaRepository = turmaRepository;
    }

    /// <inheritdoc />
    public IRepository<ConfiguracaoGeral> ConfiguracaoGeralRepository { get; }

    /// <inheritdoc />
    public IRepository<LogGenerico> LogGenericoRepository { get; }

    /// <inheritdoc />
    public IRepository<LogUsuarioRegraSistema> LogUsuarioRegraSistemaRepository { get; }

    /// <inheritdoc />
    public IRepository<RegraSistema> RegraSistemaRepository { get; }

    /// <inheritdoc />
    public IRepository<TipoUsuario> TipoUsuarioRepository { get; }

    /// <inheritdoc />
    public IRepository<TipoUsuarioRegraSistema> TipoUsuarioRegraSistemaRepository { get; }

    /// <inheritdoc />
    public IRepository<Usuario> UsuarioRepository { get; }

    /// <inheritdoc />
    public IRepository<UsuarioRegraSistema> UsuarioRegraSistemaRepository { get; }

    /// <inheritdoc />
    public IRepository<Uf> UfRepository { get; }

    /// <inheritdoc />
    public IRepository<TipoAtendimento> TipoAtendimentoRepository { get; }

    /// <inheritdoc />
    public IRepository<TipoAtividadeRemunerada> TipoAtividadeRemuneradaRepository { get; }

    /// <inheritdoc />
    public IRepository<TipoDependente> TipoDependenteRepository { get; }

    /// <inheritdoc />
    public IRepository<TipoEscolaridade> TipoEscolaridadeRepository { get; }

    /// <inheritdoc />
    public IRepository<TipoEstadoCivil> TipoEstadoCivilRepository { get; }

    /// <inheritdoc />
    public IRepository<TipoMoradia> TipoMoradiaRepository { get; }

    /// <inheritdoc />
    public IAssistidoRepository AssistidoRepository { get; }

    /// <inheritdoc />
    public IAtendimentoRepository AtendimentoRepository { get; }

    /// <inheritdoc />
    public IRepository<Atendimento_TipoAtendimento> Atendimento_TipoAtendimentoRepository { get; }

    /// <inheritdoc />
    public IRepository<Disciplina> DisciplinaRepository { get; }

    /// <inheritdoc />
    public IRepository<TurmaAluno> TurmaAlunoRepository { get; }

    /// <inheritdoc />
    public IRepository<Turma> TurmaRepository { get; }

    /// <inheritdoc />
    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _db.Dispose();
        }
    }

    /// <inheritdoc />
    public void EnableLazyLoader(bool enableLazyLoader)
    {
        _db.ChangeTracker.LazyLoadingEnabled = enableLazyLoader;
    }
}