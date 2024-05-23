using Domain.Dtos.Atendimento.Input;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoMoradia.Input;

public class TipoMoradiaEditarInModel
{
    public TipoMoradiaEditarInModel()
    {
    }

    public TipoMoradiaEditarInModel(Entities.TipoMoradia cadastro)
    {
        TpMo_ID_TipoMoradia = cadastro.TpMo_ID_TipoMoradia;
        TpMo_NM_Descricao = cadastro.TpMo_NM_Descricao;
    }

    [Display(Name = "Código")]
    [Required(ErrorMessage = "O código do atendimento a ser alterada é obrigatório.")]
    public int TpMo_ID_TipoMoradia { get; set; }

    [Required(ErrorMessage = "A descrição do tipo de atendimento é obrigatório.")]
    [Display(Name = "Descrição")]
    [StringLength(150, ErrorMessage = "O limite da descrição é de 150 caracteres.")]
    public string TpMo_NM_Descricao { get; set; }
}