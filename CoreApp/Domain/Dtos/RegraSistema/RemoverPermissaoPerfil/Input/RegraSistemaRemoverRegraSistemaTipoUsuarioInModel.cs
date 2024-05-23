using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.RemoverPermissaoPerfil.Input;

public class RegraSistemaRemoverRegraSistemaTipoUsuarioInModel
{
    public const string CodRegraSistemaRequired = "O código da regra de sistema é obrigatório.";
    public const string CodTipoUsuarioRequired = "O código do tipo de usuário vinculado a regra é obrigatório.";
    public const string CodTipoUsuarioRange = "O código do tipo de usuário vinculado a regra é obrigatório.";
    public const string CodUsuarioAlteracaoRequired = "O código do usuário que está alterandoa o tipo de usuário é obrigatório.";
    public const string CodUsuarioAlteracaoRange = "O código do usuário que está alterandoa o tipo de usuário é obrigatório.";
    public const string AplicaRegraRetroativaRequired = "Aplicar de forma retroativa?";

    /// <summary>
    /// Código da regra de sistema que será removida do tipo usuário/perfil
    /// </summary>
    [Required(ErrorMessage = CodRegraSistemaRequired)]
    public int CodRegraSistema { get; set; }
    /// <summary>
    /// Código do tipo usuário/perfil no qual terá a regra de sistema removida
    /// </summary>
    [Required(ErrorMessage = CodTipoUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodTipoUsuarioRange)]
    public int CodTipoUsuario { get; set; }
    /// <summary>
    /// Código do usuário que está realizando a remoção da regra de sistema do perfil/tipo usuário
    /// </summary>
    [Required(ErrorMessage = CodUsuarioAlteracaoRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioAlteracaoRange)]
    public int CodUsuarioAlteracao { get; set; }
    /// <summary>
    /// Informa se a remoção da regra de sistema afetará todos os usuários ou apenas
    /// os que serão cadastrados após a remoção da regra de sistema
    /// </summary>
    [Required(ErrorMessage = AplicaRegraRetroativaRequired)]
    public bool AplicaRegraRetroativa { get; set; } = true;
}