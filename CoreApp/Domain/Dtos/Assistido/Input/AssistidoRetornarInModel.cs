using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Assistido.Input;

public class AssistidoRetornarInModel
{
    [Required(ErrorMessage = "O código do assistido é obrigatório para realizar a pesquisa.")]
    public required int Assi_ID_Assistido { get; set; }
}
