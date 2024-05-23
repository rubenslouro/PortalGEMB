using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.UF.RetornaPorDescricao;

public class UFRetornaPorDescricaoInModel
{
    [Required(ErrorMessage = "A descricao da UF é obrigatório para realizar a pesquisa de uma UF.")]
    [StringLength(2, ErrorMessage = "A descrição da UF deve ter 2 letras.")]
    [MinLength(2, ErrorMessage = "A descrição da UF deve ter 2 letras.")]
    public string Descricao { get; set; } = string.Empty;
}