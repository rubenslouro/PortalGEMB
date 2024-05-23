using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public partial class Atendimento
{
    /// <summary>
    /// ID de controle do cadastro
    /// </summary>
    [Display(Name = "Código")]
    public int Aten_ID_Atendimento { get; set; }

    /// <summary>
    /// Código de cadastro do assistido
    /// </summary>
    [Display(Name = "Código do Assistido")]
    public int Aten_ID_Assistido { get; set; }

    ///// <summary>
    ///// Código de cadastro do assistido
    ///// </summary>
    //[Display(Name = "Tipo de Atendimento")]
    //public int Aten_ID_TipoAtendimento { get; set; }

    /// <summary>
    /// Observações para o atendimento
    /// </summary>
    [Display(Name = "Observação")]
    public string? Aten_TX_Observacao { get; set; }

    /// <summary>
    /// Usuário que efetuou o cadastro
    /// </summary>
    public int Aten_ID_UsuarioCadastro { get; set; }

    /// <summary>
    /// Data de cadastro da turma
    /// </summary>
    public DateTime Aten_DT_Cadastro { get; set; }

    /// <summary>
    /// Definição de relacionamento com outras tabelas
    /// </summary>

    public virtual Assistido Aten_ID_AssistidoNavigation { get; set; }

    //public virtual TipoAtendimento Aten_ID_TipoAtendimentoNavigation { get; set; }

    public virtual Usuario Aten_ID_UsuarioCadastroNavigation { get; set; }

    /// <summary>
    /// Relacionamento das classes
    /// </summary>

    public virtual ICollection<Atendimento_TipoAtendimento> TipoAtendimentos { get; set; } = new List<Atendimento_TipoAtendimento>();
}
