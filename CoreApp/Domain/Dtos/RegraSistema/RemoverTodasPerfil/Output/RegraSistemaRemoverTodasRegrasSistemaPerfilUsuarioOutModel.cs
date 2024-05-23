namespace Domain.Dtos.RegraSistema.RemoverTodasPerfil.Output;

public class RegraSistemaRemoverTodasRegrasSistemaPerfilUsuarioOutModel
{
    /// <summary>
    /// Lista dos códigos de usuários que foram afetados pela adição de regra de sistema
    /// </summary>
    public List<int> ListaCodigosUsuariosAfetados { get; set; }
}