using System.ComponentModel;
using System.Reflection;

namespace Domain.Enums;

/// <summary>
/// Enum com as definição de sim ou não
/// </summary>
public enum EnumTipoSimNao
{
    /// <summary>
    /// Valor possitivo
    /// </summary>
    [DefaultValue("S")]
    [Description("Sim")]
    Sim = 0,

    /// <summary>
    /// Valor Negativo
    /// </summary>
    [DefaultValue("N")]
    [Description("Não")]
    Nao = 1
}

public static class EnumTipoSimNaoExtensions
{
    public static string ObterDescricao(this EnumTipoSimNao valor)
    {
        var campo = valor.GetType().GetField(valor.ToString());
        var atributo = campo?.GetCustomAttribute<DescriptionAttribute>();
        return atributo?.Description ?? "Outra categoria";
    }

    public static string ObterValorDefault(this EnumTipoSimNao valor)
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
            case "S":
                result = "Sim";
                break;
            case "N":
                result = "Não";
                break;
            default:
                result = "";
                break;
        }

        return result;
    }
}