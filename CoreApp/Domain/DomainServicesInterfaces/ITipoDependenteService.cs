using Domain.Dtos.TipoAtendimento.Input;
using Domain.Dtos.TipoDependente.Input;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Núcleo de serviços de UF
/// </summary>
public interface ITipoDependenteService
{
    /// <summary>
    /// Lista todos os Tipos de Moradia cadastradas no sistema
    /// </summary>
    /// <returns></returns>
    Task<List<TipoDependente>> ListarAsync();

    /// <summary>
    /// Editar dados básicos de um assistido
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoDependente> EditarAsync(TipoDependenteEditarInModel model);

    /// <summary>
    /// Retorna uma UF pelo código
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoDependente> RetornarAsync(TipoDependenteRetornarInModel model);

    /// <summary>
    /// Retona dados básicos de um tipo de dependente
    /// </summary>
    /// <param name="codTipoDependente"></param>
    /// <returns></returns>
    Task<TipoDependente> RetornarDadosBasicosAsync(int? codTipoDependente);

    /// <summary>
    /// Lista uma UF por descrição
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoDependente> RetornarDadosPorDescricaoAsync(TipoDependenteRetornaPorDescricaoInModel model);

    /// <summary>
    /// Adicionar tipo de dependentes
    /// </summary>
    /// <returns></returns>
    Task AdicionarTipoDependenteAsync();
}