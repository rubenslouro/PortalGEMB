using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoUsuario.Retorna.Input;

public class TipoUsuarioTipoUsuarioRetornarInModel
{
    public const string CodigoRequired = "O código do tipo de usuário é obrigatório.";
    /// <summary>
    /// Código do tipo de usuário que será retornado
    /// </summary>
    [Required(ErrorMessage = CodigoRequired)]
    public int Codigo { get; set; }
}