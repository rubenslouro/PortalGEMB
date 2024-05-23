using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.RetornaRegrasUsuario.Input;

public class RegraSistemaRetornaRegrasSistemaUsuarioInModel
{
    public const string CodUsuarioRequired = "Informe o código do usuário que será consultado para regras.";
    public const string CodUsuarioRange = "Informe o código do usuário que será consultado para regras.";

    /// <summary>
    /// Código do usuário que será consultado para saber quais regras de sistema estão vinculadas a ele
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuario { get; set; }
}