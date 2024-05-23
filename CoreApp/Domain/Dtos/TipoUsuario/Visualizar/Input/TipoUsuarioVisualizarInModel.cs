using System.ComponentModel.DataAnnotations;
using Domain.Dtos.TipoUsuario.Retorna.Input;

namespace Domain.Dtos.TipoUsuario.Visualizar.Input;

public class TipoUsuarioVisualizarInModel : TipoUsuarioTipoUsuarioRetornarInModel
{
    public const string CodUsuarioConsultaRequired = "O código do tipo de usuário que realizará a consulta é obrigatório";
    public const string CodUsuarioConsultaRange = "O código do tipo de usuário que realizará a consulta é obrigatório";

    /// <summary>
    /// Código do tipo de usuário/perfil que será visualizado
    /// </summary>
    [Required(ErrorMessage = CodUsuarioConsultaRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioConsultaRange)]
    public int CodUsuarioConsulta { get; set; }
}