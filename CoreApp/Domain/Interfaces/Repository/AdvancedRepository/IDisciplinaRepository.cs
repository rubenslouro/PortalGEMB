using Domain.Entities;

namespace Domain.Interfaces.Repository.AdvancedRepository;

/// <summary>
/// Repositório de Cadastro de Disciplinas
/// </summary>
public interface IDisciplinaRepository : IRepository<Disciplina>
{
    /// <summary>
    /// Retorna uma disciplina pelo código
    /// </summary>
    /// <param name="codigo"></param>
    /// <returns></returns>
    Task<Disciplina?> RetornaPorCodigoAsync(int codigo);
}
