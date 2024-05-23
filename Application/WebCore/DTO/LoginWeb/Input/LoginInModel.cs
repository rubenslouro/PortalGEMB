using System.ComponentModel.DataAnnotations;

namespace WebCore.DTO.LoginWeb.Input;

/// <summary>
/// 
/// </summary>
public class LoginInModel
{
    /// <summary>
    /// Email do usuário a ser logado
    /// </summary>
    [Required(ErrorMessage = "O endereço de e-mail é obrigatório.")]
    [Display(Name = "E-mail")]
    [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "E-mail em formato inválido.")]
    [StringLength(60, ErrorMessage = "O campo de e-mail deve ter até 60 caractéres.")]
    public string Email { get; set; }
    /// <summary>
    /// Senha do usuário a ser logado
    /// </summary>
    [Display(Name = "Senha")]
    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "A senha deve ter no mínimo 5 caractéres e no máximo 20.")]
    public string Senha { get; set; }
}