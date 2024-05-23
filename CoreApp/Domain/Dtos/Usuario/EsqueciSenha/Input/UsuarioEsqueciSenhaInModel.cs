using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Usuario.EsqueciSenha.Input;

/// <summary>
/// Objeto de entrada para método realiza ação de lembrança de senha
/// </summary>
public class UsuarioEsqueciSenhaInModel
{
    public const string EmailMaxLength = "O email informado deve ter até 255 caracteres";
    public const string EmailRequired = "O e-mail é obrigatório";
    public const string EmailEmailAddress = "O campo tem que ser um e-mail";

    /// <summary>
    /// Email do usuário que terá sua senha enviada por email
    /// </summary>
    [MaxLength(255, ErrorMessage = EmailMaxLength)]
    [Required(ErrorMessage = EmailRequired)]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = EmailEmailAddress)]
    public string Email { get; set; }

}