using Domain.Entities;
using Domain.Interfaces.Repository.AdvancedRepository;

namespace Domain.Interfaces.Repository;

/// <inheritdoc />
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Repositório de configuração geral do sistema
    /// </summary>
    IRepository<ConfiguracaoGeral> ConfiguracaoGeralRepository
    {
        get;
    }
    
    /// <summary>
    /// Repositório de log de alterações gerais do sistema
    /// </summary>
    IRepository<LogGenerico> LogGenericoRepository
    {
        get;
    }
    
    /// <summary>
    /// Log de alterações de regras de sistema
    /// </summary>
    IRepository<LogUsuarioRegraSistema> LogUsuarioRegraSistemaRepository
    {
        get;
    }
    
    /// <summary>
    /// Repositório de regras de sistema
    /// </summary>
    IRepository<RegraSistema> RegraSistemaRepository
    {
        get;
    }
    
    /// <summary>
    /// Repositório de tipos de usuário
    /// </summary>
    IRepository<TipoUsuario> TipoUsuarioRepository
    {
        get;
    }
    
    /// <summary>
    /// Tepositório de tipo usuário X regra de sistema
    /// </summary>
    IRepository<TipoUsuarioRegraSistema> TipoUsuarioRegraSistemaRepository
    {
        get;
    }
    
    /// <summary>
    /// Repositório de usuários
    /// </summary>
    IRepository<Usuario> UsuarioRepository
    {
        get;
    }
    
    /// <summary>
    /// Repositório de usuários X regras de sistema
    /// </summary>
    IRepository<UsuarioRegraSistema> UsuarioRegraSistemaRepository
    {
        get;
    }
    
    /// <summary>
    /// Repositório de UF
    /// </summary>
    IRepository<Uf> UfRepository
    {
        get;
    }

    /// <summary>
    /// Repositório de Atividade Remunerada
    /// </summary>
    IRepository<TipoAtendimento> TipoAtendimentoRepository
    {
        get;
    }

    /// <summary>
    /// Repositório de Atividade Remunerada
    /// </summary>
    IRepository<TipoAtividadeRemunerada> TipoAtividadeRemuneradaRepository
    {
        get;
    }

    /// <summary>
    /// Repositório de Dependente
    /// </summary>
    IRepository<TipoDependente> TipoDependenteRepository
    {
        get;
    }

    /// <summary>
    /// Repositório de Escolaridade
    /// </summary>
    IRepository<TipoEscolaridade> TipoEscolaridadeRepository
    {
        get;
    }

    /// <summary>
    /// Repositório de Estado Civil
    /// </summary>
    IRepository<TipoEstadoCivil> TipoEstadoCivilRepository
    {
        get;
    }

    /// <summary>
    /// Repositório de UF
    /// </summary>
    IRepository<TipoMoradia> TipoMoradiaRepository
    {
        get;
    }

    /// <summary>
    /// Repositório de Cadastro de Assistidos
    /// </summary>
    IAssistidoRepository AssistidoRepository
    {
        get;
    }

    /// <summary>
    /// Repositório de Cadastro de Atendimentos
    /// </summary>
    IAtendimentoRepository AtendimentoRepository
    {
        get;
    }

    /// <summary>
    /// Repositório de Cadastro de Atendimentos
    /// </summary>
    IRepository<Atendimento_TipoAtendimento> Atendimento_TipoAtendimentoRepository
    {
        get;
    }

    /// <summary>
    /// Repositório de Cadastro de Disciplinas
    /// </summary>
    IRepository<Disciplina> DisciplinaRepository
    {
        get;
    }

    /// <summary>
    /// Repositório de Cadastro de Disciplinas
    /// </summary>
    IRepository<TurmaAluno> TurmaAlunoRepository
    {
        get;
    }

    /// <summary>
    /// Repositório de Cadastro de Disciplinas
    /// </summary>
    IRepository<Turma> TurmaRepository
    {
        get;
    }

    /// <summary>
    /// Habilitará carregamento preguiçoso (LazyLoader)
    /// </summary>
    /// <param name="enableLazyLoader"></param>
    void EnableLazyLoader(bool enableLazyLoader);
    
    /// <summary>
    /// Salvar alterãções de repositório
    /// </summary>
    /// <returns></returns>
    Task SaveAsync();

}