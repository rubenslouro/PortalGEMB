using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Atendimento.Input;

public class AtendimentoRetornarInModel
{
    [Required(ErrorMessage = "O código do atendimento é obrigatório para realizar a pesquisa.")]
    public required int codAtendimento { get; set; }
}
