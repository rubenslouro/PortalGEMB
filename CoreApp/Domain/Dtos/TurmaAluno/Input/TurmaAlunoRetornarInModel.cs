using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TurmaAluno.Input;

public class TurmaAlunoRetornarInModel
{
    //[Required(ErrorMessage = "O código do assistido é obrigatório para realizar a pesquisa.")]
    //public required int TuAl_ID_Assistido { get; set; }
    public int TuAl_ID_Assistido { get; set; }

    public int TuAl_ID_Turma { get; set; }
}
