using Domain.Dtos.Permissao.Input;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Núcleo de permissões
/// </summary>
public interface IPermissaoService
{
    /// <summary>
    /// Utilizado para gerar crítica caso o usuário não tenha a permissão solicitada
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task CriticaNivelAcessoAsync(PermissaoCriticaNivelAcessoInModel model);
    /// <summary>
    /// Avalia se o usuário tem determinada permissão
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Retona um boolean que quando true a permissão existe e quando false o usuário não tem acesso a permissão</returns>
    Task<bool> AvaliarNivelAsync(PermissaoAvaliarNivelInModel model);
}