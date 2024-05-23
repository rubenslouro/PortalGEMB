using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public partial class TipoEnunciado
{
    /// <summary>
    /// Lista de Tipo de Atendimento
    /// </summary>
    public List<TipoAtendimento> TipoAtendimento { get; set; }

    /// <summary>
    /// Lista de Tipo de Atividade Remunerada
    /// </summary>
    public List<TipoAtividadeRemunerada> TipoAtividadeRemunerada { get; set; }

    /// <summary>
    /// Lista de Tipo de Dependentes
    /// </summary>
    public List<TipoDependente> TipoDependente { get; set; }

    /// <summary>
    /// Lista de Tipo de Escolaridade
    /// </summary>
    public List<TipoEscolaridade> TipoEscolaridade { get; set; }

    /// <summary>
    /// Lista de Tipo do Estado Civil
    /// </summary>
    public List<TipoEstadoCivil> TipoEstadoCivil { get; set; }

    /// <summary>
    /// Lista de Tipo de Moradia
    /// </summary>
    public List<TipoMoradia> TipoMoradia { get; set; }
}
