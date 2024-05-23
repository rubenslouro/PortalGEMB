using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class TipoAtendimento
{
    /// <summary>
    /// ID
    /// </summary>
    [Display(Name = "Código")]
    public int TpAt_ID_TipoAtendimento { get; set; }

    /// <summary>
    /// Descrição do tipo de atendimento
    /// </summary>
    [Display(Name = "Descrição")]
    public string TpAt_NM_Descricao { get; set; }

    /// <summary>
    /// Código da disciplina caso o tipo de atendimento seja um curso
    /// </summary>
    [Display(Name = "Disciplina")]
    public int? TpAt_ID_Disciplina { get; set; }

    /// <summary>
    /// Relacionamento das classes
    /// </summary>

    //public virtual ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();

    public virtual ICollection<Atendimento_TipoAtendimento> TipoAtendimentos { get; set; } = new List<Atendimento_TipoAtendimento>();

}