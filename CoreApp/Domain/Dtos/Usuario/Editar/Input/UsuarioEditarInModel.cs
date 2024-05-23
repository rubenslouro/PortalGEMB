
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Usuario.Editar.Input;

public class UsuarioEditarInModel
{
    public const string CodigoRequired = "O usuario a ser alterado é obrigatório";
    public const string EmailMaxLength = "O email informado deve ter até 255 caracteres";
    public const string EmailRequired = "O e-mail é obrigatório";
    public const string EmailEmailAddress = "O campo tem que ser um e-mail";
    public const string NomeMaxLength = "O nome informado deve ter até 100 caracteres";
    public const string NomeMinLength = "O nome informado deve ter no mínimo 5 caracteres";
    public const string NomeRequired = "O nome é obrigatório";
    public const string CodTipoUsuarioRequired = "O tipo do usuário é obrigatório";
    public const string CodTipoUsuarioRange = "O tipo do usuário é obrigatório";
    public const string CodUsuarioAcaoRequired = "O usuario da ação é obrigatório";
    public const string CodUsuarioAcaoRange = "O usuario da ação é obrigatório";

    /// <summary>
    /// Código do usuário que será editado
    /// </summary>
    [Required(ErrorMessage = CodigoRequired)]
    public int Codigo { get; set; }
    /// <summary>
    /// Email do usuário que será editado
    /// </summary>
    [MaxLength(255, ErrorMessage = EmailMaxLength)]
    [Required(ErrorMessage = EmailRequired)]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = EmailEmailAddress)]
    public string Email { get; set; }
    /// <summary>
    /// Nome do usuário que será editado
    /// </summary>
    [MaxLength(100, ErrorMessage = NomeMaxLength)]
    [MinLength(5, ErrorMessage = NomeMinLength)]
    [Required(ErrorMessage = NomeRequired)]
    public string Nome { get; set; }
    /// <summary>
    /// Código do tipo de usuário/perfil associado ao usuário editado
    /// </summary>
    [Required(ErrorMessage = CodTipoUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodTipoUsuarioRange)]
    public int CodTipoUsuario { get; set; }
    /// <summary>
    /// Código do usuário que irá realizar a ação
    /// </summary>
    [Required(ErrorMessage = CodUsuarioAcaoRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioAcaoRange)]
    public int CodUsuarioAcao { get; set; }

}