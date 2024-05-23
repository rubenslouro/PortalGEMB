using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoMoradia.Input;

public class TipoMoradiaRetornaPorDescricaoInModel
{
    [Required(ErrorMessage = "A descricao do tipo de moradia é obrigatório para realizar a pesquisa.")]
    //[StringLength(2, ErrorMessage = "A descrição do Tipo de Moradia deve conter no mínimo 2 letras.")]
    [MinLength(2, ErrorMessage = "A descrição do tipo de moradia deve conter no mínimo 2 letras.")]
    public string TpMo_NM_Descricao { get; set; } = string.Empty;
}