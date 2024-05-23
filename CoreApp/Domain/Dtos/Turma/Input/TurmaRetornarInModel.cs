using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Turma.Input;

public class TurmaRetornarInModel
{
    [Required(ErrorMessage = "O código do turma é obrigatório para realizar a pesquisa.")]
    public required int Turm_ID_Turma { get; set; }
}
