using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Assistido.Input;

public class AssistidoAdicionarInModel
{
    [Required(ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    public int IDUsuarioCadastro { get; set; }

    #region Dados de Identificação

    public string? Assi_MM_Imagem { get; set; }

    [Required(ErrorMessage = "O nome do assistido é obrigatório.")]
    [Display(Name = "Nome")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Assi_NM_Nome { get; set; }

    [Display(Name = "Sexo")]
    [StringLength(3, MinimumLength = 1, ErrorMessage = "O sexo é inválido.")]
    public string Assi_CD_Sexo { get; set; }

    [Display(Name = "RG")]
    [StringLength(14, MinimumLength = 5, ErrorMessage = "O RG é inválido.")]
    public string? Assi_NR_RG { get; set; }

    [Display(Name = "CPF")]
    [StringLength(14, MinimumLength = 14, ErrorMessage = "O CPF é inválido.")]
    public string? Assi_NR_CPF { get; set; }

    [Required(ErrorMessage = "A data de nascimento é obrigatório.")]
    [Display(Name = "Data de Nascimento")]
    [StringLength(25, MinimumLength = 10, ErrorMessage = "A data de nascimento é inválida.")]
    public string Assi_DT_Nascimento { get; set; }

    [Display(Name = "Idade")]
    [StringLength(3, MinimumLength = 1, ErrorMessage = "A idade é inválida.")]
    public string? Assi_NR_Idade { get; set; }

    [Display(Name = "Telefone (com DDD)")]
    [StringLength(15, MinimumLength = 13, ErrorMessage = "O telefone deve ter entre 13 e 15 caracteres entre números ou '(',')','-' como no exemplo: (11) 99999-9999")]
    public string? Assi_NR_Telefone { get; set; }

    #endregion

    #region Dados de Referência

    [Display(Name = "Nome da Mãe")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da mãe deve ter entre 3 e 100 caracteres.")]
    public string? Assi_NM_Mae { get; set; }

    [Display(Name = "Profissão")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "A profissão deve ter entre 3 e 100 caracteres.")]
    public string? Assi_NM_Profissao { get; set; }

    [Display(Name = "Endereço")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da mãe deve ter entre 3 e 100 caracteres.")]
    public string? Assi_NM_Endereco { get; set; }

    [Display(Name = "Tipo de Moradia")]
    [StringLength(3, MinimumLength = 1, ErrorMessage = "O informação do tipo de moradia é inválido.")]
    public string? Assi_ID_Moradia { get; set; }

    [Display(Name = "Grau de Escolaridade")]
    [StringLength(3, MinimumLength = 1, ErrorMessage = "O informação do grau de escolaridade é inválido.")]
    public string? Assi_ID_Escolaridade { get; set; }

    [Display(Name = "Estado Civil")]
    [StringLength(3, MinimumLength = 1, ErrorMessage = "O informação do estado civil é inválido.")]
    public string? Assi_ID_EstadoCivil { get; set; }

    [Display(Name = "Deficiência Física")]
    [StringLength(3, MinimumLength = 1, ErrorMessage = "O informação se possui deficiência física é inválido.")]
    public string? Assi_CD_DeficienteFisico { get; set; }

    [Display(Name = "Deficiência Mental")]
    [StringLength(3, MinimumLength = 1, ErrorMessage = "O informação se possui deficiência mental é inválida.")]
    public string? Assi_CD_DeficienteMental { get; set; }

    [Display(Name = "Quantidade de Dependentes")]
    [StringLength(3, MinimumLength = 1, ErrorMessage = "A informação se possui dependentes é inválida.")]
    public string? Assi_ID_Dependente { get; set; }

    [Display(Name = "Problemas de Saúde que Impossibilitam o Trabalho")]
    [StringLength(3, MinimumLength = 1, ErrorMessage = "A informação se o assistido possui problema de saúde é inválido.")]
    public string? Assi_CD_ImpossibilidadeTrabalho { get; set; }

    [Display(Name = "Atividade Remunerada")]
    [StringLength(3, MinimumLength = 1, ErrorMessage = "A informação se possui atividade remunerada é inválido.")]
    public string? Assi_ID_AtividadeRemunerada { get; set; }

    [Display(Name = "Códido do Score")]
    [StringLength(3, MinimumLength = 1, ErrorMessage = "O códido do score é Inválido.")]
    public string? Assi_CD_Score { get; set; }

    [Display(Name = "Número do Score")]
    [StringLength(3, MinimumLength = 1, ErrorMessage = "O número do score é inválido.")]
    public string? Assi_NR_Score { get; set; }

    #endregion

    #region Dados de Informações Extras

    [Display(Name = "Observação")]
    [StringLength(3000, ErrorMessage = "O limite da observação é de 3000 caracteres.")]
    public string? Assi_TX_Observacao { get; set; }

    #endregion
}

