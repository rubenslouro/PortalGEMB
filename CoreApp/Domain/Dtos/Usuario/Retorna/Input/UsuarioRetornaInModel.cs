
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Usuario.Retorna.Input;

public class UsuarioRetornarInModel
{
    public const string EmailMaxLength = "O email informado deve ter até 255 caracteres";
    public const string EmailEmailAddress = "O campo tem que ser um e-mail";
    public const string CodUsuarioRange = "O código do usuário deve ser maior que 0.";

    /// <summary>
    /// Email do usuário a ser retornado
    /// </summary>
    [MaxLength(255, ErrorMessage = EmailMaxLength)]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = EmailEmailAddress)]
    public string Email { get; set; }
    /// <summary>
    /// Código do usuário a ser retornado
    /// </summary>
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int? CodUsuario { get; set; }

}