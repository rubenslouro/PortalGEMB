using Domain.Dtos.RegraSistema.Retorna.Output;

namespace Domain.Dtos.RegraSistema.RetornaRegrasNegadasTipoUsuario.Output;

public class RegrasSistemaNegadasTipoUsuarioOutModel
{
    /// <summary>
    /// Objeto de lista de regras de sistema
    /// </summary>
    public List<RegraSistemaRetornaOutModel> ListaRegras { get; set; } = new List<RegraSistemaRetornaOutModel>();
}