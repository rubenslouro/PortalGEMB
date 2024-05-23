using Domain.Dtos.LogGenerico.Output;

namespace Domain.Dtos.ConfiguracaoGeral.Output;

public class ConfiguracaoGeralRetornaDetalhadoOutModel
{
    public ConfiguracaoGeralRetornaDetalhadoOutModel()
    {
    }

    public ConfiguracaoGeralRetornaDetalhadoOutModel(Entities.ConfiguracaoGeral configuracaoGeral, bool permiteLog, LogGenericoListarLogOutModel? logGenericoListarLogOutModel)
    {
        UrlSite = configuracaoGeral.UrlSite;
        PermiteLog = permiteLog;
        Log = logGenericoListarLogOutModel;
    }

    public string UrlSite { get; private set; }

    /// <summary>
    /// objeto de alterações/Log
    /// </summary>
    public LogGenericoListarLogOutModel? Log { get; private set; }
    /// <summary>
    /// Informa se permitirá se o Log de alterações poderá ser visualizado
    /// </summary>
    public bool PermiteLog { get; private set; }
}