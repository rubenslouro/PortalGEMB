using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.AdicionarRegraPerfil.Input;

public class RegraSistemaAdicionarRegraSistemaPerfilUsuarioInModel
{
    public const string CodRegraSistemaRequired = "O código da regra de sistema é obrigatório.";
    public const string CodTipoUsuarioRequired = "O código do tipo de usuário vinculado a regra é obrigatório.";
    public const string CodTipoUsuarioRange = "O código do tipo de usuário vinculado a regra é obrigatório.";
    public const string CodUsuarioAlteracaoRequired = "O código do usuário que está incluindo a regra é obrigatório.";
    public const string CodUsuarioAlteracaoRange = "O código do usuário que está incluindo a regra é obrigatório";
    public const string AplicaRegraRetroativaRequired = "Aplicar regra de forma retroativa?";

    /// <summary>
    /// Código da regra de sistema a ser adicionada
    /// </summary>
    [Required(ErrorMessage = CodRegraSistemaRequired)]
    public int CodRegraSistema { get; set; }
    /// <summary>
    /// Código do tipo usuário/perfil no qual receberá a nova regra de sistema
    /// </summary>
    [Required(ErrorMessage = CodTipoUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodTipoUsuarioRange)]
    public int CodTipoUsuario { get; set; }
    /// <summary>
    /// Código do usuário que estará incluindo a nova regra de sistema (usuário da ação)
    /// </summary>
    [Required(ErrorMessage = CodUsuarioAlteracaoRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioAlteracaoRange)]
    public int CodUsuarioAlteracao { get; set; }
    /// <summary>
    /// Informa se a regra será aplicada de forma retroativa a todos os tipos de usuário/perfil
    /// ou se valerá apenas aos novos usuários cadastrados
    /// </summary>
    [Required(ErrorMessage = AplicaRegraRetroativaRequired)]
    public bool AplicaRegraRetroativa { get; set; } = true;
        
}