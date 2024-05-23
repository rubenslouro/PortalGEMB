
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.AdicionarRegra.Input;

public class RegraSistemaAdicionarUsuarioInModel
{
    public const string CodRegraSistemaRequired = "O código da regra de sistema é obrigatório.";
    public const string CodUsuarioRequired = "O código do usuário vinculado a regra é obrigatório.";
    public const string CodUsuarioRange = "O código do usuário vinculado a regra é obrigatório.";
    public const string CodUsuarioInclusaoRequired = "O código do usuário que está incluindo a regra é obrigatório.";
    public const string CodUsuarioInclusaoRange = "O código do usuário que está incluindo a regra é obrigatório.";
    public const string ValidaUsuarioRequired = "Informe se o usuário deve ser validado.";

    /// <summary>
    /// Código da regra de sistema a ser adicionada
    /// </summary>
    [Required (ErrorMessage = CodRegraSistemaRequired)]
    public int CodRegraSistema { get; set; }
    /// <summary>
    /// Código do usuário no qual receberá a regra de sistema
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuario { get; set; }
    /// <summary>
    /// Código do usuário que está adicionando a nova regra de sistema (não confundir com o usuário que recebe a regra)
    /// </summary>
    [Required(ErrorMessage = CodUsuarioInclusaoRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioInclusaoRange)]
    public int CodUsuarioInclusao { get; set; }
    /// <summary>
    /// Informa se será avaliada a permissão do usuário que está fazendo a adição da nova regra (CodUsuarioInclusao). Caso false, a permissão será adicionada sem avaliar a ação
    /// </summary>
    [Required(ErrorMessage = ValidaUsuarioRequired)]
    public bool ValidaUsuario { get; set; } = true; 

}