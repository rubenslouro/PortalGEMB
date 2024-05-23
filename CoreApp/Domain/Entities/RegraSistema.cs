namespace Domain.Entities;

public partial class RegraSistema
{
    public int Codigo { get; set; }

    public string RegraSistemaDescricao { get; set; }

    public string Detalhamento { get; set; }

    public virtual ICollection<LogUsuarioRegraSistema> LogUsuarioRegraSistemas { get; set; } = new List<LogUsuarioRegraSistema>();

    public virtual ICollection<TipoUsuarioRegraSistema> TipoUsuarioRegraSistemas { get; set; } = new List<TipoUsuarioRegraSistema>();

    public virtual ICollection<UsuarioRegraSistema> UsuarioRegraSistemas { get; set; } = new List<UsuarioRegraSistema>();
}