using UtilService.Util;

namespace Domain.Dtos.TipoMoradia.Output
{
    public class TipoMoradiaRetornarBasicoOutModel
    {
        public TipoMoradiaRetornarBasicoOutModel(Entities.TipoMoradia entity)
        {
            Codigo = entity.TpMo_ID_TipoMoradia;
            Nome = entity.TpMo_NM_Descricao;
        }

        public int Codigo { get; set; }

        public string Nome { get; set; }
    }
}
