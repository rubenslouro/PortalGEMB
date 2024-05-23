using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class TurmaAluno
{
    /// <summary>
    /// Código de Identificação da Turma
    /// </summary>
    [Display(Name = "Código da Turma")]
    public int TuAl_ID_Turma { get; set; }

    /// <summary>
    /// Código de Identificação do Assistido
    /// </summary>
    [Display(Name = "Código do Assistido")]
    public int TuAl_ID_Assistido { get; set; }

    /// <summary>
    /// Definição de relacionamento com outras tabelas
    /// </summary>

    public virtual Turma TuAl_ID_TurmaNavigation { get; set; }

    public virtual Assistido TuAl_ID_AssistidoNavigation { get; set; }

}

public class TurmaAlunoIndex
{
    /// <summary>
    /// Código de Identificação da Turma
    /// </summary>
    [Display(Name = "Código da Turma")]
    public int TuAl_ID_Turma { get; set; }

    /// <summary>
    /// Nome da Turma
    /// </summary>
    [Display(Name = "Nome da Turma")]
    public string TuAl_NM_Turma { get; set; }

    /// <summary>
    /// Código de Identificação do Assistido
    /// </summary>
    [Display(Name = "Código do Assistido")]
    public int TuAl_ID_Assistido { get; set; }

    /// <summary>
    /// Nome do Assistido
    /// </summary>
    [Display(Name = "Nome do Assistido")]
    public string TuAl_NM_Assistido { get; set; }

    /// <summary>
    /// Período Letivo (Semestre)
    /// </summary>
    [Display(Name = "Periodo Letivo")]
    public string TuAl_CD_PeriodoLetivo { get; set; }

    /// <summary>
    /// Ano Letivo
    /// </summary>
    [Display(Name = "Ano Letivo")]
    public int TuAl_NR_AnoLetivo { get; set; }
}
