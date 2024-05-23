using System.Reflection;
using System.Text.RegularExpressions;

namespace UtilService.Util;

/// <summary>
/// Classe de mátodos de extensão para objetos serem transformados em string formatada
/// </summary>
public static class TextOperations
{
    /// <summary>
    /// Realiza trim em toras as propriedades string de um modelo de dados
    /// </summary>
    /// <param name="obj"></param>
    /// <typeparam name="T"></typeparam>
    public static void TrimStringProperties<T>(this T obj)
    {
        // Obtém as propriedades públicas do objeto do tipo T
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Percorre todas as propriedades
        foreach (var property in properties)
        {
            // Verifica se a propriedade é do tipo string
            if (property.PropertyType == typeof(string))
            {
                // Obtém o valor atual da propriedade
                var value = (string)property.GetValue(obj);

                // Faz o Trim do valor e atribui de volta à propriedade
                if (value != null)
                {
                    var trimmedValue = value.Trim();
                    property.SetValue(obj, trimmedValue);
                }
            }
        }
    }
    
    /// <summary>
    /// Converte uma string para null se vazia
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string AsNullIfEmpty(this string value)
    {
        if (!value.IsNullOrWhiteSpace())
        {
            return value;
        }
        return null;
    }

    /// <summary>
    /// Converte uma string para null se tiver espaço vazio
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string AsNullIfWhiteSpace(this string value)
    {
        if (!value.IsNullOrWhiteSpace())
        {
            return value;
        }
        return null;
    }

    /// <summary>
    /// Verifica se é nulo ou espaço vazio
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static bool IsNullOrWhiteSpace(this string text)
    {
        return string.IsNullOrWhiteSpace(text);
    }

    /// <summary>
    /// Formata string para trim e uppercase
    /// </summary>
    /// <param name="texto"></param>
    /// <returns></returns>
    public static string ToTrimUpper(this string texto)
    {
        if (!string.IsNullOrEmpty(texto)) { texto = texto.Trim().ToUpper(); }
        else
        {
            return "";
        }

        return texto;
    }

    /// <summary>
    /// Formata string para trim e lowercase
    /// </summary>
    /// <param name="texto"></param>
    /// <returns></returns>
    public static string ToTrimLower(this string texto)
    {
        if (!string.IsNullOrEmpty(texto))
        {
            texto = texto.Trim().ToLower();
        }

        return texto;
    }

    /// <summary>
    /// Retorna apenas letras de uma string
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string ApenasLetras(this string text)
    {
        var input = text;
        const string expr = @"(?i)[^a-záéíóúàèìòùâêîôûãõç\s]";
        var rgx = new Regex(expr);
        return rgx.Replace(input, string.Empty).Trim();
    }

    /// <summary>
    /// Retorna letras e números de uma string retirando caractéres especiais
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string ApenasLetrasNumeros(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return text;
        }

        var input = text;
        const string expr = @"(?i)[^a-z0-9áéíóúàèìòùâêîôûãõç\s]";
        var rgx = new Regex(expr);
        return rgx.Replace(input, string.Empty).Trim();
    }

    /// <summary>
    /// Retorna números de uma string 
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string ApenasNumeros(this string text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        var ret = string.Join(string.Empty, Regex.Split(text, @"[^\d]")).Trim();
        return ret.IsNullOrWhiteSpace() ? null : ret;
    }
}