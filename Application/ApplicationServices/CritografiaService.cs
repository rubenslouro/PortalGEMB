using System.Security.Cryptography;
using System.Text;
using Domain.DomainServicesInterfaces;
using Microsoft.Extensions.Configuration;

namespace ApplicationServices;

/// <inheritdoc />
public class CritografiaService : ICriptografiaService
{
    private readonly string _encryptionKey;

    /// <summary>
    /// Contrutor
    /// </summary>
    /// <param name="configuration"></param>
    public CritografiaService(IConfiguration configuration)
    {
        _encryptionKey = configuration["CryptoKey"] ?? throw new InvalidOperationException("CryptoKey");
    }


    /// <summary>
    /// Método para dcriptografar os dados 
    /// </summary>
    /// <returns></returns>
    [Obsolete("Obsolete but i dont have time to change")]
    public string EncryptString(string clearText)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using Aes encryptor = Aes.Create();
        var pdb = new Rfc2898DeriveBytes(_encryptionKey, "Rubens Louro"u8.ToArray());
        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        {
            cs.Write(clearBytes, 0, clearBytes.Length);
        }

        clearText = Convert.ToBase64String(ms.ToArray());

        return clearText;
    }

    /// <summary>
    /// Método para descriptografar os dados 
    /// </summary>
    /// <returns></returns>
    [Obsolete("Obsolete, but I don't have time to change")]
    public string DecryptString(string cipherText)
    {
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using Aes encryptor = Aes.Create();
        var pdb = new Rfc2898DeriveBytes(_encryptionKey, "Rubens Louro"u8.ToArray());

        encryptor.Key = pdb.GetBytes(32);
        encryptor.IV = pdb.GetBytes(16);
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        {
            cs.Write(cipherBytes, 0, cipherBytes.Length);
        }
        cipherText = Encoding.Unicode.GetString(ms.ToArray());

        return cipherText;
    }
}