using Domain.Dtos.Turma.Input;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Conjunto de métodos voltados para operações relacionadas a turma
/// </summary>
public interface ITurmaService
{
    /// <summary>
    /// Lista de todas as turmas
    /// </summary>
    /// <returns></returns>
    Task<List<Turma>> ListarAsync();

    /// <summary>
    /// Cria uma nova turma
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Turma> AdicionarAsync(TurmaAdicionarInModel model);

    /// <summary>
    /// Edita uma turma existente
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Turma> EditarAsync(TurmaEditarInModel model);

    /// <summary>
    /// Retorna uma turma pelo código
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Turma> RetornarAsync(TurmaRetornarInModel model);

    ///// <summary>
    ///// Lista uma UF por descrição
    ///// </summary>
    ///// <param name="model"></param>
    ///// <returns></returns>
    //Task<Turma> RetornarDadosPorDescricaoAsync(TurmaRetornaPorDescricaoInModel model);
}

