using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoDependente.Input;

public class TipoDependenteRetornarInModel
{
    [Required(ErrorMessage = "O código do tipo de dependente é obrigatório para realizar a pesquisa.")]
    public int TpDe_ID_TipoDependente { get; set; }
}