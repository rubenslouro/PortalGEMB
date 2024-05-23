using System;
using System.Text.RegularExpressions;
using UtilService.Util;

namespace UtilService;

/// <summary>
/// Classe que valida operações com telefone
/// </summary>
public static class PhoneOperations
{
    /// <summary>
    /// Valida se um telefone é válido
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    public static bool IsValidPhone(this string phoneNumber)
    {
        // Remove caracteres não numéricos do número de telefone
        var cleanedPhoneNumber = Regex.Replace(phoneNumber, @"[^\d]", "");

        // Verifica se o número possui o formato correto
        if (cleanedPhoneNumber.Length != 11 && cleanedPhoneNumber.Length != 10)
            return false;

        // Obtém o DDD e o prefixo do número de telefone
        var ddd = cleanedPhoneNumber.Substring(0, 2);
        var prefix = cleanedPhoneNumber.Substring(2, cleanedPhoneNumber.Length - 2);

        // Verifica se o DDD está dentro de uma faixa válida
        if (!int.TryParse(ddd, out var dddValue) || (dddValue < 11 || dddValue > 99))
            return false;

        // Verifica se o prefixo tem 4 ou 5 dígitos
        return cleanedPhoneNumber.Length is 10 or 11;
    }

    /// <summary>
    /// Formata 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToFormatedPhoneString(this string value)
    {
        try
        {
            if (value != null)
            {
                if (!value.IsNullOrWhiteSpace() && value.Length > 10)
                    return long.Parse(value).ToString("(##) #####-####");
                else
                    return long.Parse(value).ToString("(##) ####-####");
            }

            return string.Empty;
        }
        catch (Exception)
        {
            return value ?? "";
        }
    }

}