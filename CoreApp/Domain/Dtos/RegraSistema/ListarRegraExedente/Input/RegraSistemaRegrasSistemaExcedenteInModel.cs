
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.ListarRegraExedente.Input;

public class RegraSistemaRegrasSistemaExcedenteInModel
{  
    public const string CodUsuarioRequired = "Informe o código do usuário que será consultado para regras exedente.";
    public const string CodUsuarioRange = "Informe o código do usuário que será consultado para regras exedente.";

    /// <summary>
    /// Código do usuário no qual será consultado para saber quais regras de sistema são
    /// exedentes em relação ao perfil/tipo usuário atual do usuário.
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuario { get; set; }
}