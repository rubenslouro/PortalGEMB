using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoMoradia.Input;

public class TipoMoradiaRetornarInModel
{
    [Required(ErrorMessage = "O código do tipo de moradia é obrigatório para realizar a pesquisa de uma UF.")]
    public int TpMo_ID_TipoMoradia { get; set; }
}