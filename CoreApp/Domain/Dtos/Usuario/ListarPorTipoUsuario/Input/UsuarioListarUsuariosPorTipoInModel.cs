
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Usuario.ListarPorTipoUsuario.Input;

public class UsuarioListarUsuariosPorTipoInModel
{
    public const string CodUsuarioConsultaRequired = "O código do usuário que realizará a consulta é obrigatório";
    public const string CodUsuarioRange = "O código do usuário que realizará a consulta é obrigatório";
    public const string CodTipoUsuarioRequired = "O código do tipo de usuário é obrigatório para realizara consulta de usuário por tipo.";
    public const string CodTipoUsuarioRange = "O código do tipo de usuário é obrigatório para realizara consulta de usuário por tipo.";

    /// <summary>
    /// Código do usuário que está solicitando a consulta
    /// </summary>
    [Required(ErrorMessage = CodUsuarioConsultaRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuarioConsulta { get; set; }
    /// <summary>
    /// Código do tipo de usuário/perfil que terá os usuários vinculados listados
    /// </summary>
    [Required(ErrorMessage = CodTipoUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodTipoUsuarioRange)]
    public int CodTipoUsuario { get; set; }
}