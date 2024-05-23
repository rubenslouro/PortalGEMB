using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.AtendimentoTipoAtendimento.Input;

public class Atendimento_TipoAtendimentoRetornarInModel
{
    [Required(ErrorMessage = "O código do atendimento é obrigatório para realizar a pesquisa.")]
    public int AtTA_ID_Atendimento { get; set; }
}