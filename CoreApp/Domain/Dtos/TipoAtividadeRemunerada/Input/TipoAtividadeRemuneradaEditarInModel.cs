using Domain.Dtos.Atendimento.Input;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoAtividadeRemunerada.Input;

public class TipoAtividadeRemuneradaEditarInModel
{
    public TipoAtividadeRemuneradaEditarInModel()
    {
    }

    public TipoAtividadeRemuneradaEditarInModel(Entities.TipoAtividadeRemunerada cadastro)
    {
        TpAR_ID_TipoAtividadeRemunerada = cadastro.TpAR_ID_TipoAtividadeRemunerada;
        TpAR_NM_Descricao = cadastro.TpAR_NM_Descricao;
    }

    [Display(Name = "Código")]
    [Required(ErrorMessage = "O código do atendimento a ser alterada é obrigatório.")]
    public int TpAR_ID_TipoAtividadeRemunerada { get; set; }

    [Required(ErrorMessage = "A descrição do tipo de atendimento é obrigatório.")]
    [Display(Name = "Descrição")]
    [StringLength(150, ErrorMessage = "O limite da descrição é de 150 caracteres.")]
    public string TpAR_NM_Descricao { get; set; }
}