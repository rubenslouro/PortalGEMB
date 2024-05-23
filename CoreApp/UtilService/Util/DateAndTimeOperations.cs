using System;

namespace UtilService.Util;

/// <summary>
/// Classe de operações de tempo
/// </summary>
public static class DateAndTimeOperations
{
    /// <summary>
    /// Formata um DateTime para string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToFormatedDataHora(this DateTime value)
    {
        return value.ToShortDateString() + " " + value.ToShortTimeString();
    }

    /// <summary>
    /// Formata um TimeSpan para formato de string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToFormatedTimeSpan(this TimeSpan value)
    {
        try
        {
            return $"{value.Hours:00}:{value.Minutes:00}:{value.Seconds:00}.{value.Milliseconds:000}";
        }
        catch (Exception)
        {
            return "";
        }
    }

    /// <summary>
    /// Formata uma DateTime para string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToFormatedData(this DateTime? value)
    {
        if (value.HasValue)
        {
            return value.Value.ToShortDateString();
        }

        return string.Empty;
    }
}