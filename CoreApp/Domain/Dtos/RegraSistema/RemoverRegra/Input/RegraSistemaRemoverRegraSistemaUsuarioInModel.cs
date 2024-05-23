
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.RemoverRegra.Input;

public class RegraSistemaRemoverRegraSistemaUsuarioInModel
{
    public const string CodRegraSistemaRequired = "O código da regra de sistema é obrigatório.";
    public const string CodUsuarioRequired = "O código do usuário vinculado a regra é obrigatório.";
    public const string CodUsuarioRange = "O código do usuário vinculado a regra é obrigatório.";
    public const string CodUsuarioAltecaoRequired = "O código do usuário que está alterandoa a regra é obrigatório.";
    public const string CodUsuarioAltecaoRange = "O código do usuário que está alterandoa a regra é obrigatório.";
    public const string ValidaUsuarioRequired = "Informe se o usuário deve ser validado.";

    /// <summary>
    /// Código da regra de sistema na qual será removida do usuário
    /// </summary>
    [Required(ErrorMessage = CodRegraSistemaRequired)]
    public int CodRegraSistema { get; set; }
    /// <summary>
    /// Código do usuário no qual terá a regra de sistema removida
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuario { get; set; }
    /// <summary>
    /// Código do usuário no qual realizará a ação de remoção da regar de sistema (usuário da ação)
    /// </summary>
    [Required(ErrorMessage = CodUsuarioAltecaoRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioAltecaoRange)]
    public int CodUsuarioAltecao { get; set; }
    /// <summary>
    /// Informa se será avaliada a permissão do usuário da ação (CodUsuarioAltecao) para realizar a tarefa
    /// </summary>
    [Required(ErrorMessage = ValidaUsuarioRequired)]
    public bool ValidaUsuario { get; set; } = true;
}