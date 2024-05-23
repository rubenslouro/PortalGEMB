namespace Domain.Dtos.RegraSistema.AdicionarRegraPerfil.Output;

public class RegraSistemaAdicionarRegraSistemaPerfilUsuarioOutModel
{
    /// <summary>
    /// Lista dos códigos de usuários que foram afetados pela adição de regra de sistema
    /// </summary>
    public List<int> ListaCodigosUsuariosAfetados { get; set; }
}