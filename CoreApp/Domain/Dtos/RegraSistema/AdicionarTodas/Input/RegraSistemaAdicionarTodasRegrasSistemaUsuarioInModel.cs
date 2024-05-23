
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.AdicionarTodas.Input;

public class RegraSistemaAdicionarTodasRegrasSistemaUsuarioInModel
{
    public const string CodUsuarioRequired = "O código do usuário que receberá as regras é obrigatório.";
    public const string CodUsuarioRange = "O código do usuário que receberá as regras é obrigatório.";
    public const string CodUsuarioAlteracaoRequired = "O código do usuário que está incluindo as regras é obrigatório.";
    public const string CodUsuarioAlteracaoRange = "O código do usuário que está incluindo as regras é obrigatório.";

    /// <summary>
    /// Código do usuário que receberá as novas regras de sistema
    /// (receberá todas menos as de tipo usuário/perfil e as relativas ao cadastro de usuário)
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuario { get; set; }
    /// <summary>
    /// Código do usuário que está incluindo as novas regras de sistema (o usuário da alteração)
    /// </summary>
    [Required(ErrorMessage = CodUsuarioAlteracaoRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioAlteracaoRange)]
    public int CodUsuarioAlteracao { get; set; }

       
}