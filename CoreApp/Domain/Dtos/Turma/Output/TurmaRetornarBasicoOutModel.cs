using UtilService.Util;

namespace Domain.Dtos.Turma.Output
{
    public class TurmaRetornarBasicoOutModel
    {
        public TurmaRetornarBasicoOutModel(Entities.Turma entity)
        {
            //Nome = $"{entity.Aten_NM_Nome} ({entity.Aten_NR_CPF.ToFormatedCpfCnpjString()})";
            Codigo = entity.Turm_ID_Turma;
        }

        public int Codigo { get; set; }

        public string Nome { get; set; }
    }
}
