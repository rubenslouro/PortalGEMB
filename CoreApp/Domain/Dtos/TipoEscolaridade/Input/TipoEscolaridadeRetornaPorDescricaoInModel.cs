using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoEscolaridade.Input;

public class TipoEscolaridadeRetornaPorDescricaoInModel
{
    [Required(ErrorMessage = "A descricao do tipo da escolaridade é obrigatório para realizar a pesquisa.")]
    //[StringLength(2, ErrorMessage = "A descrição do Tipo de Moradia deve conter no mínimo 2 letras.")]
    [MinLength(2, ErrorMessage = "A descrição do tipo da escolaridade deve conter no mínimo 2 letras.")]
    public string TpEs_NM_Descricao { get; set; } = string.Empty;
}