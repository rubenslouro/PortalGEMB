using Domain.Dtos.ConfiguracaoGeral.Input;
using Domain.Dtos.ConfiguracaoGeral.Output;
using Domain.Dtos.LogGenerico.Output;
using Domain.Entities;

namespace Domain.DomainServicesInterfaces;

/// <summary>
/// Configurações gerais
/// </summary>
public interface IConfiguracaoGeralService
{
    /// <summary>
    /// Retorna as configurações do sistema
    /// </summary>
    /// <returns></returns>
    Task<ConfiguracaoGeralRetornaOutModel?> RetornarAsync();

    /// <summary>
    /// Edita as configurações existentes no sistema
    /// </summary>
    /// <param name="editarConfiguracaoInModel"></param>
    /// <returns></returns>
    Task<ConfiguracaoGeralEditarConfiguracaoOutModel> EditarConfiguracaoAsync(ConfiguracaoGeralEditarConfiguracaoInModel editarConfiguracaoInModel);

    /// <summary>
    /// Retorna dados detalhados sobre as configurações do sistema
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ConfiguracaoGeralRetornaDetalhadoOutModel?> RetornaDetalhadoAsync(ConfiguracaoGeralRetornaDetalhadoInModel model);

    /// <summary>
    /// Exibe o histórico de alterações realizadas nas configurações do sistema
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<LogGenericoListarLogOutModel> ListarLogAsync(ConfiguracaoGeralListarLogInModel model);

    /// <summary>
    /// Salva configuração do sistema
    /// </summary>
    /// <param name="configuracaoGeral"></param>
    /// <returns></returns>
    Task IncluirPrimeiraConfiguracaoSistemaAsync(ConfiguracaoGeral configuracaoGeral);
}