using Domain.Dtos.Turma.Input;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Turma.Input;

public class TurmaEditarInModel : TurmaAdicionarInModel
{
    public TurmaEditarInModel()
    {
    }

    public TurmaEditarInModel(Entities.Turma cadastro)
    {
        Turm_ID_Turma = cadastro.Turm_ID_Turma;
        Turm_ID_Disciplina = cadastro.Turm_ID_Disciplina;
        Turm_NR_Turma = cadastro.Turm_NR_Turma;
        Turm_TX_Descricao = cadastro.Turm_TX_Descricao;
        Turm_DT_Inicio = cadastro.Turm_DT_Inicio.ToString();
        Turm_DT_Final = cadastro.Turm_DT_Final.ToString();

        Turm_TX_Observacao = cadastro.Turm_TX_Observacao;
    }

    [Required(ErrorMessage = "O código do turma a ser alterada é obrigatório.")]
    public int Turm_ID_Turma { get; set; }
}