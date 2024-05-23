using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UtilService.Util;

/// <summary>
/// Classe de validação de modelos de dados
/// </summary>
public static class ValidationModels
{
    /// <summary>
    /// Validador de modelos 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static void ValidateModel(this object model)
    {
        var validationResults = new List<ValidationResult>();
        var ctx = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, ctx, validationResults, true);

        if (validationResults.Any())
            throw new Exception(validationResults.FirstOrDefault().ErrorMessage);
    }
}