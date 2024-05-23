using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoDependente.Input;

public class TipoDependenteRetornaPorDescricaoInModel
{
    [Required(ErrorMessage = "A descricao do tipo de dependente é obrigatório para realizar a pesquisa.")]
    //[StringLength(2, ErrorMessage = "A descrição do Tipo de Moradia deve conter no mínimo 2 letras.")]
    [MinLength(2, ErrorMessage = "A descrição do tipo de dependente deve conter no mínimo 2 letras.")]
    public string TpDe_NM_Descricao { get; set; } = string.Empty;
}