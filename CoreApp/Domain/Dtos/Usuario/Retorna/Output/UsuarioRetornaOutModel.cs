using System.ComponentModel;

namespace Domain.Dtos.Usuario.Retorna.Output;

public class UsuarioRetornaOutModel
{
    public UsuarioRetornaOutModel(Entities.Usuario usuario)
    {
        Codigo = usuario.Codigo;
        Email = usuario.Email;
        Nome = usuario.Nome;
        CodTipoUsuario = usuario.CodTipoUsuario;
        TipoUsuarioDescricao = usuario.CodTipoUsuarioNavigation.Descricao;
    }

    /// <summary>
    /// Código do usuário retornado
    /// </summary>
    [DisplayName("Código")]
    public int Codigo { get; private set; }
    /// <summary>
    /// Email do usuário retornado
    /// </summary>
    [DisplayName("E-mail")]
    public string Email { get; private set; }
    /// <summary>
    /// Nome do usuário retornado
    /// </summary>
    [DisplayName("Nome")]
    public string Nome { get; private set; }
    /// <summary>
    /// Código do tipo usuário retornado
    /// </summary>
    [DisplayName("Cód. Tipo Usuário")]
    public int CodTipoUsuario { get; private set; }
    /// <summary>
    /// Tipo do usuário/perfil do usuário retornado
    /// </summary>
    [DisplayName("Tipo Usuário")]
    public string TipoUsuarioDescricao { get; private set; }
    /// <summary>
    /// Data de afastamento do usuário
    /// </summary>
    [DisplayName("Data de afastamento")]
    public DateTime? DataAfastamento { get; private set; }
}