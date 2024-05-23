using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Usuario.EsqueciSenha.Output;

/// <summary>
/// Classe de saída com as informações necessárias para tratar a lembrança de senha
/// </summary>
public class UsuarioEsqueciSenhaOutModel
{
    /// <summary>
    /// Email do destinatário
    /// </summary>
    [EmailAddress]
    public string ParaEmail { get; set; }
    /// <summary>
    /// Nome do destinatário
    /// </summary>
    public string ParaNome { get; set; }
    /// <summary>
    /// Nome (identificação) de quem está enviando o email
    /// </summary>
    public string De { get; set; }
    /// <summary>
    /// Assunto do email
    /// </summary>
    public string Assunto { get; set; }
    /// <summary>
    /// Mensagem do email
    /// </summary>
    public string Mensagem { get; set; }
}