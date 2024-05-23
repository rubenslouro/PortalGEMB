namespace Domain.Entities;

public partial class TipoUsuarioRegraSistema
{
    public int Codigo { get; set; }

    public int CodTipoUsuario { get; set; }

    public int CodRegraSistema { get; set; }

    public int CodUsuarioInclusao { get; set; }

    /// <summary>
    /// Definição de relacionamento com outras tabelas
    /// </summary>

    public virtual RegraSistema CodRegraSistemaNavigation { get; set; }

    public virtual TipoUsuario CodTipoUsuarioNavigation { get; set; }

    public virtual Usuario CodUsuarioInclusaoNavigation { get; set; }
}