namespace Domain.Entities;

public partial class TipoAtividadeRemunerada
{
    public int TpAR_ID_TipoAtividadeRemunerada { get; set; }

    public string TpAR_NM_Descricao { get; set; }

    /// <summary>
    /// Relacionamento das classes
    /// </summary>
    /// 

    public virtual ICollection<Assistido> Assistidos { get; set; } = new List<Assistido>();
}