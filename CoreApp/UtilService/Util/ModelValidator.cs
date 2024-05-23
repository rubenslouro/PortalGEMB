using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UtilService.Util;

/// <summary>
/// Classe de validação de modelos
/// </summary>
public static class ValidationExtensions
{

    /// <summary>
    /// Verifica se o modelo de dados é válido
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static void CheckIfModelIsValid<T>(this T obj)
    {
        var context = new ValidationContext(obj);
        var results = new System.Collections.Generic.List<ValidationResult>();
        if (!Validator.TryValidateObject(obj, context, results, true)) 
        {
            throw new Exception(results.FirstOrDefault().ErrorMessage);
        }            
    }
}