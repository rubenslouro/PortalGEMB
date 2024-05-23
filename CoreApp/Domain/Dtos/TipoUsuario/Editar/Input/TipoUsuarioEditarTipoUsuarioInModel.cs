using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoUsuario.Editar.Input;

public class TipoUsuarioEditarTipoUsuarioInModel
{
    public const string CodigoRequired = "O código do tipo de usuário qie está sendo alterado é obrigatório.";
    public const string DescricaoRequired = "A descrição do tipo de usuário é obrigatória.";
    public const string DescricaoStringLength = "A descrição do tipo de usuário deve conter entre 3 e 20 caracteres.";
    public const string CodUsuarioAlteracaoRequired = "O código do usuário da alteração do tipo de usuário é obrigatório.";
    public const string CodUsuarioAlteracaoRange = "O código do usuário da alteração do tipo de usuário é obrigatório.";

    /// <summary>
    /// Código do tipo de usuário/perfil que será editado
    /// </summary>
    [Required(ErrorMessage = CodigoRequired)]
    public int Codigo { get; set; }
    /// <summary>
    /// Descrição do tipo de usuário
    /// </summary>
    [Required(ErrorMessage = DescricaoRequired)]
    [StringLength(20, MinimumLength = 3, ErrorMessage = DescricaoStringLength)]
    [Description("Descrição")]
    public string Descricao { get; set; }
    /// <summary>
    /// Código do usuário que está realizando a edição
    /// </summary>
    [Required(ErrorMessage = CodUsuarioAlteracaoRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioAlteracaoRange)]
    public int CodUsuarioAlteracao { get; set; }
}