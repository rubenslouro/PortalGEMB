using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.RetornaRegrasTipoUsuario.Input;

public class RegraSistemaRetornaRegrasSistemaTipoUsuarioInModel
{
    public const string CodTipoUsuarioRequired = "Informe o código do tipo de usuário que terá as regras consultadas.";
    public const string CodTipoUsuarioRange = "Informe o código do tipo de usuário que terá as regras consultadas.";

    /// <summary>
    /// Código do tipo usuário/perfil que será conusltado para saber as regras de sistema vinculadas a ele
    /// </summary>
    [Required(ErrorMessage = CodTipoUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodTipoUsuarioRange)]
    public int CodTipoUsuario { get; set; }
}