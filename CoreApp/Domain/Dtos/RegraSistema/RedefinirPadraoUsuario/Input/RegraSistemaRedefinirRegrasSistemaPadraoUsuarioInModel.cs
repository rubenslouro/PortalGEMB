using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.RedefinirPadraoUsuario.Input;

public class RegraSistemaRedefinirRegrasSistemaPadraoUsuarioInModel
{
    public const string CodUsuarioRange = "O usuario a ser alterado é obrigatório";
    public const string CodUsuarioRequired = "O usuario a ser alterado é obrigatório";
    public const string CodUsuarioAlteracaoRequired = "O usuario da ação é obrigatório";
    public const string CodUsuarioAlteracaoRange = "O usuario da ação é obrigatório";

    /// <summary>
    /// Código do usuário que será editado
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRange)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRequired)]
    public int? CodUsuario { get; set; }

    /// <summary>
    /// Código do usuário que irá realizar a ação
    /// </summary>
    [Required(ErrorMessage = CodUsuarioAlteracaoRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioAlteracaoRange)]
    public int CodUsuarioAlteracao { get; set; }
}