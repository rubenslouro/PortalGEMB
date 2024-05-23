using Domain.Dtos.Assistido.Output;
using Domain.Dtos.TipoMoradia.Input;
using Domain.Dtos.TipoMoradia.Output;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Conjunto de métodos voltados para operações relacionadas ao tipo de moradia
/// </summary>
public interface ITipoMoradiaService
{
    /// <summary>
    /// Lista todos os Tipos de Moradia cadastradas no sistema
    /// </summary>
    /// <returns></returns>
    Task<List<TipoMoradia>> ListarAsync();

    /// <summary>
    /// Editar dados básicos de um assistido
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoMoradia> EditarAsync(TipoMoradiaEditarInModel model);

    /// <summary>
    /// Retorna uma UF pelo código
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoMoradia> RetornarAsync(TipoMoradiaRetornarInModel model);

    /// <summary>
    /// Retona dados básicos de um tipo de moradia
    /// </summary>
    /// <param name="codTipoMoradia"></param>
    /// <returns></returns>
    Task<TipoMoradia> RetornarDadosBasicosAsync(int? codTipoMoradia);
    //Task<TipoMoradiaRetornarBasicoOutModel> RetornarDadosBasicosAsync(int codigo);

    /// <summary>
    /// Lista uma UF por descrição
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoMoradia> RetornarDadosPorDescricaoAsync(TipoMoradiaRetornaPorDescricaoInModel model);

    /// <summary>
    /// Adicionar tipo de moradia
    /// </summary>
    /// <returns></returns>
    Task AdicionarTipoMoradiaAsync();
}