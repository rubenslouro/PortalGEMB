using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoAtendimento.Input;

public class Atendimento_TipoAtendimentoRetornaPorDescricaoInModel
{
    [Required(ErrorMessage = "A descricao do tipo de atendimento é obrigatório para realizar a pesquisa.")]
    [MinLength(2, ErrorMessage = "A descrição do tipo de atendimento deve conter no mínimo 2 letras.")]
    public string TpAt_NM_Descricao { get; set; } = string.Empty;
}