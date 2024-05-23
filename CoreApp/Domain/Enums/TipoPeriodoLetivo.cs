using System.ComponentModel;
using System.Reflection;

namespace Domain.Enums;

/// <summary>
/// Enum com as definição de sim ou não
/// </summary>
public enum EnumTipoPeriodoLetivo
{
    /// <summary>
    /// 1º Semestre
    /// </summary>
    [DefaultValue("1ºSem")]
    [Description("1º Semestre")]
    Semestre1 = 0,

    /// <summary>
    /// 2º Semestre
    /// </summary>
    [DefaultValue("2ºSem")]
    [Description("2º Semestre")]
    Semestre2 = 1
}

public static class EnumTipoPeriodoLetivoExtensions
{
    public static string ObterDescricao(this EnumTipoPeriodoLetivo valor)
    {
        var campo = valor.GetType().GetField(valor.ToString());
        var atributo = campo?.GetCustomAttribute<DescriptionAttribute>();
        return atributo?.Description ?? "Outra categoria";
    }

    public static string ObterValorDefault(this EnumTipoPeriodoLetivo valor)
    {
        var campo = valor.GetType().GetField(valor.ToString());
        var atributo = campo?.GetCustomAttribute<DefaultValueAttribute>();
        return atributo?.Value.ToString() ?? "Outra categoria";
    }

    public static string ObterDescricao(string valor)
    {
        string result;

        switch (valor)
        {
            case "1ºSem":
                result = "1º Semestre";
                break;
            case "2ºSem":
                result = "2º Semestre";
                break;
            default:
                result = "";
                break;
        }

        return result;
    }
}