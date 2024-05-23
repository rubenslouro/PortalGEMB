using System;
using System.Text.RegularExpressions;

namespace UtilService.Util;
/// <summary>
/// Classe dedicada a endereços
/// </summary>
public static class AddressValidator
{

    /// <summary>
    /// Formata uma string para o formato CEP
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToFormatedCepString(this string value)
    {
        try
        {
            return !value.IsNullOrWhiteSpace() ? long.Parse(value).ToString(@"00\.000-000") : string.Empty;
        }
        catch (Exception)
        {
            return "";
        }
    }
    /// <summary>
    /// Método que retorna se um CEP é válido.
    /// </summary>
    /// <param name="cep"></param>
    /// <returns></returns>
    public static bool IsValidCep(this string cep)
    {
        // Remove caracteres não numéricos do CEP
        var cleanedCep = Regex.Replace(cep, @"[^\d]", "");

        // Verifica se o CEP possui o formato correto
        return cleanedCep.Length == 8;
    }
}