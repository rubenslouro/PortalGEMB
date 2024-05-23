using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoAtendimento.Input;

public class TipoAtendimentoAdicionarInModel
{
    //[Required(ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    //[Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    //public int IDUsuarioCadastro { get; set; }

    #region Dados de Idenficação

    [Required(ErrorMessage = "O código do tipo de atendimento é obrigatório.")]
    [Display(Name = "Código")]
    [StringLength(2, MinimumLength = 1, ErrorMessage = "A informação do tipo de atendimento é inválida.")]
    public int TpAt_ID_TipoAtendimento { get; set; }

    [Required(ErrorMessage = "A descrição do tipo de atendimento é obrigatório.")]
    [Display(Name = "Descrição")]
    [StringLength(150, ErrorMessage = "O limite da descrição é de 150 caracteres.")]
    public string TpAt_NM_Descricao { get; set; }

    #endregion
}

