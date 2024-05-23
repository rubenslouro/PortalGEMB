using Domain.Dtos.TipoUsuario.Retorna.Input;
using Domain.Dtos.TipoUsuario.Retorna.Output;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Responsável pelas checagens de tipo de usuário
/// </summary>
public interface ITipoUsuarioCheckerService
{
    /// <summary>
    /// Método que critica os tipos de usuários especiais
    /// </summary>
    /// <param name="codTipoUsuario"></param>
    /// <returns></returns>
    Task CriticaUsuariosEspeciaisTipoUsuario(int codTipoUsuario);

    /// <summary>
    /// Critica caso o usuário não exista no banco de dados
    /// </summary>
    /// <param name="codTipoUsuario"></param>
    /// <returns></returns>
    Task CriticarTipoUsuarioNaoExistenteAsync(int codTipoUsuario);

    /// <summary>
    /// Retorna dados de um determinado tipo de usuário/perfil
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<TipoUsuarioRetornaOutModel> RetornarAsync(TipoUsuarioTipoUsuarioRetornarInModel model);
}