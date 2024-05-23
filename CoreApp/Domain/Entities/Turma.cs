using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class Turma
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
    /// Número da turma
    /// </summary>
    [Display(Name = "Número da Turma")]
    public int Turm_NR_Turma { get; set; }

    /// <summary>
    /// Descrição da turma
    /// </summary>
    [Display(Name = "Descrição")]
    public string Turm_TX_Descricao { get; set; }

    /// <summary>
    /// Data de início da turma
    /// </summary>
    [Display(Name = "Data de Início")]
    public DateTime Turm_DT_Inicio { get; set; }

    /// <summary>
    /// Data Final da turma
    /// </summary>
    [Display(Name = "Data Final")]
    public DateTime Turm_DT_Final { get; set; }

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

    /// <summary>
    /// Informação para validar se a turma está aberta para matrícula
    /// </summary>
    [Display(Name = "Aberta para Matrícula")]
    public string Turm_IN_AbertaMatrícula { get; set; }

    /// <summary>
    /// Usuário que efetuou o cadastro
    /// </summary>
    [Display(Name = "Usuário de Cadastro")]
    public int Turm_ID_UsuarioCadastro { get; set; }

    /// <summary>
    /// Data de cadastro da turma
    /// </summary>
    [Display(Name = "Data de Cadastro")]
    public DateTime Turm_DT_Cadastro { get; set; }

    /// <summary>
    /// Definição de relacionamento com outras tabelas
    /// </summary>

    public virtual Disciplina Turm_ID_DisciplinaNavigation { get; set; }
    public virtual Usuario Turm_ID_UsuarioCadastroNavigation { get; set; }

    /// <summary>
    /// Relacionamento das classes
    /// </summary>

    public virtual ICollection<Presenca> Presencas { get; set; } = new List<Presenca>();

    public virtual ICollection<TurmaAluno> TurmaAlunos { get; set; } = new List<TurmaAluno>();

}

