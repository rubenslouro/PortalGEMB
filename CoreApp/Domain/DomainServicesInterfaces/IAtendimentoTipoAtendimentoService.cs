using Domain.Dtos.Assistido.Output;
using Domain.Dtos.Atendimento.Output;
using Domain.Dtos.AtendimentoTipoAtendimento.Input;
using Domain.Dtos.AtendimentoTipoAtendimento.Output;
using Domain.Dtos.TipoAtendimento.Input;
using Domain.Dtos.TipoMoradia.Input;
using Domain.Dtos.TipoMoradia.Output;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Conjunto de métodos voltados para operações relacionadas ao tipo de moradia
/// </summary>
public interface IAtendimento_TipoAtendimentoService
{
    /// <summary>
    /// Lista todos os tipos de atendimento cadastrados
    /// </summary>
    /// <returns></returns>
    Task<List<Atendimento_TipoAtendimento>> ListarAsync();

    ///// <summary>
    ///// Editar dados básicos de um assistido
    ///// </summary>
    ///// <param name="model"></param>
    ///// <returns></returns>
    //Task<Atendimento_TipoAtendimento> EditarAsync(Atendimento_TipoAtendimentoEditarInModel model);

    /// <summary>
    /// Retorna uma UF pelo código
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Atendimento_TipoAtendimento> RetornarAsync(Atendimento_TipoAtendimentoRetornarInModel model);

    /// <summary>
    /// Retona dados dos tipos de atendimento por atendimento
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<List<AtendimentoTipoAtendimentoRetornarOutModel>> RetornarDadosBasicosAsync(Atendimento_TipoAtendimentoRetornarInModel model);

    /// <summary>
    /// Retona dados dos tipos de atendimento por atendimento
    /// </summary>
    /// <param name="codAtendimento"></param>
    /// <returns></returns>
    Task<List<AtendimentoTipoAtendimentoRetornarOutModel>> RetornarListaDadosAsync(int codAtendimento);

    ///// <summary>
    ///// Lista uma UF por descrição
    ///// </summary>
    ///// <param name="model"></param>
    ///// <returns></returns>
    //Task<List<AtendimentoTipoAtendimentoRetornarOutModel>> RetornarDadosPorDescricaoAsync(Atendimento_TipoAtendimentoRetornaPorDescricaoInModel model);
}