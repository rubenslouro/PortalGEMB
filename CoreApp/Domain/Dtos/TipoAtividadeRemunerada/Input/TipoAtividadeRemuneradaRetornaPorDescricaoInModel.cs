using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoAtividadeRemunerada.Input;

public class TipoAtividadeRemuneradaRetornaPorDescricaoInModel
{
    [Required(ErrorMessage = "A descricao do tipo de atividade remunerada é obrigatório para realizar a pesquisa.")]
    //[StringLength(2, ErrorMessage = "A descrição do Tipo de Moradia deve conter no mínimo 2 letras.")]
    [MinLength(2, ErrorMessage = "A descrição do tipo de atividade remunerada deve conter no mínimo 2 letras.")]
    public string TpAR_NM_Descricao { get; set; } = string.Empty;
}