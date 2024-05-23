using UtilService.Util;

namespace Domain.Dtos.Assistido.Output
{
    public class AssistidoRetornarBasicoOutModel
    {
        public AssistidoRetornarBasicoOutModel(Entities.Assistido entity)
        {
            Nome = $"{entity.Assi_NM_Nome} ({entity.Assi_NR_CPF.ToFormatedCpfCnpjString()})";
            Codigo = entity.Assi_ID_Assistido;
        }
        public int Codigo { get; set; }

        public string Nome { get; set; }
    }
}
