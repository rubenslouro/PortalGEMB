using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class Disciplina
{
    /// <summary>
    /// ID
    /// </summary>
    [Display(Name = "Código")]
    public int Disc_ID_Disciplina { get; set; }

    /// <summary>
    /// Descrição da disciplina
    /// </summary>
    [Display(Name = "Descrição")]
    public string Disc_NM_Nome { get; set; }

    /// <summary>
    /// Observações para o cadastro
    /// </summary>
    [Display(Name = "Observação")]
    public string? Disc_TX_Observacao { get; set; }

    /// <summary>
    /// Usuário que efetuou o cadastro
    /// </summary>
    [Display(Name = "Usuário de Cadastro")]
    public int Disc_ID_UsuarioCadastro { get; set; }

    /// <summary>
    /// Data de cadastro da turma
    /// </summary>
    [Display(Name = "Data de Cadastro")]
    public DateTime Disc_DT_Cadastro { get; set; }

    /// <summary>
    /// Definição de relacionamento com outras tabelas
    /// </summary>

    public virtual Usuario Disc_ID_UsuarioCadastroNavigation { get; set; }

    /// <summary>
    /// Relacionamento das classes
    /// </summary>
    
    public virtual ICollection<Turma> Turmas { get; set; } = new List<Turma>();
}
