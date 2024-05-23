using Domain.Dtos.LogGenerico.Input;
using Domain.Dtos.LogGenerico.Output;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Núcleo de LOG's
/// </summary>
public interface ILogGenericoService
{
    /// <summary>
    /// Exibe os dados de logs genéricos
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<LogGenericoListarLogOutModel> ListarAsync(LogGenericoListarInModel model);
    /// <summary>
    /// Cria log genérico de sistema
    /// </summary>
    /// <param name="itemOriginal">Classe original a ser comparada</param>
    /// <param name="itemAtual">Classe após a alteração a ser comparada</param>
    /// <param name="referencia">Tabela/Entidade a ser buscada nos registros de alteração</param>
    /// <param name="codUsuarioAcao">Usuário que solicitou a alteração</param>
    /// <returns></returns>
    Task AdicionarAsync(object itemOriginal, object itemAtual, string referencia, int codUsuarioAcao);
}