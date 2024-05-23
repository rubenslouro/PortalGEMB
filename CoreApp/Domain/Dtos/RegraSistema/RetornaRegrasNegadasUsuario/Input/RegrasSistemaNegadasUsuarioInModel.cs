
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.RetornaRegrasNegadasUsuario.Input;

public class RegrasSistemaNegadasUsuarioInModel
{
    public const string CodUsuarioRequired = "Informe o código do usuário que será consultado para regras negadas.";
    public const string CodUsuarioRange = "Informe o código do usuário que será consultado para regras negadas.";

    /// <summary>
    /// Código do usuário no qual será consultada quais regras de sistema não estão vinculadas a ele
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuario { get; set; }
}