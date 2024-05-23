namespace Domain.Dtos.RegraSistema.RemoverPermissaoPerfil.Output;

public class RegraSistemaRemoverRegraSistemaTipoUsuarioOutModel
{
    /// <summary>
    /// Lista dos códigos de usuários que foram afetados pela adição de regra de sistema
    /// </summary>
    public List<int> ListaCodigosUsuariosAfetados { get; set; }
}