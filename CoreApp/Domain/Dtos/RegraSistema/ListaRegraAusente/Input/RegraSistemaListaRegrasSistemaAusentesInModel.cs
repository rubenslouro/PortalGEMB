using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.ListaRegraAusente.Input;

public class RegraSistemaListaRegrasSistemaAusentesInModel
{
    public const string CodUsuarioRequired = "Informe o código do usuário que será consultado para regras ausentes.";
    public const string CodUsuarioRange = "Informe o código do usuário que será consultado para regras ausentes.";

    /// <summary>
    /// Código do usuário que será consultado para saber quais regras de sistema
    /// estão ausentes em relação ao perfil/tipo de usuário atual do usuário pesquisado
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuario { get; set; }
       
}