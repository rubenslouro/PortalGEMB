using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Usuario.Afastar.Input;

public class UsuarioAfastarInModel
{
    public const string CodUsuarioRequired = "O código do usuário que será afastado é obrigatório.";
    public const string CodUsuarioRange = "O código do usuário que será afastado é obrigatório.";
    public const string CodUsuarioResponsavelRequired = "O código do usuário que será responsável pelo afastamento é obrigatório.";
    public const string CodUsuarioResponsavelRange = "O código do usuário que será responsável pelo afastamento é obrigatório.";

    /// <summary>
    /// Código do usuário a ser afastado
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuario { get; set; }
    /// <summary>
    /// Código do usuário responsável pelo afastamento
    /// </summary>
    [Required(ErrorMessage = CodUsuarioResponsavelRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioResponsavelRange)]
    public int CodUsuarioResponsavel { get; set; }
}