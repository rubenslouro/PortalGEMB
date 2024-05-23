namespace Domain.Entities;

public partial class TipoDependente
{
    public int TpDe_ID_TipoDependente { get; set; }

    public string TpDe_NM_Descricao { get; set; }

    /// <summary>
    /// Relacionamento das classes
    /// </summary>
    /// 

    public virtual ICollection<Assistido> Assistidos { get; set; } = new List<Assistido>();
}