namespace Domain.Entities;

public partial class TipoEscolaridade
{
    public int TpEs_ID_TipoEscolaridade { get; set; }

    public string TpEs_NM_Descricao { get; set; }

    /// <summary>
    /// Relacionamento das classes
    /// </summary>
    /// 

    public virtual ICollection<Assistido> Assistidos { get; set; } = new List<Assistido>();
}