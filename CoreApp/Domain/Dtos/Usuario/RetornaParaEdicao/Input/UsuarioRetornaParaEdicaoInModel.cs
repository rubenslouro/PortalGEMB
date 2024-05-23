using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Usuario.RetornaParaEdicao.Input;

public class UsuarioRetornaParaEdicaoInModel
{
    public const string CodUsuarioRequired = "O código do usuário que será editado é obrigatório.";
    public const string CodUsuarioRange = "O código do usuário que será editado é obrigatório.";
    public const string CodUsuarioAlteracaoRequired = "O código do usuário fará a edição é obrigatório.";
    public const string CodUsuarioAlteracaoRange = "O código do usuário fará a edição é obrigatório.";

    /// <summary>
    /// Código do usuário que será exibido para edição (usaod apenas pela aplicação MVC .NET)
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuario { get; set; }

    /// <summary>
    /// Código do usuário que solicitou a edição (usaod apenas pela aplicação MVC .NET)
    /// </summary>
    [Required(ErrorMessage = CodUsuarioAlteracaoRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioAlteracaoRange)]
    public int CodUsuarioAlteracao { get; set; }
}