using UtilService.Util;

namespace WebCore.Models;

/// <summary>
/// Modelo para uso futuro de CSS customizado pelo cliente/usuário
/// </summary>
public class CustomCss
{
    /// <summary>
    /// Apenas teste para cor de fundo dos boões
    /// </summary>
    public string BtnPadraoCorFundo { get; set; }
    /// <summary>
    /// Apenas teste para cor do texto do botão
    /// </summary>
    public string BtnPadraoCorTexto { get; set; }


    /// <summary>
    /// Método que cria o custon CSS
    /// </summary>
    /// <returns></returns>
    public string CssCreate()
    {
        var strRet = "";

        if (!BtnPadraoCorFundo.IsNullOrWhiteSpace())
        {
            strRet += "\n.btn-primary { background-color:" + BtnPadraoCorFundo + ";}\n";
        }

        if (!BtnPadraoCorTexto.IsNullOrWhiteSpace())
        {
            strRet += "\n.btn-primary { color:" + BtnPadraoCorTexto + ";}\n";
        }

        return strRet;
    }

}