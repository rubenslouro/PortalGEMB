using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public partial class AtendimentoHistorico
{
    /// <summary>
    /// Lista de Atendimento
    /// </summary>
    public Atendimento Atendimento { get; set; }

    /// <summary>
    /// Lista de Assistido
    /// </summary>
    public Assistido Assistido { get; set; }

    /// <summary>
    /// Lista de Tipo de Atendimento
    /// </summary>
    [Display(Name = "Lista de Atendimento")]
    public List<TipoAtendimento> TipoAtendimento { get; set; }
}

public partial class Atendimento_TipoAtendimento
{
    /// <summary>
    /// ID de controle do atendimento
    /// </summary>
    [Display(Name = "Código")]
    public int AtTA_ID_Atendimento { get; set; }

    /// <summary>
    /// Código de cadastro do tipo de atendimento
    /// </summary>
    [Display(Name = "Tipo de Atendimento")]
    public int AtTA_ID_TipoAtendimento { get; set; }

    /// <summary>
    /// Definição de relacionamento com outras tabelas
    /// </summary>

    public virtual Atendimento AtTA_ID_AtendimentoNavigation { get; set; }

    public virtual TipoAtendimento AtTA_ID_TipoAtendimentoNavigation { get; set; }
}

