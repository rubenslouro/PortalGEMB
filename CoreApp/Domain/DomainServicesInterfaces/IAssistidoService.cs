using Domain.Dtos.Assistido.Input;
using Domain.Dtos.Assistido.Output;
using Domain.Dtos.LogGenerico.Output;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Conjunto de métodos voltados para operações relacionadas ao assistido
/// </summary>
public interface IAssistidoService
{
    /// <summary>
    /// Lista de todos os assistidos
    /// </summary>
    /// <returns></returns>
    Task<List<Assistido>> ListarAsync();

    /// <summary>
    /// Lista todas as alterações realizadas em um assistido
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<LogGenericoListarLogOutModel> ListarLogAsync(AssistidoListarLogInModel model);

    /// <summary>
    /// Cria um novo assistido
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Assistido> AdicionarAsync(AssistidoAdicionarInModel model);

    /// <summary>
    /// Edita um assistido existente
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Assistido> EditarAsync(AssistidoEditarInModel model);

    /// <summary>
    /// Retona dados básicos de um assistido
    /// </summary>
    /// <param name="codigo"></param>
    /// <returns></returns>
    Task<AssistidoRetornarBasicoOutModel> RetornarDadosBasicosAsync(int codigo);

    /// <summary>
    /// Retorna um cadastro de um pessoa
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<Assistido> RetornarAsync(AssistidoRetornarInModel model);
}

