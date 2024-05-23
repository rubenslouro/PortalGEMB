namespace Domain.Entities;

public partial class TipoMoradia
{
    public int TpMo_ID_TipoMoradia { get; set; }

    public string TpMo_NM_Descricao { get; set; }

    /// <summary>
    /// Relacionamento das classes
    /// </summary>
    /// 

    public virtual ICollection<Assistido> Assistidos { get; set; } = new List<Assistido>();
}