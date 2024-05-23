
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.AdicionarTodasPerfil.Input;

public class RegraSistemaAdicionarTodasRegraSistemaPerfilUsuarioInModel
{
    public const string CodTipoUsuarioRequired = "O código do tipo usuário/perfil que receberá as regras é obrigatório.";
    public const string CodTipoUsuarioRange = "O código do tipo usuário/perfil que receberá as regras é obrigatório.";
    public const string CodUsuarioAlteracaoRequired = "O código do usuário que está adicionando as regras é obrigatório.";
    public const string CodUsuarioAlteracaoRange = "O código do usuário que está adicionando as regras é obrigatório.";
    public const string AplicaRegraRetroativaRequired = "Informe se a regra será retroativa.";

    /// <summary>
    /// Código do tipo usuário/perfil que receberá as novas regras de sistema.
    /// (Rereceberá todas menos as de tipo usuário/perfil e as relativas ao cadastro de usuário)
    /// </summary>
    [Required(ErrorMessage = CodTipoUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodTipoUsuarioRange)]
    public int CodTipoUsuario { get; set; }
    /// <summary>
    /// Código do usuário que está realizando a ação de inclusão das regras de sistema
    /// </summary>
    [Required(ErrorMessage = CodUsuarioAlteracaoRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioAlteracaoRange)]
    public int CodUsuarioAlteracao { get; set; }
    /// <summary>
    /// Informa se a adição das novas regras de sistema valerá para os usuário cadastrados
    /// ou apenas para os novos usuários criados a partir da inclusão das regras no tipo
    /// </summary>
    [Required(ErrorMessage = AplicaRegraRetroativaRequired)]
    public bool AplicaRegraRetroativa { get; set; } = true;
}