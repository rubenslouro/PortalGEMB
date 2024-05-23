using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Disciplina.Input;

public class DisciplinaRetornarInModel
{
    [Required(ErrorMessage = "O código do disciplina é obrigatório para realizar a pesquisa.")]
    public required int Disc_ID_Disciplina { get; set; }
}
