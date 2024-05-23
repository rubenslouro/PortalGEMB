namespace Domain.Dtos.RegraSistema.AdicionarTodasPerfil.Output;

public class RegraSistemaAdicionarTodasRegraSistemaPerfilUsuarioOutModel
{
    /// <summary>
    /// Lista dos códigos de usuários que foram afetados pela adição de regra de sistema
    /// </summary>
    public List<int> ListaCodigosUsuariosAfetados { get; set; }
}