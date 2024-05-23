using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoEstadoCivil.Input;

public class TipoEstadoCivilRetornaPorDescricaoInModel
{
    [Required(ErrorMessage = "A descricao do tipo do estado civil é obrigatório para realizar a pesquisa.")]
    //[StringLength(2, ErrorMessage = "A descrição do Tipo de Moradia deve conter no mínimo 2 letras.")]
    [MinLength(2, ErrorMessage = "A descrição do tipo do estado civil deve conter no mínimo 2 letras.")]
    public string TpEC_NM_Descricao { get; set; } = string.Empty;
}