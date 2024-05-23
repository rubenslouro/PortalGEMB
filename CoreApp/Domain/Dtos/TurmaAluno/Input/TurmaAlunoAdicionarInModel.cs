using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TurmaAluno.Input;

public class TurmaAlunoAdicionarInModel
{
    [Required(ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    public int IDUsuarioCadastro { get; set; }

    #region Dados de Idenficação

    [Required(ErrorMessage = "O código da turma é obrigatório.")]
    [Display(Name = "Turma")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O código da turma é obrigatório.")]
    public int TuAl_ID_Turma { get; set; }

    [Required(ErrorMessage = "O código do assistido é obrigatório.")]
    [Display(Name = "Aluno \\ Assistido")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O código do assistido é obrigatório.")]
    public int TuAl_ID_Assistido { get; set; }

    [Required(ErrorMessage = "O assistido é obrigatório.")]
    [Display(Name = "Aluno \\ Assistido")]
    public List<CheckBoxList> ChechBoxList { get; set; }

    #endregion
}

