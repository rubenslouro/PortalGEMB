using Domain.Dtos.TurmaAluno.Input;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TurmaAluno.Input;

public class TurmaAlunoEditarInModel : TurmaAlunoAdicionarInModel
{
    public TurmaAlunoEditarInModel()
    {
    }

    public TurmaAlunoEditarInModel(Entities.TurmaAluno cadastro)
    {
        TuAl_ID_Turma = cadastro.TuAl_ID_Turma;
        TuAl_ID_Assistido = cadastro.TuAl_ID_Assistido;
        //TuAl_CD_PeriodoLetivo = cadastro.TuAl_CD_PeriodoLetivo;
        //TuAl_NR_AnoLetivo = cadastro.TuAl_NR_AnoLetivo;
    }
}