using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Usuario.AlterarSenha.Input;

public class UsuarioAlterarSenhaInModel
{
    public const string SenhaAntigaRequired = "A senha antiga é obrigatória";
    public const string SenhaAntigaStringLength = "A senha antiga deve ter entre 5 e 20 caracteres.";
    public const string SenhaNovaRequired = "A senha nova é obrigatória.";
    public const string SenhaNovaStringLength = "A senha nova deve ter entre 5 e 20 caracteres.";
    public const string SenhaNovaConfirmacaoRequired = "A confirmação da senha nova é obrigatória.";
    public const string SenhaNovaConfirmacaoCompare = "A confirmação de senha não é idêntica a nova senha.";
    public const string SenhaNovaConfirmacaoStringLength = "A confirmação da senha nova deve ter entre 5 e 20 caracteres.";

    /// <summary>
    /// Código do usuário que terá a senha alterada
    /// </summary>
    [Display(Name = "Código")]
    public int CodUsuario { get; set; }
    /// <summary>
    /// Senha antiga
    /// </summary>
    [Display(Name = "Senha antiga")]
    [Required(ErrorMessage = SenhaAntigaRequired)]
    [StringLength(20, MinimumLength = 5, ErrorMessage = SenhaAntigaStringLength)]
    [DataType(DataType.Password)]
    public string SenhaAntiga { get; set; }
    /// <summary>
    /// Nova senha 
    /// </summary>
    [Display(Name = "Senha nova")]
    [Required(ErrorMessage = SenhaNovaRequired)]
    [StringLength(20, MinimumLength = 5, ErrorMessage = SenhaNovaStringLength)]
    [DataType(DataType.Password)]
    public string SenhaNova { get; set; }
    /// <summary>
    /// Confirmação da nova senha
    /// </summary>
    [Display(Name = "Senha nova (confirmação)")]
    [Required(ErrorMessage = SenhaNovaConfirmacaoRequired)]
    [StringLength(20, MinimumLength = 5, ErrorMessage = SenhaNovaConfirmacaoStringLength)]
    [DataType(DataType.Password)]
    [Compare("SenhaNova", ErrorMessage = SenhaNovaConfirmacaoCompare)]
    public string SenhaNovaConfirmacao { get; set; }

}