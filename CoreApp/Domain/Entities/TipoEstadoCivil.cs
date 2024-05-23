namespace Domain.Entities;

public partial class TipoEstadoCivil
{
    public int TpEC_ID_TipoEstadoCivil { get; set; }

    public string TpEC_NM_Descricao { get; set; }

    /// <summary>
    /// Relacionamento das classes
    /// </summary>
    /// 

    public virtual ICollection<Assistido> Assistidos { get; set; } = new List<Assistido>();
}