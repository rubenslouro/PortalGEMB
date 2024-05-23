using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Usuario.Readmitir.Input;

public class UsuarioReativarInModel
{
    public const string CodUsuarioRequired = "O código do usuário que será readmitido é obrigatório.";
    public const string CodUsuarioRange = "O código do usuário que será readmitido é obrigatório.";
    public const string CodUsuarioResponsavelRequired = "O código do usuário que será responsável pela readmissão é obrigatório.";
    public const string CodUsuarioResponsavelRange = "O código do usuário que será responsável pela readmissão é obrigatório.";

    /// <summary>
    /// Código do usuário a ser readimitido no sistema
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuario { get; set; }
    /// <summary>
    /// Código do usuário que realiza a readmissão
    /// </summary>
    [Required(ErrorMessage = CodUsuarioResponsavelRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioResponsavelRange)]
    public int CodUsuarioResponsavel { get; set; }
}