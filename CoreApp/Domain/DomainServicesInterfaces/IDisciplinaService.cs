using Domain.Dtos.Disciplina.Input;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Conjunto de métodos voltados para operações relacionadas a disciplina
/// </summary>
public interface IDisciplinaService
{
    /// <summary>
    /// Lista de todos os Disciplinas
    /// </summary>
    /// <returns></returns>
    Task<List<Disciplina>> ListarAsync();

    /// <summary>
    /// Cria um novo disciplina
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Disciplina> AdicionarAsync(DisciplinaAdicionarInModel model);

    /// <summary>
    /// Edita um disciplina existente
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Disciplina> EditarAsync(DisciplinaEditarInModel model);

    /// <summary>
    /// Retorna uma UF pelo código
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Disciplina> RetornarAsync(DisciplinaRetornarInModel model);

    ///// <summary>
    ///// Lista uma UF por descrição
    ///// </summary>
    ///// <param name="model"></param>
    ///// <returns></returns>
    //Task<Disciplina> RetornarDadosPorDescricaoAsync(DisciplinaRetornaPorDescricaoInModel model);

    /// <summary>
    /// Adicionar disciplinas
    /// </summary>
    /// <returns></returns>
    Task AdicionarDisciplinaAsync();
}

