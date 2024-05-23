using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoEstadoCivil.Input;

public class TipoEstadoCivilRetornarInModel
{
    [Required(ErrorMessage = "O código do tipo de estado civil é obrigatório para realizar a pesquisa.")]
    public int TpEC_ID_TipoEstadoCivil { get; set; }
}