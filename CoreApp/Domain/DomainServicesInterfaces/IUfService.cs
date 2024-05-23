using Domain.Dtos.UF.RetornaPorDescricao;
using Domain.Dtos.UF.RetornarInModel;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Núcleo de serviços de UF
/// </summary>
public interface IUfService
{
    /// <summary>
    /// Lista todas as UFs cadastradas no sistema
    /// </summary>
    /// <returns></returns>
    Task<List<Uf>> ListarAsync();

    /// <summary>
    /// Retorna uma UF pelo código
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Uf> RetornarAsync(UFRetornarInModel model);

    /// <summary>
    /// Lista uma UF por descrição
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Uf> RetornarDadosPorDescricaoAsync(UFRetornaPorDescricaoInModel model);

    /// <summary>
    /// Adicionar uf dos estados
    /// </summary>
    /// <returns></returns>
    Task AdicionarUfAsync();
}