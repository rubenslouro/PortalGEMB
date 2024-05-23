using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.RetornaRegrasNegadasTipoUsuario.Input;

public class RegrasSistemaNegadasTipoUsuarioInModel
{
    public const string CodTipoUsuarioRequired = "Informe o código do tipo de usuário que terá as regras negadas consultadas.";
    public const string CodTipoUsuarioRange = "Informe o código do tipo de usuário que terá as regras negadas consultadas.";

    /// <summary>
    /// Código do tipo de usuário/perfil que retornará as regras de sistema que não estão vinculadas a ele
    /// </summary>
    [Required(ErrorMessage = CodTipoUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodTipoUsuarioRange)]
    public int CodTipoUsuario { get; set; }
}