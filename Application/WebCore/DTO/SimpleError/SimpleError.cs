using System;

namespace WebCore.DTO.SimpleError;

/// <summary>
/// Classe de erros simples, geralmente usada para retornar erros no ajax
/// </summary>
public class SimpleError
{
    /// <summary>
    /// Contrutor
    /// </summary>
    /// <param name="ex"></param>
    public SimpleError(Exception ex)
    {
        MessageError = ex.Message;
    }

    /// <summary>
    /// Menssagem de erro
    /// </summary>
    public string MessageError { get; }
}