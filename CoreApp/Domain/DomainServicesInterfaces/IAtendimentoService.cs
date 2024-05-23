using Domain.Dtos.Atendimento.Input;
using Domain.Dtos.Atendimento.Output;
using Domain.Dtos.LogGenerico.Output;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Conjunto de métodos voltados para operações relacionadas ao atendimento
/// </summary>
public interface IAtendimentoService
{
    /// <summary>
    /// Lista de todos os atendimentos
    /// </summary>
    /// <returns></returns>
    Task<List<Atendimento>> ListarAsync();

    /// <summary>
    /// Lista todas as alterações realizadas em um atendimento
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<LogGenericoListarLogOutModel> ListarLogAsync(AtendimentoListarLogInModel model);

    /// <summary>
    /// Cria um novo atendimento
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Atendimento> AdicionarAsync(AtendimentoAdicionarInModel model);

    /// <summary>
    /// Edita um atendimento existente
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Atendimento> EditarAsync(AtendimentoEditarInModel model);

    /// <summary>
    /// Retona dados básicos de um atendimento
    /// </summary>
    /// <param name="codigo"></param>
    /// <returns></returns>
    Task<AtendimentoRetornarBasicoOutModel> RetornarDadosBasicosAsync(int codigo);

    /// <summary>
    /// Retorna um cadastro de um atendimento
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Atendimento> RetornarAsync(AtendimentoRetornarInModel model);


    Task<Atendimento> QtdAtendimento_SexoAsync();

    Task<List<Atendimento>> RetornarQtdAtendimentoAsync();

}

