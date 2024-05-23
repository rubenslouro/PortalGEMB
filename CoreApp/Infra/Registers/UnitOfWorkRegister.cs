using Domain.Entities;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Repository.AdvancedRepository;
using InfraDatabase.Repository;
using InfraDatabase.Repository.AdvancedRepository;
using Microsoft.Extensions.DependencyInjection;

namespace InfraDatabase.Registers;

/// <summary>
/// 
/// </summary>
public static class UnitOfWorRegister
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddTransient<IRepository<ConfiguracaoGeral>, Repository<ConfiguracaoGeral>>();
        services.AddTransient<IRepository<Usuario>, Repository<Usuario>>();
        services.AddTransient<IRepository<LogGenerico>, Repository<LogGenerico>>();
        services.AddTransient<IRepository<LogUsuarioRegraSistema>, Repository<LogUsuarioRegraSistema>>();
        services.AddTransient<IRepository<RegraSistema>, Repository<RegraSistema>>();
        services.AddTransient<IRepository<TipoUsuario>, Repository<TipoUsuario>>();
        services.AddTransient<IRepository<TipoUsuarioRegraSistema>, Repository<TipoUsuarioRegraSistema>>();
        services.AddTransient<IRepository<UsuarioRegraSistema>, Repository<UsuarioRegraSistema>>();
        services.AddTransient<IRepository<Uf>, Repository<Uf>>();
        services.AddTransient<IRepositoryCreation, RepositoryCreation>();

        services.AddTransient<IAssistidoRepository, AssistidoRepository>();
        services.AddTransient<IAtendimentoRepository, AtendimentoRepository>();
        services.AddTransient<IRepository<Disciplina>, Repository<Disciplina>>();
        services.AddTransient<IRepository<TurmaAluno>, Repository<TurmaAluno>>();
        services.AddTransient<IRepository<Turma>, Repository<Turma>>();
        services.AddTransient<IRepository<Atendimento_TipoAtendimento>, Repository<Atendimento_TipoAtendimento>>();
        services.AddTransient<IRepository<TipoAtendimento>, Repository<TipoAtendimento>>();
        services.AddTransient<IRepository<TipoAtividadeRemunerada>, Repository<TipoAtividadeRemunerada>>();
        services.AddTransient<IRepository<TipoDependente>, Repository<TipoDependente>>();
        services.AddTransient<IRepository<TipoEscolaridade>, Repository<TipoEscolaridade>>();
        services.AddTransient<IRepository<TipoEstadoCivil>, Repository<TipoEstadoCivil>>();
        services.AddTransient<IRepository<TipoMoradia>, Repository<TipoMoradia>>();

        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }
}