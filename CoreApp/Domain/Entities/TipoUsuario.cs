namespace Domain.Entities;

public partial class TipoUsuario
{
    public int Codigo { get; set; }

    public string Descricao { get; set; }
    
    /// <summary>
    /// Relacionamento das classes
    /// </summary>

    public virtual ICollection<TipoUsuarioRegraSistema> TipoUsuarioRegraSistemas { get; set; } = new List<TipoUsuarioRegraSistema>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}