using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoUsuario.Criar.Input;

public class TipoUsuarioAdicionarInModel
{
    public const string DescricaoRequired = "A descrição do tipo de usuário é obrigatória.";
    public const string DescricaoStringLength = "A descrição do tipo de usuário deve conter entre 3 e 20 caracteres.";
    public const string CodUsuarioInclusaoRequired = "O código do usuário da inclusão do tipo de usuário é obrigatório.";

    /// <summary>
    /// Descrição do novo tipo de usuário/perfil a ser criado
    /// </summary>
    [Required(ErrorMessage = DescricaoRequired)]
    [StringLength(20, MinimumLength = 3, ErrorMessage = DescricaoStringLength)]
    [Description("Descrição")]
    public string Descricao { get; set; }
    /// <summary>
    /// Código do usuário que está realizado a inclusão
    /// </summary>
    [Required(ErrorMessage = CodUsuarioInclusaoRequired)]
    public int CodUsuarioInclusao { get; set; }
}