
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Usuario.Listar.Input;

public class UsuarioListarInModel
{
    public const string CodigoRequired = "O código do usuário que solicitou a listagem é obrigatório.";
    public const string CodigoRange = "O código do usuário que solicitou a listagem é obrigatório.";
    /// <summary>
    /// Código do usuário que solicitou a listagem de usuários
    /// </summary>
    [Required(ErrorMessage = CodigoRequired)]
    [Range(1, int.MaxValue, ErrorMessage = CodigoRange)]
    public int Codigo { get; set; }
}