using Domain.Dtos.ConfiguracaoGeral.Input;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Classe responsável pela instalaçaõ das configurações do sistema
/// </summary>
public interface IApplicationInstallService
{
    /// <summary>
    /// Método responsável pela instalação das configurações do sistema
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<bool> InstalarConfiguracaoAsync(ConfiguracaoGeralInstalarConfiguracaoInModel model);
}