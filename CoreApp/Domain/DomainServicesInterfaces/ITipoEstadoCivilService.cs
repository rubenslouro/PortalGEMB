using Domain.Dtos.TipoAtendimento.Input;
using Domain.Dtos.TipoEstadoCivil.Input;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Núcleo de serviços de UF
/// </summary>
public interface ITipoEstadoCivilService
{
    /// <summary>
    /// Lista todos os Tipos de Moradia cadastradas no sistema
    /// </summary>
    /// <returns></returns>
    Task<List<TipoEstadoCivil>> ListarAsync();

    /// <summary>
    /// Editar dados básicos de um assistido
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoEstadoCivil> EditarAsync(TipoEstadoCivilEditarInModel model);

    /// <summary>
    /// Retorna uma UF pelo código
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoEstadoCivil> RetornarAsync(TipoEstadoCivilRetornarInModel model);

    /// <summary>
    /// Retona dados básicos de um tipo de estado civil
    /// </summary>
    /// <param name="codTipoEstadoCivil"></param>
    /// <returns></returns>
    Task<TipoEstadoCivil> RetornarDadosBasicosAsync(int? codTipoEstadoCivil);

    /// <summary>
    /// Lista uma UF por descrição
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoEstadoCivil> RetornarDadosPorDescricaoAsync(TipoEstadoCivilRetornaPorDescricaoInModel model);

    /// <summary>
    /// Adicionar tipo de estado civil
    /// </summary>
    /// <returns></returns>
    Task AdicionarTipoEstadoCivilAsync();
}