

using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.UpdateDiretivasPerfilTipoUsuario.Input;

public class RegraSistemaRedefinirRegrasSistemaPadraoTipoUsuarioInModel
{
    public const string CodTipoUsuarioRequired = "O código do tipo de usuário é obrigatório para atualizar diretivas de perfil de tipo de usuário.";
    public const string codTipoUsuarioRange = "O código do tipo de usuário é obrigatório para atualizar diretivas de perfil de tipo de usuário.";
    public const string CodUsuarioAlteracaoRequired = "O código do tipo de usuário é obrigatório para atualizar diretivas de perfil de tipo de usuário.";
    public const string CodUsuarioAlteracaoRange = "O código do usuário que está executando a ação é obrigatório para atualizar diretivas de perfil de tipo de usuário.";

    /// <summary>
    /// Código do tipo usuário/perfil no qual será aplicado as regras de sistema de forma retroativa
    /// </summary>
    [Required(ErrorMessage = CodTipoUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = codTipoUsuarioRange)]
    public int CodTipoUsuario { get; set; }
    /// <summary>
    /// Código do usuário que vai realizar a ação
    /// </summary>
    [Required(ErrorMessage = CodUsuarioAlteracaoRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioAlteracaoRange)]
    public int CodUsuarioAlteracao { get; set; }
    /// <summary>
    /// Informa se será validada a permissão do usuário que realizada a ação (CodUsuarioAlteracao)
    /// </summary>
    public bool ValidarUsuario { get; set; }
}