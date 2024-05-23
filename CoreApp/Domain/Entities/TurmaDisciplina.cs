using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class TurmaDisciplina
{
    /// <summary>
    /// Lista de Turma
    /// </summary>
    public List<TurmaIndex> Turma { get; set; }

    /// <summary>
    /// Lista de Disciplina
    /// </summary>
    public List<Disciplina> Disciplina { get; set; }
}

public partial class TurmaIndex
{
    /// <summary>
    /// ID
    /// </summary>
    [Display(Name = "Código")]
    public int Turm_ID_Turma { get; set; }

    /// <summary>
    /// Número da disciplina
    /// </summary>
    [Display(Name = "Código da Disciplina")]
    public int Turm_ID_Disciplina { get; set; }

    /// <summary>
    /// Nome da Disciplina
    /// </summary>
    [Display(Name = "Nome da Disciplina")]
    public string Turm_NM_Disciplina { get; set; }

    /// <summary>
    /// Descrição da turma
    /// </summary>
    [Display(Name = "Descrição da Turma")]
    public string Turm_TX_Descricao { get; set; }

    /// <summary>
    /// Data inicial da turma
    /// </summary>
    [Display(Name = "Data de Inicio")]
    public string Turm_DT_Inicio { get; set; }

    /// <summary>
    /// Data final da turma
    /// </summary>
    [Display(Name = "Data Final")]
    public string Turm_DT_Final { get; set; }

    /// <summary>
    /// Periodo Letivo
    /// </summary>
    [Display(Name = "Periodo Letivo")]
    public string Turm_CD_PeriodoLetivo { get; set; }

    /// <summary>
    /// Ano Letivo
    /// </summary>
    [Display(Name = "Ano Letivo")]
    public int Turm_NR_AnoLetivo { get; set; }

    /// <summary>
    /// Observações para o cadastro
    /// </summary>
    [Display(Name = "Observação")]
    public string? Turm_TX_Observacao { get; set; }
}