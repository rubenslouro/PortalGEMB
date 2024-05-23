using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Turma.Input;

public class TurmaAdicionarInModel
{
    [Required(ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    public int IDUsuarioCadastro { get; set; }

    #region Dados de Idenficação

    [Required(ErrorMessage = "O código da disciplina é obrigatório.")]
    [Display(Name = "Discilina")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O código da disciplina é obrigatório.")]
    public int Turm_ID_Disciplina { get; set; }

    //[Required(ErrorMessage = "O número da turma é obrigatório.")]
    [Display(Name = "Número da Turma")]
    //[Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O número da turma é obrigatório.")]
    public int Turm_NR_Turma { get; set; }

    [Required(ErrorMessage = "O nome da turma é obrigatório.")]
    [Display(Name = "Nome da Turma")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da turma deve ter entre 3 e 100 caracteres.")]
    public string Turm_TX_Descricao { get; set; }

    [Required(ErrorMessage = "Data de ínicio da turma é obrigatório.")]
    [Display(Name = "Data de Início")]
    [StringLength(25, MinimumLength = 10, ErrorMessage = "A data de início da turma é inválida.")]
    public string Turm_DT_Inicio { get; set; }

    [Required(ErrorMessage = "Data de fim da turma é obrigatório.")]
    [Display(Name = "Data de Fim")]
    [StringLength(25, MinimumLength = 10, ErrorMessage = "A data de fim da turma é inválida.")]
    public string Turm_DT_Final { get; set; }

    [Required(ErrorMessage = "O periodo letivo é obrigatório.")]
    [Display(Name = "Periodo Letivo")]
    public string Turm_CD_PeriodoLetivo { get; set; }

    [Required(ErrorMessage = "O ano letivo é obrigatório.")]
    [Display(Name = "Ano Letivo")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O ano letivo é obrigatório.")]
    public int Turm_NR_AnoLetivo { get; set; }

    #endregion

    #region Dados de Informações Extras

    [StringLength(3000, ErrorMessage = "O limite da observação é de 3000 caracteres.")]
    public string? Turm_TX_Observacao { get; set; }

    #endregion
}

