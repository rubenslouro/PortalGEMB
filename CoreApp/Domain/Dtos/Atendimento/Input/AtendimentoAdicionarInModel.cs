using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Atendimento.Input;

public class AtendimentoAdicionarInModel
{
    [Required(ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    public int IDUsuarioCadastro { get; set; }

    #region Dados de Idenficação

    [Required(ErrorMessage = "O código do assistido é obrigatório.")]
    [Display(Name = "Código do Assistido")]
    [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O código do assistido é obrigatório.")]
    public int Aten_ID_Assistido { get; set; }

    [Required(ErrorMessage = "O nome do assistido é obrigatório.")]
    [Display(Name = "Nome")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do assistido deve ter entre 3 e 100 caracteres.")]
    public string Aten_NM_Nome { get; set; }

    //[Required(ErrorMessage = "O tipo de atendimento é obrigatório.")]
    //[Display(Name = "Tipo de Atendimento")]
    //[StringLength(2, MinimumLength = 1, ErrorMessage = "A informação do tipo de atendimento é inválida.")]
    //public int Aten_ID_TipoAtendimento { get; set; }

    public List<CheckBoxList> ChechBoxList { get; set; }

    #endregion

    #region Dados de Informações Extras

    [StringLength(3000, ErrorMessage = "O limite da observação é de 3000 caracteres.")]
    public string? Aten_TX_Observacao { get; set; }

    #endregion
}
