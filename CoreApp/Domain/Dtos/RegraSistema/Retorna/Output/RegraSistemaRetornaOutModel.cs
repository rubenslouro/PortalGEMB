
using System.ComponentModel;

namespace Domain.Dtos.RegraSistema.Retorna.Output;

public class RegraSistemaRetornaOutModel
{
    public RegraSistemaRetornaOutModel(Entities.RegraSistema regraSistema) 
    {
        Codigo = regraSistema.Codigo;
        Detalhamento = regraSistema.Detalhamento;
        RegraSistemaDescricao = regraSistema.RegraSistemaDescricao;
    }

    /// <summary>
    /// Código da regra de sistema
    /// </summary>
    [Description("Código")]
    public int Codigo { get; private set; }
    /// <summary>
    /// Detalhamento dos dados da regra de sistema
    /// </summary>
    [Description("Detalhamento")]
    public string Detalhamento { get; private set; }

    /// <summary>
    /// Descrição da regra de sistema
    /// </summary>
    [Description("Descricao")]
    public string RegraSistemaDescricao { get; private set; } 
}