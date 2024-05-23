using Domain.Dtos.Atendimento.Input;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Atendimento.Input;

public class AtendimentoEditarInModel : AtendimentoAdicionarInModel
{
    public AtendimentoEditarInModel()
    {
    }

    public AtendimentoEditarInModel(Entities.Atendimento cadastro)
    {
        Aten_ID_Atendimento = cadastro.Aten_ID_Atendimento;
        Aten_ID_Assistido = cadastro.Aten_ID_Assistido;
        //Aten_ID_TipoAtendimento = cadastro.Aten_ID_TipoAtendimento;

        Aten_TX_Observacao = cadastro.Aten_TX_Observacao;
    }

    [Required(ErrorMessage = "O código do atendimento a ser alterada é obrigatório.")]
    public int Aten_ID_Atendimento { get; set; }
}