using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public partial class Assistido
{
    /// <summary>
    /// ID
    /// </summary>
    [Display(Name = "Código")]
    public int Assi_ID_Assistido { get; set; }

    /// <summary>
    /// Imagem do assistido
    /// </summary>
    [Display(Name = "Imagem")]
    public string? Assi_MM_Imagem { get; set; }

    /// <summary>
    /// Nome do Assistido
    /// </summary>
    [Display(Name = "Nome")]
    public string Assi_NM_Nome { get; set; }

    /// <summary>
    /// Sexo
    /// </summary>
    [Display(Name = "Sexo")]
    public string Assi_CD_Sexo { get; set; }

    /// <summary>
    /// RG
    /// </summary>
    [Display(Name = "RG")]
    public string? Assi_NR_RG { get; set; }

    /// <summary>
    /// CPF
    /// </summary>
    [Display(Name = "CPF")]
    public string? Assi_NR_CPF { get; set; }

    /// <summary>
    /// Data de nascimento
    /// </summary>
    [Display(Name = "Data de Nascimento")]
    public DateTime Assi_DT_Nascimento { get; set; }

    /// <summary>
    /// Idade
    /// </summary>
    [Display(Name = "Idade")]
    public int? Assi_NR_Idade { get; set; }

    /// <summary>
    /// Telefone
    /// </summary>
    [Display(Name = "Telefone")]
    public string? Assi_NR_Telefone { get; set; }

    /// <summary>
    /// Nome da Mãe
    /// </summary>
    [Display(Name = "Nome da Mãe")]
    public string? Assi_NM_Mae { get; set; }

    /// <summary>
    /// Profissão
    /// </summary>
    [Display(Name = "Profissão")]
    public string? Assi_NM_Profissao { get; set; }

    /// <summary>
    /// Endereço
    /// </summary>
    [Display(Name = "Endereço")]
    public string? Assi_NM_Endereco { get; set; }

    /// <summary>
    /// Tipo de Moradia
    /// </summary>
    [Display(Name = "Moraria")]
    public int? Assi_ID_Moradia { get; set; }

    /// <summary>
    /// Tipo de Escolaridade
    /// </summary>
    [Display(Name = "Escolaridade")]
    public int? Assi_ID_Escolaridade { get; set; }

    /// <summary>
    /// Tipo do Estado 
    /// </summary>
    [Display(Name = "Estado Civil")]
    public int? Assi_ID_EstadoCivil { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Display(Name = "Deficiente Físico")]
    public string? Assi_CD_DeficienteFisico { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Display(Name = "Deficiente Mental")]
    public string? Assi_CD_DeficienteMental { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Display(Name = "Qtde. Dependentes")]
    public int? Assi_ID_Dependente { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Display(Name = "Impossibidade de Trabalhar")]
    public string? Assi_CD_ImpossibilidadeTrabalho { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Display(Name = "Atividade Remunerada")]
    public int? Assi_ID_AtividadeRemunerada { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Display(Name = "Cod. Score")]
    public string? Assi_CD_Score { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Display(Name = "Número Score")]
    public int? Assi_NR_Score { get; set; }

    /// <summary>
    /// Observações para o cadastro
    /// </summary>
    [Display(Name = "Observação")]
    public string? Assi_TX_Observacao { get; set; }

    /// <summary>
    /// Usuário que efetuou o cadastro
    /// </summary>
    public int Assi_ID_UsuarioCadastro { get; set; }

    /// <summary>
    /// Data de cadastro do assistido
    /// </summary>
    public DateTime Assi_DT_Cadastro { get; set; }

    /// <summary>
    /// Definição de relacionamento com outras tabelas
    /// </summary>

    public virtual Usuario Assi_ID_UsuarioCadastroNavigation { get; set; }

    public virtual TipoAtividadeRemunerada Assi_ID_TipoAtividadeRemuneradaCadastroNavigation { get; set; }

    public virtual TipoDependente Assi_ID_TipoDependenteCadastroNavigation { get; set; }

    public virtual TipoEscolaridade Assi_ID_TipoEscolaridadeCadastroNavigation { get; set; }

    public virtual TipoEstadoCivil Assi_ID_TipoEstadoCivilCadastroNavigation { get; set; }

    public virtual TipoMoradia Assi_ID_TipoMoradiaCadastroNavigation { get; set; }

    /// <summary>
    /// Relacionamento das classes
    /// </summary>

    public virtual ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();

    public virtual ICollection<Presenca> Presencas { get; set; } = new List<Presenca>();

    public virtual ICollection<TurmaAluno> TurmaAlunos { get; set; } = new List<TurmaAluno>();
}

public partial class Assistido_Visualizar
{
    /// <summary>
    /// ID
    /// </summary>
    [Display(Name = "Código")]
    public int Assi_ID_Assistido { get; set; }

    /// <summary>
    /// Imagem do assistido
    /// </summary>
    [Display(Name = "Imagem")]
    public string? Assi_MM_Imagem { get; set; }

    /// <summary>
    /// Nome do Assistido
    /// </summary>
    [Display(Name = "Nome")]
    public string Assi_NM_Nome { get; set; }

    /// <summary>
    /// Sexo
    /// </summary>
    [Display(Name = "Sexo")]
    public string Assi_CD_Sexo { get; set; }

    /// <summary>
    /// RG
    /// </summary>
    [Display(Name = "RG")]
    public string? Assi_NR_RG { get; set; }

    /// <summary>
    /// CPF
    /// </summary>
    [Display(Name = "CPF")]
    public string? Assi_NR_CPF { get; set; }

    /// <summary>
    /// Data de nascimento
    /// </summary>
    [Display(Name = "Data de Nascimento")]
    public DateTime Assi_DT_Nascimento { get; set; }

    /// <summary>
    /// Idade
    /// </summary>
    [Display(Name = "Idade")]
    public int? Assi_NR_Idade { get; set; }

    /// <summary>
    /// Telefone
    /// </summary>
    [Display(Name = "Telefone")]
    public string? Assi_NR_Telefone { get; set; }

    /// <summary>
    /// Nome da Mãe
    /// </summary>
    [Display(Name = "Nome da Mãe")]
    public string? Assi_NM_Mae { get; set; }

    /// <summary>
    /// Profissão
    /// </summary>
    [Display(Name = "Profissão")]
    public string? Assi_NM_Profissao { get; set; }

    /// <summary>
    /// Endereço
    /// </summary>
    [Display(Name = "Endereço")]
    public string? Assi_NM_Endereco { get; set; }

    /// <summary>
    /// Descrição da Moradia
    /// </summary>
    [Display(Name = "Moraria")]
    public string? Assi_TX_Moradia { get; set; }

    /// <summary>
    /// Descrição da Escolaridade
    /// </summary>
    [Display(Name = "Escolaridade")]
    public string? Assi_TX_Escolaridade { get; set; }

    /// <summary>
    /// Descrição da Estado Civil
    /// </summary>
    [Display(Name = "Estado Civil")]
    public string? Assi_TX_EstadoCivil { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Display(Name = "Deficiente Físico")]
    public string? Assi_CD_DeficienteFisico { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Display(Name = "Deficiente Mental")]
    public string? Assi_CD_DeficienteMental { get; set; }

    /// <summary>
    /// Descrição da Quantidade de Dependentes
    /// </summary>
    [Display(Name = "Qtde. Dependentes")]
    public string? Assi_TX_Dependente { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Display(Name = "Impossibidade de Trabalhar")]
    public string? Assi_CD_ImpossibilidadeTrabalho { get; set; }

    /// <summary>
    /// Descrição da Atividade Remunerada
    /// </summary>
    [Display(Name = "Atividade Remunerada")]
    public string? Assi_TX_AtividadeRemunerada { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Display(Name = "Cod. Score")]
    public string? Assi_CD_Score { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Display(Name = "Número Score")]
    public int? Assi_NR_Score { get; set; }

    /// <summary>
    /// Observações para o cadastro
    /// </summary>
    [Display(Name = "Observação")]
    public string? Assi_TX_Observacao { get; set; }

    /// <summary>
    /// Usuário que efetuou o cadastro
    /// </summary>
    public int Assi_ID_UsuarioCadastro { get; set; }

    /// <summary>
    /// Data de cadastro do assistido
    /// </summary>
    public DateTime Assi_DT_Cadastro { get; set; }

    ///// <summary>
    ///// Definição de relacionamento com outras tabelas
    ///// </summary>

    //public virtual Usuario Assi_ID_UsuarioCadastroNavigation { get; set; }

    //public virtual TipoAtividadeRemunerada Assi_ID_TipoAtividadeRemuneradaCadastroNavigation { get; set; }

    //public virtual TipoDependente Assi_ID_TipoDependenteCadastroNavigation { get; set; }

    //public virtual TipoEscolaridade Assi_ID_TipoEscolaridadeCadastroNavigation { get; set; }

    //public virtual TipoEstadoCivil Assi_ID_TipoEstadoCivilCadastroNavigation { get; set; }

    //public virtual TipoMoradia Assi_ID_TipoMoradiaCadastroNavigation { get; set; }

    ///// <summary>
    ///// Relacionamento das classes
    ///// </summary>

    //public virtual ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();

    //public virtual ICollection<Presenca> Presencas { get; set; } = new List<Presenca>();

    //public virtual ICollection<TurmaAluno> TurmaAlunos { get; set; } = new List<TurmaAluno>();
}

public partial class Assistido_Presenca
{
    /// <summary>
    /// ID
    /// </summary>
    [Display(Name = "Código")]
    public int Assi_ID_Assistido { get; set; }

    /// <summary>
    /// Nome do Assistido
    /// </summary>
    [Display(Name = "Nome")]
    public string Assi_NM_Nome { get; set; }

}
