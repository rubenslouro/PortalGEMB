using UtilService.Util;

namespace Domain.Dtos.Atendimento.Output
{
    public class AtendimentoRetornarBasicoOutModel
    {
        public AtendimentoRetornarBasicoOutModel(Entities.Atendimento entity)
        {
            Codigo = entity.Aten_ID_Atendimento;
        }

        public int Codigo { get; set; }

        public string Nome { get; set; }
    }
}
