namespace Domain.Entities;

public partial class LogUsuarioRegraSistema
{
    public int Codigo { get; set; }

    public int CodUsuario { get; set; }

    public int CodUsuarioAcao { get; set; }

    public int CodRegraSistema { get; set; }

    public bool Inclusao { get; set; }

    public DateTime DataHora { get; set; }

    public virtual RegraSistema CodRegraSistemaNavigation { get; set; }

    public virtual Usuario CodUsuarioAcaoNavigation { get; set; }

    public virtual Usuario CodUsuarioNavigation { get; set; }
}