using Domain.Dtos.TipoAtendimento.Input;
using Domain.Dtos.TipoAtividadeRemunerada.Input;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Núcleo de serviços de UF
/// </summary>
public interface ITipoAtividadeRemuneradaService
{
    /// <summary>
    /// Lista todos os Tipos de Moradia cadastradas no sistema
    /// </summary>
    /// <returns></returns>
    Task<List<TipoAtividadeRemunerada>> ListarAsync();

    /// <summary>
    /// Editar dados básicos de um assistido
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoAtividadeRemunerada> EditarAsync(TipoAtividadeRemuneradaEditarInModel model);

    /// <summary>
    /// Retorna uma UF pelo código
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoAtividadeRemunerada> RetornarAsync(TipoAtividadeRemuneradaRetornarInModel model);

    /// <summary>
    /// Retona dados básicos de um tipo de atividade remunerada
    /// </summary>
    /// <param name="codTipoAtividadeRemunerada"></param>
    /// <returns></returns>
    Task<TipoAtividadeRemunerada> RetornarDadosBasicosAsync(int? codTipoAtividadeRemunerada);

    /// <summary>
    /// Lista uma UF por descrição
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoAtividadeRemunerada> RetornarDadosPorDescricaoAsync(TipoAtividadeRemuneradaRetornaPorDescricaoInModel model);

    /// <summary>
    /// Adicionar tipo de atividade remunerada
    /// </summary>
    /// <returns></returns>
    Task AdicionarTipoAtividadeRemuneradaAsync();
}