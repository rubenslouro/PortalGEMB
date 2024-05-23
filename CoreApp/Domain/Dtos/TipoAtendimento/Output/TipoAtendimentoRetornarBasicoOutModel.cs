using UtilService.Util;

namespace Domain.Dtos.Assistido.Output
{
    public class TipoAtendimentoRetornarBasicoOutModel
    {
        public TipoAtendimentoRetornarBasicoOutModel(Entities.TipoAtendimento entity)
        {
            TpAt_NM_Descricao = $"{entity.TpAt_NM_Descricao} ({entity.TpAt_NM_Descricao})";
            TpAt_ID_TipoAtendimento = entity.TpAt_ID_TipoAtendimento;
        }

        public int TpAt_ID_TipoAtendimento { get; set; }

        public string TpAt_NM_Descricao { get; set; }
    }
}
