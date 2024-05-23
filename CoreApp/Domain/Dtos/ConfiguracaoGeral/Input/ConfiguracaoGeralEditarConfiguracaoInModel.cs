using System.ComponentModel.DataAnnotations;
using Domain.Dtos.ConfiguracaoGeral.Output;

namespace Domain.Dtos.ConfiguracaoGeral.Input;

/// <summary>
/// Classe de DTO para realizar alteração de configuração de sistema
/// </summary>
public class ConfiguracaoGeralEditarConfiguracaoInModel
{
    public const string UsuarioRange = "O código do usuário que está solicitando os dados deve ser informado.";
    public const string UrlSiteRequired = "O endereço do site é obrigatório.";
    public const string UrlSiteStringLength = "O campo endereço do site deve ter entre 8 e 255 caracteres.";

    public ConfiguracaoGeralEditarConfiguracaoInModel(ConfiguracaoGeralRetornaOutModel model, int codUsuarioAcao)
    {
        UrlSite = model.UrlSite;
        CodUsuarioAcao = codUsuarioAcao;
    }
    public ConfiguracaoGeralEditarConfiguracaoInModel()
    {
        UrlSite = string.Empty;
    }

    /// <summary>
    /// Url do domínio onde a aplicação será instalada ex: http://www.google.com ou http://10.10.10.1
    /// </summary>
    [Required(ErrorMessage = UrlSiteRequired)]
    [Display(Name = "Endereço do site (ex: http://www.google.com)")]
    [StringLength(255, MinimumLength = 8, ErrorMessage = UrlSiteStringLength)]
    public string UrlSite { get; set; }
    /// <summary>
    /// Usuário que realiza a alteração nas configurações de sistema. Em caso de inclusão não será necessário informar
    /// </summary>
    [Range(1, maximum: int.MaxValue, ErrorMessage = UsuarioRange)]
    public int CodUsuarioAcao { get; set; }
}