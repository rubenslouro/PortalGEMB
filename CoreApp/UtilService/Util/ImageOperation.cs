using System;
using System.Linq;

namespace UtilService.Util;

/// <summary>
/// Classe voltada para operações com imagens
/// </summary>
public static class ImageOperation
{
    /// <summary>
    /// Tag que adiciona o prefixo data:image/png;base64, em imágens
    /// </summary>
    public const string PrefixBase64Png = "data:image/png;base64,";

    /// <summary>
    /// Tag que adiciona o prefixo data:image/png;base64, em imágens
    /// </summary>
    public const string PrefixBase64Jpeg = "data:image/jpeg;base64,";

    /// <summary>
    /// Verifica se um Base64 é uma imagem
    /// </summary>
    /// <param name="imagemBase64"></param>
    /// <returns></returns>
    public static bool IsImage(string imagemBase64)
    {
        try
        {
            byte[] data = Convert.FromBase64String(imagemBase64);
            return IsImageFromByte(data);
        }
        catch (Exception)
        {
            return false;
        }
    }
        
    private static bool IsImageFromByte(byte[] bytes)
    {
        // Verifica se o array de bytes começa com um dos cabeçalhos de imagem conhecidos.
        // Para este exemplo, consideramos apenas os cabeçalhos das imagens JPEG, PNG e GIF.
        string jpgHeader = "FFD8FFE0";
        string pngHeader = "89504E47";
        string gifHeader = "47494638";
        string header = ByteArrayToHexString(bytes.Take(4).ToArray());

        return (header == jpgHeader || header == pngHeader || header == gifHeader);
    }

    private static string ByteArrayToHexString(byte[] bytes)
    {
        // Converte o array de bytes em uma string hexadecimal.
        return string.Concat(bytes.Select(b => b.ToString("X2")));
    }
}