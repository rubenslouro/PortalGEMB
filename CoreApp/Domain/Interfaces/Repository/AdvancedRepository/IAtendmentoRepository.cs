using Domain.Entities;

namespace Domain.Interfaces.Repository.AdvancedRepository;

/// <summary>
/// Repositório de Cadastro de Atendimentos
/// </summary>
public interface IAtendimentoRepository : IRepository<Atendimento>
{
    /// <summary>
    /// Retorna um atendimento pelo código
    /// </summary>
    /// <param name="codigo"></param>
    /// <returns></returns>
    Task<Atendimento?> RetornaPorCodigoAsync(int codigo);
}