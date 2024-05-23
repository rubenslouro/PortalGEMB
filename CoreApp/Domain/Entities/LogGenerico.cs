namespace Domain.Entities;

public partial class LogGenerico
{
    public int Codigo { get; set; }

    public string Tabela { get; set; }

    public string Referencia { get; set; }

    public string Campo { get; set; }

    public string ValorAnterior { get; set; }

    public string ValorAlterado { get; set; }

    public int CodUsuarioAcao { get; set; }

    public DateTime DataHoraAcao { get; set; }

    public virtual Usuario CodUsuarioAcaoNavigation { get; set; }
}