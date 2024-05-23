using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoEscolaridade.Input;

public class TipoEscolaridadeRetornarInModel
{
    [Required(ErrorMessage = "O código do tipo de escolaridade é obrigatório para realizar a pesquisa.")]
    public int TpEs_ID_TipoEscolaridade { get; set; }
}