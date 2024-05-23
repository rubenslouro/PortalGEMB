using Domain.DomainServicesInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationServices.Registers;

/// <summary>
/// Injetor do ManagerCore
/// </summary>
internal static class DomainServicesRegister
{
    /// <summary>
    /// método que injeta o ManagerCore
    /// </summary>
    internal static void AddDomainServices(this IServiceCollection service)
    {
        service.AddScoped<IApplicationInstallService, ApplicationInstallService>();
        service.AddScoped<ITipoUsuarioCheckerService, TipoUsuarioCheckerService>();
        service.AddScoped<IUsuarioCheckerService, UsuarioCheckerService>();
        service.AddScoped<ICriptografiaService, CritografiaService>();
        service.AddScoped<IConfiguracaoGeralService, ConfiguracaoGeralService>();
        service.AddScoped<IPermissaoService, PermissaoService>();
        service.AddScoped<IRegraSistemaService, RegraSistemaService>();
        service.AddScoped<ITipoUsuarioService, TipoUsuarioService>();
        service.AddScoped<IUsuarioService, UsuarioService>();
        service.AddScoped<ILogGenericoService, LogGenericoService>();
        service.AddScoped<IDatabaseService, DatabaseService>();
        service.AddScoped<IUfService, UfService>();

        service.AddScoped<ITurmaAlunoService, TurmaAlunoService>();
        service.AddScoped<IAssistidoService, AssistidoService>();
        service.AddScoped<IAtendimentoService, AtendimentoService>();
        service.AddScoped<IAtendimento_TipoAtendimentoService, Atendimento_TipoAtendimentoService>();
        service.AddScoped<IDisciplinaService, DisciplinaService>();
        service.AddScoped<ITurmaService, TurmaService>();
        service.AddScoped<ITipoAtendimentoService, TipoAtendimentoService>();
        service.AddScoped<ITipoAtividadeRemuneradaService, TipoAtividadeRemuneradaService>();
        service.AddScoped<ITipoDependenteService, TipoDependenteService>();
        service.AddScoped<ITipoEscolaridadeService, TipoEscolaridadeService>();
        service.AddScoped<ITipoEstadoCivilService, TipoEstadoCivilService>();
        service.AddScoped<ITipoMoradiaService, TipoMoradiaService>();
    }
}