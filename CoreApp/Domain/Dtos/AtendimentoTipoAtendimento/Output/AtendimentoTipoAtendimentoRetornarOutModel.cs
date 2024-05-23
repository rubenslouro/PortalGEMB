
using System.ComponentModel;

namespace Domain.Dtos.AtendimentoTipoAtendimento.Output;

public class AtendimentoTipoAtendimentoRetornarOutModel
{
    public AtendimentoTipoAtendimentoRetornarOutModel(Entities.Atendimento_TipoAtendimento entrada) 
    {
        AtTA_ID_Atendimento = entrada.AtTA_ID_Atendimento;
        AtTA_ID_TipoAtendimento = entrada.AtTA_ID_TipoAtendimento;
    }

    /// <summary>
    /// Código do atendimento
    /// </summary>
    [Description("Código")]
    public int AtTA_ID_Atendimento { get; private set; }

    /// <summary>
    /// Código do atendimento
    /// </summary>
    [Description("Código")]
    public int AtTA_ID_TipoAtendimento { get; private set; }
}