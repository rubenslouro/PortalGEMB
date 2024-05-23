namespace Domain.Dtos.Usuario.Criar.Output;

public class UsuarioAdicionarOutModel
{
    public UsuarioAdicionarOutModel(Entities.Usuario usuario) 
    {
        Codigo = usuario.Codigo;
        Email = usuario.Email;
        Nome = usuario.Nome;
        CodTipoUsuario = usuario.CodTipoUsuario;
        TipoUsuarioDescricao = usuario.CodTipoUsuarioNavigation.Descricao;
        DataCadastro = usuario.DataCadastro;
    }
    /// <summary>
    /// Código do usuário criado
    /// </summary>
    public long Codigo { get; set; }
    /// <summary>
    /// Email do usuário criado
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Nome do usuário criado
    /// </summary>
    public string Nome { get; set; }
    /// <summary>
    /// Código do tipo de usuário/perfil do usuário criado
    /// </summary>
    public int CodTipoUsuario { get; set; }
    /// <summary>
    /// Código e descrição do tipo de usuário/perfil para o usuário criado. Ex: 3 - Gerente
    /// </summary>
    public string TipoUsuarioDescricao { get; set; }
    /// <summary>
    /// Data e hora de cadastro do novo usuário
    /// </summary>
    public DateTime DataCadastro { get; set; }
}