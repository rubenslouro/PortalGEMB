using System.ComponentModel.DataAnnotations;

namespace WebCore.Models;

/// <summary>
/// Calcular a diferença em anos de duas datas, ou seja, a idade
/// </summary>
public class CalcModel
{
    [Required(ErrorMessage = "Campo obrigatório")]
    public double fist { get; set; }
    [Required(ErrorMessage = "Campo obrigatório")]
    public double second { get; set; }
    public string operation { get; set; }
    public double result { get; set; }
}

