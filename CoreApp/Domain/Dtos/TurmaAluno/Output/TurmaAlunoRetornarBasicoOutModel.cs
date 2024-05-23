using UtilService.Util;

namespace Domain.Dtos.TurmaAluno.Output
{
    public class TurmaAlunoRetornarBasicoOutModel
    {
        public TurmaAlunoRetornarBasicoOutModel(Entities.TurmaAluno entity)
        {
            //Nome = $"{entity.Aten_NM_Nome} ({entity.Aten_NR_CPF.ToFormatedCpfCnpjString()})";
            Codigo = entity.TuAl_ID_Assistido;
        }

        public int Codigo { get; set; }

        public string Nome { get; set; }
    }
}
