using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Notify.Input;

/// <summary>
/// Classe modelo de entrada para envio de notificação por email
/// </summary>
public class NotifySendEmailInModel
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