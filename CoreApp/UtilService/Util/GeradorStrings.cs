

using System.Linq;
using System;

namespace UtilService.Util;

/// <summary>
/// Classe responsável por gerar strings aleatórias
/// </summary>
public static class GeradorStrings
{
      
    /// <summary>
    /// Gera uma string aleatória com tamanho pre definido, podendo incluir números e caracteres especiais
    /// </summary>
    /// <param name="tamanho"></param>
    /// <param name="incluirNumeros"></param>
    /// <param name="incluirCaracteresEspeciais"></param>
    /// <returns></returns>
    public static string Gerar(int tamanho, bool incluirNumeros, bool incluirCaracteresEspeciais)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        chars += chars.ToLower();

        if(incluirNumeros)
            chars += "0123456789";

        if(incluirCaracteresEspeciais)
            chars += "@#$%¨&*()_-";

        var random = new Random();
        var result = new string(Enumerable.Repeat(chars, tamanho).Select(s => s[random.Next(s.Length)]).ToArray());
        return result;
    }
}