
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.Retorna.Input;

public class RegraSistemaRetornarInModel
{
    public const string CodigoRequired = "O código da regra de sistema é obrigatório.";
    /// <summary>
    /// Código da regra sistema que deseja consultar
    /// </summary>
    [Required (ErrorMessage = CodigoRequired)]
    public int Codigo { get; set; }
}