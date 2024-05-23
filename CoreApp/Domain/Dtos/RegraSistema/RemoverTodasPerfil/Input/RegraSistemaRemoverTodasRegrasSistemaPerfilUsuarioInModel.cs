

using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.RemoverTodasPerfil.Input;

public class RegraSistemaRemoverTodasRegrasSistemaPerfilUsuarioInModel
{
    public const string CodTipoUsuarioRequired = "O código do tipo de usuário que terá as regras removidas é obrigatório.";
    public const string CodTipoUsuarioRange = "O código do tipo de usuário que terá as regras removidas é obrigatório.";
    public const string CodUsuarioAlteracaoRequired = "O código do usuário que está removendo as regras é obrigatório.";
    public const string CodUsuarioAlteracaoRange = "O código do usuário que está alterandoa a regra é obrigatório.";
    public const string AplicaRegraRetroativaRequired = "Aplicará de forma retroativa?";

    /// <summary>
    /// Código do tipo de usuário/perfil no qual terá todas as regras de sistemaremovidas
    /// </summary>
    [Required(ErrorMessage = CodTipoUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodTipoUsuarioRange)]
    public int CodTipoUsuario { get; set; }
    /// <summary>
    /// Código do usuário que está removendo todas as regras de sistema do perfil
    /// </summary>
    [Required(ErrorMessage = CodUsuarioAlteracaoRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioAlteracaoRange)]
    public int CodUsuarioAlteracao { get; set; }
    /// <summary>
    /// Informa de a remoção de regras do perfil/tipo usuário afetará
    /// todos os usuários relacionados a ele ou apenas os novos cadastrados apartir do momento da remoção
    /// </summary>
    [Required(ErrorMessage = AplicaRegraRetroativaRequired)]
    public bool AplicaRegraRetroativa { get; set; } = true;
}