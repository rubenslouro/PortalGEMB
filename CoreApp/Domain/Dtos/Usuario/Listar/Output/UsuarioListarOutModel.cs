namespace Domain.Dtos.Usuario.Listar.Output;

public class UsuarioListarOutModel
{
    /// <summary>
    /// Objeto de lista de usuário
    /// </summary>
    public List<UsuarioItemOutModel> Usuarios { get; set; } = new List<UsuarioItemOutModel>();
}