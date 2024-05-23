using Domain.Dtos.ConfiguracaoGeral.Input;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Configurações gerais
/// </summary>
public interface IDatabaseService
{
    /// <summary>
    /// Utilizado durante a instalação do sistema para configurar e criar o banco de dados
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task ConfigurarBancoDadosECriaAsync(ConfiguracaoGeralConfigurarBancoDadosECriaInModel model);
    /// <summary>
    /// Verifica se o banco de dados já foi criado
    /// </summary>
    /// <returns></returns>
    Task<bool> BancoDadosEstaCriadoAsync();
    /// <summary>
    /// Verifica se o banco de dados já teve as configurações preenchidas
    /// </summary>
    /// <returns></returns>
    Task<bool> BancoDeDadosEstaConfiguradoAsync();
}