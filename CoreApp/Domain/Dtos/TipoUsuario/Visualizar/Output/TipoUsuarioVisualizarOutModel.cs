using System.ComponentModel;
using Domain.Dtos.LogGenerico.Output;

namespace Domain.Dtos.TipoUsuario.Visualizar.Output;

public class TipoUsuarioVisualizarOutModel
{
    /// <summary>
    /// Código do tipo de usuário/perfil
    /// </summary>
    [Description("Código")]
    public int Codigo { get; set; }
    /// <summary>
    /// Descrição do tipo de usuário/perfil
    /// </summary>
    [Description("Descrição")]
    public string Descricao { get; set; }
    /// <summary>
    /// Informa se o tipo de usuário pode ou não ser editado
    /// </summary>
    public bool Editavel { get; set; }
    /// <summary>
    /// Informa se será possível verificar os logs de alteração deste tipo de usuário
    /// </summary>
    public bool PermiteLog { get; set; }
    /// <summary>
    /// Objeto com a lista de logs de alteração
    /// </summary>
    public LogGenericoListarLogOutModel  Log { get; set; }
}