using Domain.Entities;
using System;
using System.ComponentModel;

namespace WebCore.DTO.LoginWeb.Output;

/// <summary>
/// Classe de loginde usuário via Web
/// </summary>
public class LoginOutModel
{
    /// <summary>
    /// Constructor
    /// </summary>
    public LoginOutModel() 
    { }

    /// <summary>
    /// Contrutor
    /// </summary>
    /// <param name="usuario"></param>
    /// <param name="dataExpiracaoSessao"></param>
    public LoginOutModel(Usuario usuario, DateTime dataExpiracaoSessao)
    {
        Codigo = usuario.Codigo;
        Email = usuario.Email;
        Nome = usuario.Nome;
        CodTipoUsuario = usuario.CodTipoUsuario;
        TipoUsuarioDescricao = usuario.CodTipoUsuarioNavigation.Descricao;
        DataExpiracaoSessao = dataExpiracaoSessao;
    }

    /// <summary>
    /// Atualiza data de expiração
    /// </summary>
    /// <param name="dataExpiracaoSessao"></param>
    public void RefreshDataExpiracaoSessao(DateTime dataExpiracaoSessao) 
    {
        DataExpiracaoSessao = dataExpiracaoSessao;
    }

    /// <summary>
    /// Código do usuário retornado
    /// </summary>
    [DisplayName("Código")]
    public int Codigo { get;  set; }
    /// <summary>
    /// Email do usuário retornado
    /// </summary>
    [DisplayName("E-mail")]
    public string Email { get;  set; }
    /// <summary>
    /// Nome do usuário retornado
    /// </summary>
    [DisplayName("Nome")]
    public string Nome { get;  set; }
    /// <summary>
    /// Código do tipo usuário retornado
    /// </summary>
    [DisplayName("Cód. Tipo Usuário")]
    public int CodTipoUsuario { get;  set; }
    /// <summary>
    /// Tipo do usuário/perfil do usuário retornado
    /// </summary>
    [DisplayName("Tipo Usuário")]
    public string TipoUsuarioDescricao { get;  set; }
    /// <summary>
    /// Data de afastamento do usuário
    /// </summary>
    [DisplayName("Data de afastamento")]
    public DateTime? DataAfastamento { get;  set; }
    /// <summary>
    /// Data da expiração da sessão do usuário
    /// </summary>
    public DateTime DataExpiracaoSessao { get;  set; }
}