using Domain.Dtos.Disciplina.Input;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Disciplina.Input;

public class DisciplinaEditarInModel : DisciplinaAdicionarInModel
{
    public DisciplinaEditarInModel()
    {
    }

    public DisciplinaEditarInModel(Entities.Disciplina cadastro)
    {
        Disc_ID_Disciplina = cadastro.Disc_ID_Disciplina;
        Disc_NM_Nome = cadastro.Disc_NM_Nome;

        Disc_TX_Observacao = cadastro.Disc_TX_Observacao;
    }

    [Required(ErrorMessage = "O código do disciplina a ser alterada é obrigatório.")]
    public int Disc_ID_Disciplina { get; set; }
}