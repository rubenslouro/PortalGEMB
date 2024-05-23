using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoAtendimento.Input;

public class TipoAtendimentoRetornarInModel
{
    [Required(ErrorMessage = "O código do tipo de atendimento é obrigatório para realizar a pesquisa de uma UF.")]
    public int TpAt_ID_TipoAtendimento { get; set; }
}