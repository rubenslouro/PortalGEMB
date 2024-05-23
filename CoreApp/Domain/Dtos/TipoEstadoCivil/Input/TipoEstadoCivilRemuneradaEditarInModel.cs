using Domain.Dtos.Atendimento.Input;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoEstadoCivil.Input;

public class TipoEstadoCivilEditarInModel
{
    public TipoEstadoCivilEditarInModel()
    {
    }

    public TipoEstadoCivilEditarInModel(Entities.TipoEstadoCivil cadastro)
    {
        TpEC_ID_TipoEstadoCivil = cadastro.TpEC_ID_TipoEstadoCivil;
        TpEC_NM_Descricao = cadastro.TpEC_NM_Descricao;
    }

    [Display(Name = "Código")]
    [Required(ErrorMessage = "O código do atendimento a ser alterada é obrigatório.")]
    public int TpEC_ID_TipoEstadoCivil { get; set; }

    [Required(ErrorMessage = "A descrição do tipo de atendimento é obrigatório.")]
    [Display(Name = "Descrição")]
    [StringLength(150, ErrorMessage = "O limite da descrição é de 150 caracteres.")]
    public string TpEC_NM_Descricao { get; set; }
}