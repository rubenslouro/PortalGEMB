using Domain.Dtos.Usuario.Retorna.Input;
using Domain.Dtos.Usuario.Retorna.Output;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Checagem de usuário
/// </summary>
public interface IUsuarioCheckerService
{
    /// <summary>
    /// Critica usuários não ativos ao mesmo tempo que checa a existência
    /// </summary>
    /// <param name="codUsuario"></param>
    /// <returns></returns>
    Task CriticarUsuarioInativoAsync(int codUsuario);

    /// <summary>
    /// Retorna usuário com seus dados básicos
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<UsuarioRetornaOutModel> RetornarAsync(UsuarioRetornarInModel model);

    /// <summary>
    /// Critica se o usuário não existir
    /// </summary>
    /// <param name="codUsuario"></param>
    /// <returns></returns>
    Task CriticarUsuarioNaoExistenteAsync(int codUsuario);

    /// <summary>
    /// Critica se o usuário não estiver na lista de usuários que são especiais Ex: Usuários Master
    /// </summary>
    /// <param name="codUsuario"></param>
    /// <returns></returns>
    Task CriticaUsuariosEspeciais(int codUsuario);
    /// <summary>
    /// Retona um usuário ativos. Caso o usuário não esteja ativo, será emitida critica
    /// </summary>
    /// <param name="RetornarInModel"></param>
    /// <returns></returns>
    Task<UsuarioRetornaOutModel> RetornaAtivoAsync(UsuarioRetornarInModel RetornarInModel);
}