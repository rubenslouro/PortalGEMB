using UtilService.Util;

namespace Domain.Dtos.Disciplina.Output
{
    public class DisciplinaRetornarBasicoOutModel
    {
        public DisciplinaRetornarBasicoOutModel(Entities.Disciplina entity)
        {
            //Nome = $"{entity.Aten_NM_Nome} ({entity.Aten_NR_CPF.ToFormatedCpfCnpjString()})";
            Codigo = entity.Disc_ID_Disciplina;
        }

        public int Codigo { get; set; }

        public string Nome { get; set; }
    }
}
