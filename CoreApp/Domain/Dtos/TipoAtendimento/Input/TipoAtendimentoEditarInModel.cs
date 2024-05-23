using Domain.Dtos.Atendimento.Input;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoAtendimento.Input;

public class TipoAtendimentoEditarInModel
{
    public TipoAtendimentoEditarInModel()
    {
    }

    public TipoAtendimentoEditarInModel(Entities.TipoAtendimento cadastro)
    {
        TpAt_ID_TipoAtendimento = cadastro.TpAt_ID_TipoAtendimento;
        TpAt_NM_Descricao = cadastro.TpAt_NM_Descricao;
    }

    //[Required(ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    //[Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "O código do usuário desta ação é obrigatório.")]
    //public int IDUsuarioCadastro { get; set; }

    [Display(Name = "Código")]
    [Required(ErrorMessage = "O código do atendimento a ser alterada é obrigatório.")]
    public int TpAt_ID_TipoAtendimento { get; set; }

    [Required(ErrorMessage = "A descrição do tipo de atendimento é obrigatório.")]
    [Display(Name = "Descrição")]
    [StringLength(150, ErrorMessage = "O limite da descrição é de 150 caracteres.")]
    public string TpAt_NM_Descricao { get; set; }

    //public const string CodigoRequired = "O código do tipo de usuário qie está sendo alterado é obrigatório.";
    //public const string DescricaoRequired = "A descrição do tipo de usuário é obrigatória.";
    //public const string DescricaoStringLength = "A descrição do tipo de usuário deve conter entre 3 e 20 caracteres.";
    //public const string CodUsuarioAlteracaoRequired = "O código do usuário da alteração do tipo de usuário é obrigatório.";
    //public const string CodUsuarioAlteracaoRange = "O código do usuário da alteração do tipo de usuário é obrigatório.";

    ///// <summary>
    ///// Código do tipo de usuário/perfil que será editado
    ///// </summary>
    //[Required(ErrorMessage = CodigoRequired)]
    //public string TpAt_CD_TipoAtendimento { get; set; }

    ///// <summary>
    ///// Descrição do tipo de usuário
    ///// </summary>
    //[Required(ErrorMessage = DescricaoRequired)]
    //[StringLength(20, MinimumLength = 3, ErrorMessage = DescricaoStringLength)]
    //[Description("Descrição")]
    //public string TpAt_NM_Descricao { get; set; }

    ///// <summary>
    ///// Código do usuário que está realizando a edição
    ///// </summary>
    //[Required(ErrorMessage = CodUsuarioAlteracaoRequired)]
    //[Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioAlteracaoRange)]
    //public int CodUsuarioAlteracao { get; set; }
}