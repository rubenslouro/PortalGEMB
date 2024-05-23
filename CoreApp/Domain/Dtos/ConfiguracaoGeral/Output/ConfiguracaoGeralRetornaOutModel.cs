using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ConfiguracaoGeral.Output;

public class ConfiguracaoGeralRetornaOutModel
{
    public ConfiguracaoGeralRetornaOutModel(Entities.ConfiguracaoGeral configuracaoGeral)
    {
        UrlSite = configuracaoGeral.UrlSite;
    }

    /// <summary>
    /// Domínio onde a plicação está hospedada
    /// </summary>
    [Display(Name = "Endereço do site (ex: http://www.google.com)")]
    public string UrlSite { get; private set; }

}