using Domain.Dtos.AtendimentoTipoAtendimento.Output;
using Domain.Dtos.TurmaAluno.Input;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Conjunto de métodos voltados para operações relacionadas a aluno
/// </summary>
public interface ITurmaAlunoService
{
    /// <summary>
    /// Lista de todas as alunos
    /// </summary>
    /// <returns></returns>
    Task<List<TurmaAluno>> ListarAsync();

    /// <summary>
    /// Cria uma nova aluno
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TurmaAluno> AdicionarAsync(TurmaAlunoAdicionarInModel model);

    /// <summary>
    /// Edita uma aluno existente
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TurmaAluno> EditarAsync(TurmaAlunoEditarInModel model);

    /// <summary>
    /// Retorna uma aluno pelo código
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TurmaAluno> RetornarAsync(TurmaAlunoRetornarInModel model);

    /// <summary>
    /// Retona a lista de alunos matriculado para algum curso
    /// </summary>
    /// <param name="codTurma"></param>
    /// <returns></returns>
    Task<List<TurmaAluno>> RetornarListaAlunosTurmaAsync(int codTurma);

    ///// <summary>
    ///// Lista uma UF por descrição
    ///// </summary>
    ///// <param name="model"></param>
    ///// <returns></returns>
    //Task<Aluno> RetornarDadosPorDescricaoAsync(AlunoRetornaPorDescricaoInModel model);
}

