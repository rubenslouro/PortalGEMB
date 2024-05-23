namespace Domain.Dtos.TipoUsuario.Listar.Output;

public class TipoUsuarioListarOutModel
{
    /// <summary>
    /// Objeto com a lista dos tipos de usuário
    /// </summary>
    public List<TipoUsuarioItemOutModel> ListaTiposUsuario { get; set; } = new List<TipoUsuarioItemOutModel>();

}