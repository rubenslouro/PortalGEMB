namespace Domain.Dtos.Usuario.Listar.Output;

public class UsuarioItemOutModel
{
    /// <summary>
    /// Código do usuário
    /// </summary>
    public int Codigo { get; set; }
    /// <summary>
    /// Email do usuário
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Nome do usuário
    /// </summary>
    public string Nome { get; set; }
    /// <summary>
    /// Data e hora de cadastro do usuário
    /// </summary>
    public DateTime DataCadastro { get; set; }
    /// <summary>
    /// Data de afastamento do usuário
    /// </summary>
    public DateTime? DataAfastamento { get; set; }
    /// <summary>
    /// usuário que realizou o cadastro do usuário
    /// </summary>
    public string UsuarioCadastro { get; set; }
}