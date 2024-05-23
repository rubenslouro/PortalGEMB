using System;

namespace UtilService.Util;

/// <summary>
/// Classe que contém métodos para operações com documentos
/// </summary>
public static class DocumentOperations
{
    /// <summary>
    /// Retorna true se o CNPJ for válido. Requer CNPJ com pontuação.
    /// </summary>
    /// <param name="cnpj"></param>
    /// <returns></returns>
    public static bool ValidateCNPJ(this string cnpj)
    {
        cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

        if (cnpj.Length != 14)
            return false;

        int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCnpj = cnpj.Substring(0, 12);
        var soma = 0;

        for (var i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicadores1[i];

        var resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        tempCnpj += resto;

        soma = 0;
        for (var i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicadores2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        tempCnpj += resto;

        return cnpj.EndsWith(tempCnpj);
    }

    /// <summary>
    /// Retorna true se o CPF for válido. Requer CPF com pontuação.
    /// </summary>
    /// <param name="cpf"></param>
    /// <returns></returns>
    public static bool ValidateCPF(this string cpf)
    {
        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
            return false;

        int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        var tempCpf = cpf.Substring(0, 9);
        var soma = 0;

        for (var i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

        var resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        tempCpf += resto;

        soma = 0;
        for (var i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        tempCpf += resto;

        return cpf.EndsWith(tempCpf);
    }

    /// <summary>
    /// Formata uma string para CPF ou CNPJ
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToFormatedCpfCnpjString(this string value)
    {
        try
        {
            if (!value.IsNullOrWhiteSpace())
            {
                return long.Parse(value).ToString(value.Length < 12 ? @"000\.000\.000\-00" : @"00\.000\.000\/0000\-00");
            }

            return string.Empty;
        }
        catch (Exception)
        {
            return "";
        }
    }
}