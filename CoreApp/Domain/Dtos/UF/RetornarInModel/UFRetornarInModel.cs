using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.UF.RetornarInModel;

public class UFRetornarInModel
{
    [Required (ErrorMessage = "O código da UF é obrigatório para realizar a pesquisa de uma UF.")]
    public int CodUf { get; set; }
}