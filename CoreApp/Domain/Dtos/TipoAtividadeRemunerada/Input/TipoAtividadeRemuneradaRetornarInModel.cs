using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoAtividadeRemunerada.Input;

public class TipoAtividadeRemuneradaRetornarInModel
{
    [Required(ErrorMessage = "O código do tipo de atividade remunerada é obrigatório para realizar a pesquisa.")]
    public int TpAR_ID_TipoAtividadeRemunerada { get; set; }
}