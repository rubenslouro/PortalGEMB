

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace UtilService.Util;

/// <summary>
/// Classe para operações em arquivos
/// </summary>
public static class FileOperations
{
    /// <summary>
    /// Abre um arquivo de texto e converte cada linha em um item de uma lista
    /// </summary>
    /// <param name="arquivo"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<List<string>> AbreToListAsync(string arquivo)
    {
        if (!File.Exists(arquivo))
        {
            throw new Exception("O arquivo " + arquivo + " não existe.");
        }

        var lista = new List<string>();

        var fluxoTexto = new StreamReader(arquivo);

        while (!fluxoTexto.EndOfStream)
        {
            lista.Add(await fluxoTexto.ReadLineAsync());
        }

        fluxoTexto.Close();
        fluxoTexto.Dispose();

        return lista;
    }
}