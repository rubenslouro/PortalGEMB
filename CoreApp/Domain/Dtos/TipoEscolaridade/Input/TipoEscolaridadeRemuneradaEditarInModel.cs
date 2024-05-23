using Domain.Dtos.Atendimento.Input;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoEscolaridade.Input;

public class TipoEscolaridadeEditarInModel
{
    public TipoEscolaridadeEditarInModel()
    {
    }

    public TipoEscolaridadeEditarInModel(Entities.TipoEscolaridade cadastro)
    {
        TpEs_ID_TipoEscolaridade = cadastro.TpEs_ID_TipoEscolaridade;
        TpEs_NM_Descricao = cadastro.TpEs_NM_Descricao;
    }

    [Display(Name = "Código")]
    [Required(ErrorMessage = "O código do atendimento a ser alterada é obrigatório.")]
    public int TpEs_ID_TipoEscolaridade { get; set; }

    [Required(ErrorMessage = "A descrição do tipo de atendimento é obrigatório.")]
    [Display(Name = "Descrição")]
    [StringLength(150, ErrorMessage = "O limite da descrição é de 150 caracteres.")]
    public string TpEs_NM_Descricao { get; set; }
}