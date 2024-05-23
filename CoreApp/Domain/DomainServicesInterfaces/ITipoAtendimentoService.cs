using Domain.Dtos.Assistido.Output;
using Domain.Dtos.Atendimento.Input;
using Domain.Dtos.TipoAtendimento.Input;
using Domain.Dtos.TipoAtividadeRemunerada;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Núcleo de serviços de UF
/// </summary>
public interface ITipoAtendimentoService
{
    /// <summary>
    /// Lista todos os Tipos de Moradia cadastradas no sistema
    /// </summary>
    /// <returns></returns>
    Task<List<TipoAtendimento>> ListarAsync();

    /// <summary>
    /// Editar dados básicos de um assistido
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoAtendimento> EditarAsync(TipoAtendimentoEditarInModel model);

    /// <summary>
    /// Retonar dados básicos de um assistido
    /// </summary>
    /// <param name="codAssistido"></param>
    /// <returns></returns>
    Task<TipoAtendimento> RetornarAsync(TipoAtendimentoRetornarInModel model);

    /// <summary>
    /// Lista uma UF por descrição
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoAtendimento> RetornarDadosPorDescricaoAsync(TipoAtendimentoRetornaPorDescricaoInModel model);

    /// <summary>
    /// Adicionar tipo de atendimento
    /// </summary>
    /// <returns></returns>
    Task AdicionarTipoAtendimentoAsync();
}