using Domain.Dtos.TipoAtendimento.Input;
using Domain.Dtos.TipoEscolaridade.Input;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Núcleo de serviços de UF
/// </summary>
public interface ITipoEscolaridadeService
{
    /// <summary>
    /// Lista todos os Tipos de Moradia cadastradas no sistema
    /// </summary>
    /// <returns></returns>
    Task<List<TipoEscolaridade>> ListarAsync();

    /// <summary>
    /// Editar dados básicos de um assistido
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoEscolaridade> EditarAsync(TipoEscolaridadeEditarInModel model);

    /// <summary>
    /// Retorna uma UF pelo código
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoEscolaridade> RetornarAsync(TipoEscolaridadeRetornarInModel model);

    /// <summary>
    /// Retona dados básicos de um tipo de escolaridade
    /// </summary>
    /// <param name="codTipoEscolaridade"></param>
    /// <returns></returns>
    Task<TipoEscolaridade> RetornarDadosBasicosAsync(int? codTipoEscolaridade);

    /// <summary>
    /// Lista uma UF por descrição
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoEscolaridade> RetornarDadosPorDescricaoAsync(TipoEscolaridadeRetornaPorDescricaoInModel model);

    /// <summary>
    /// Adicionar tipo de escolaridade
    /// </summary>
    /// <returns></returns>
    Task AdicionarTipoEscolaridadeAsync();
}