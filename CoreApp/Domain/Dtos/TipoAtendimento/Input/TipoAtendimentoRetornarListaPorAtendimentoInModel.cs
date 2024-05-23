using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoAtendimento.Input;

public class TipoAtendimentoRetornarListaPorAtendimentoInModel
{
    [Required(ErrorMessage = "O código do tipo de atendimento é obrigatório para realizar a pesquisa de uma UF.")]
    public int TpAt_ID_Atendimento { get; set; }
}