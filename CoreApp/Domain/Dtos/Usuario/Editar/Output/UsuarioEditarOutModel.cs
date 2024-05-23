namespace Domain.Dtos.Usuario.Editar.Output;

public class UsuarioEditarOutModel
{
    public UsuarioEditarOutModel(Entities.Usuario usuario)
    {
        Codigo = usuario.Codigo;
        Email = usuario.Email;
        Nome = usuario.Nome;           
        DataCadastro = usuario.DataCadastro;
    }

    /// <summary>
    /// Código do usuário editado
    /// </summary>
    public long Codigo { get; set; }
    /// <summary>
    /// Email do usuário editado
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Nome do usuário editado
    /// </summary>
    public string Nome { get; set; }
    /// <summary>
    /// Data e hora da criação do usuário
    /// </summary>
    public DateTime DataCadastro { get; set; }
}