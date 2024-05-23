using Domain.Dtos.Usuario.Retorna.Output;

namespace Domain.Dtos.Usuario.RetornaParaEdicao.Output;

public class UsuarioRetornaParaEdicaoOutModel
{
    public UsuarioRetornaParaEdicaoOutModel(UsuarioRetornaOutModel usuario)
    {
        Codigo = usuario.Codigo;
        Email = usuario.Email;
        Nome = usuario.Nome;
        CodTipoUsuario = usuario.CodTipoUsuario;
    }

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
    /// Código do tipo usuário
    /// </summary>
    public int CodTipoUsuario { get; set; }
    /// <summary>
    /// Código do usuário da ação
    /// </summary>
    public int CodUsuarioAcao { get; set; }
}