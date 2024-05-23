using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Domain.Enums;

/// <summary>
/// Enum com as definição de sim ou não
/// </summary>
public enum EnumTipoSexo
{
    /// <summary>
    /// Feminino
    /// </summary>
    [DefaultValue("F")]
    [Description("Feminino")]
    [Display(Name = "Feminino")]
    Feminino = 0,

    /// <summary>
    /// Masculino
    /// </summary>
    [DefaultValue("M")]
    [Description("Masculino")]
    [Display(Name = "Masculino")]
    Masculino = 1
}

public static class EnumTipoSexoExtensions
{
    public static string ObterDescricao(this EnumTipoSexo valor)
    {
        var campo = valor.GetType().GetField(valor.ToString());
        var atributo = campo?.GetCustomAttribute<DescriptionAttribute>();
        return atributo?.Description ?? "Outra categoria";
    }

    public static string ObterValorDefault(this EnumTipoSexo valor)
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
            case "F":
                result = "Feminino";
                break;
            case "M":
                result = "Masculino";
                break;
            default:
                result = "";
                break;
        }

        return result;
    }
}
