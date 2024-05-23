using Domain.Dtos.Atendimento.Input;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoDependente.Input;

public class TipoDependenteEditarInModel
{
    public TipoDependenteEditarInModel()
    {
    }

    public TipoDependenteEditarInModel(Entities.TipoDependente cadastro)
    {
        TpDe_ID_TipoDependente = cadastro.TpDe_ID_TipoDependente;
        TpDe_NM_Descricao = cadastro.TpDe_NM_Descricao;
    }

    [Display(Name = "Código")]
    [Required(ErrorMessage = "O código do atendimento a ser alterada é obrigatório.")]
    public int TpDe_ID_TipoDependente { get; set; }

    [Required(ErrorMessage = "A descrição do tipo de atendimento é obrigatório.")]
    [Display(Name = "Descrição")]
    [StringLength(150, ErrorMessage = "O limite da descrição é de 150 caracteres.")]
    public string TpDe_NM_Descricao { get; set; }
}