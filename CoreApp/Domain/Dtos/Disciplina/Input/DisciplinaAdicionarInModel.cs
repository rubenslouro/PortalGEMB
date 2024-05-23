using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Disciplina.Input;

public class DisciplinaAdicionarInModel
{
    [Required(ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    public int IDUsuarioCadastro { get; set; }

    #region Dados de Idenficação

    //[Required(ErrorMessage = "O código da disciplina é obrigatório.")]
    //[Display(Name = "Código da diciplina")]
    //[Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O código da disciplina é obrigatório.")]
    //public int Disc_ID_Disciplina { get; set; }

    [Required(ErrorMessage = "O nome da disciplina é obrigatório.")]
    [Display(Name = "Nome")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da disciplina deve ter entre 3 e 100 caracteres.")]
    public string Disc_NM_Nome { get; set; }

    #endregion

    #region Dados de Informações Extras

    [Display(Name = "Observação")]
    [StringLength(3000, ErrorMessage = "O limite da observação é de 3000 caracteres.")]
    public string? Disc_TX_Observacao { get; set; }

    #endregion
}

