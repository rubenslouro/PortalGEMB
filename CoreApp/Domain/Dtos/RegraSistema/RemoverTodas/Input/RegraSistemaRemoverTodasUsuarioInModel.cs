

using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.RegraSistema.RemoverTodas.Input;

public class RegraSistemaRemoverTodasUsuarioInModel
{
    public const string CodUsuarioRequired = "O código do usuário que terá as regras removidas é obrigatório.";
    public const string CodUsuarioRange = "O código do usuário que terá as regras removidas é obrigatório.";
    public const string CodUsuarioAlteracaoRequired = "O código do usuário que está removendo as regras é obrigatório.";
    public const string CodUsuarioAlteracaoRange = "O código do usuário que está removendo as regras é obrigatório.";
    public const string ValidaUsuarioRequired = "Informe se o usuário deve ser validado.";

    /// <summary>
    /// Código do usuário que terá todas as regras de sistema removidas
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuario { get; set; }
    /// <summary>
    /// Código do usuário que está realizando a ação
    /// </summary>
    [Required(ErrorMessage = CodUsuarioAlteracaoRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioAlteracaoRange)]
    public int CodUsuarioAlteracao { get; set; }
    /// <summary>
    /// Informa se a permissão do usuário que está removendo as regras de sistema será avaliada
    /// </summary>
    [Required(ErrorMessage = ValidaUsuarioRequired)]
    public bool ValidaUsuario { get; set; } = true;
}