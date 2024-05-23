

using System;
using System.ComponentModel;

namespace UtilService.Util;

/// <summary>
/// Classe de operações com enum
/// </summary>
public static class EnumOperations
{

    /// <summary>
    /// Converte o enum para um array de inteiro
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static int[] ToIntArray<T>()
    {
        return (int[])Enum.GetValues(typeof(T));
    }

    /// <summary>
    /// recupera a descrição do enum
    /// </summary>
    /// <param name="enum"></param>
    /// <returns></returns>
    public static string GetDescription(this Enum @enum)
    {
        var e = @enum.GetType().GetField(@enum.ToString());
        var attributes = (DescriptionAttribute[])e?.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes != null && attributes.Length > 0 ? attributes[0].Description : @enum.ToString();
    }

    /// <summary>
    /// Recupera o valor default do enum
    /// </summary>
    /// <param name="enum"></param>
    /// <returns></returns>
    public static string GetDefaultValue(this Enum @enum)
    {
        var e = @enum.GetType().GetField(@enum.ToString());
        var attributes = (DefaultValueAttribute[])e?.GetCustomAttributes(typeof(DefaultValueAttribute), false);
        return attributes != null && attributes.Length > 0 ? attributes[0].Value?.ToString() : @enum.ToString();
    }

    /// <summary>
    /// Recupera um valor do inteiro do enum
    /// </summary>
    /// <param name="enum"></param>
    /// <returns></returns>
    public static int GetIntValue(this Enum @enum)
    {
        return Convert.ToInt32(@enum);
    }
}