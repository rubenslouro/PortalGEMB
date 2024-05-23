namespace Domain.Entities;

public partial class UsuarioRegraSistema
{
    public int Codigo { get; set; }

    public int CodUsuario { get; set; }

    public int CodRegraSistema { get; set; }

    public int CodUsuarioInclusao { get; set; }

    public DateTime DataHoraInclusao { get; set; }

    /// <summary>
    /// Definição de relacionamento com outras tabelas
    /// </summary>

    public virtual RegraSistema CodRegraSistemaNavigation { get; set; }

    public virtual Usuario CodUsuarioInclusaoNavigation { get; set; }

    public virtual Usuario CodUsuarioNavigation { get; set; }
}