
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.UsuarioRegraCustomizada.Input;

public class RegrasSistemaCustomizadasUsuarioInModel
{
    public const string CodUsuarioRequired = "Informe o código do usuário que será consultado para regras customizadas.";
    public const string CodUsuarioRange = "Informe o código do usuário que será consultado para regras customizadas.";

    /// <summary>
    /// Código do usuário que será consultado se possui regras customizadas
    /// </summary>
    [Required ( ErrorMessage = "Informe o código do usuário que será consultado para regras customizadas.")]
    [Range(1, maximum: int.MaxValue, ErrorMessage = "Informe o código do usuário que será consultado para regras customizadas.")]
    public int CodUsuario { get; set; }
}