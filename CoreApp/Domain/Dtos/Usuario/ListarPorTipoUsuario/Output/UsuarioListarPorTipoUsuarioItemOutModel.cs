namespace Domain.Dtos.Usuario.ListarPorTipoUsuario.Output;

public class UsuarioListarPorTipoUsuarioItemOutModel
{
    /// <summary>
    /// Código do usuário
    /// </summary>
    public int Codigo { get; set; }
    /// <summary>
    /// Usuário 
    /// </summary>
    public string Usuario { get; set; }
    /// <summary>
    /// Informa se o usuário possi regras de sistema customizadas
    /// </summary>
    public bool RegraCustomizada { get; set; }
    /// <summary>
    /// Lista de regras de sistema exedentes do usuário
    /// </summary>
    public List<string> RegrasExedentes { get; set; }
    /// <summary>
    /// Lista de regras ausentes do usuário
    /// </summary>
    public List<string> RegrasAusentes { get; set; }
    /// <summary>
    /// Informa se o usuário é um usuário ativo
    /// </summary>
    public bool Ativo { get; set; }


}