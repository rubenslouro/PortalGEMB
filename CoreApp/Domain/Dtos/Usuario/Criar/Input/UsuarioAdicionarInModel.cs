using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Usuario.Criar.Input;

public class UsuarioAdicionarInModel
{
    public const string EmailMaxLength = "O email informado deve ter até 255 caracteres";
    public const string EmailRequired = "O e-mail é obrigatório";
    public const string EmailAddress = "O campo tem que ser um e-mail";
    public const string NomeMaxLength = "O nome informado deve ter até 100 caracteres";
    public const string NomeMinLength = "O nome informado deve ter no mínimo 5 caracteres";
    public const string NomeRequired = "O nome é obrigatório";
    public const string SenhaRequired = "Senha obrigatória";
    public const string SenhaStringLength = "A senha tem que ter entre 5 e 20 dígitos";
    public const string SenhaConfirmacaoRequired = "A confirmação de sua senha é necessária";
    public const string SenhaConfirmacaoStringLength = "A senha tem que ter entre 5 e 20 dígitos";
    public const string SenhaConfirmacaoCompare = "A senha e a confirmação de senha devem ser idênticas";
    public const string CodTipoUsuarioRequired = "O tipo do usuário é obrigatório";

    /// <summary>
    /// Email do novo usuário (será utilizado para login no sistema)
    /// </summary>
    [MaxLength(255, ErrorMessage = EmailMaxLength)]
    [Required(ErrorMessage = EmailRequired)]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = EmailAddress)]
    public string Email { get; set; }
    /// <summary>
    /// Nome do novo usuário
    /// </summary>
    [MaxLength(100, ErrorMessage = NomeMaxLength)]
    [MinLength(5, ErrorMessage = NomeMinLength)]
    [Required(ErrorMessage = NomeRequired)]
    public string Nome { get; set; }
    /// <summary>
    /// Senha do novo usuário
    /// </summary>
    [Required(ErrorMessage = SenhaRequired)]
    [StringLength(20, ErrorMessage = SenhaStringLength, MinimumLength = 5)]
    [DataType(DataType.Password)]
    public string Senha { get; set; }
    /// <summary>
    /// Confirmação da nova senha de usuário
    /// </summary>
    [Required(ErrorMessage = SenhaConfirmacaoRequired)]
    [StringLength(20, ErrorMessage = SenhaConfirmacaoStringLength, MinimumLength = 5)]
    [DataType(DataType.Password)]
    [Compare("Senha", ErrorMessage = SenhaConfirmacaoCompare)]
    [DisplayName("Confirmação de senha")]
    public string SenhaConfirmacao { get; set; }
    /// <summary>
    /// Código do tipo de usuário/perfil que será utilizado pelo novo usuário
    /// </summary>
    [Required(ErrorMessage = CodTipoUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodTipoUsuarioRequired)]
    public int CodTipoUsuario { get; set; }
    /// <summary>
    /// Código do usuário que realizou o cadastro
    /// </summary>
    public int CodUsuarioCadastro { get; set; }

}