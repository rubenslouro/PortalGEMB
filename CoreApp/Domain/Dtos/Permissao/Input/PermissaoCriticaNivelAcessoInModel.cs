using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Permissao.Input;

public class PermissaoCriticaNivelAcessoInModel
{
    public const string CodUsuarioRequired = "O código do usuário é obrigatório para checagem de permissão de acesso.";
    public const string CodUsuarioRange = "O código do usuário é obrigatório para checagem de permissão de acesso.";
    public const string CodRegraSistemaRequired = "O código da regra de sistema é obrigatório para checagem de permissão de acesso.";

    /// <summary>
    /// Código do usuário no qual a permissão será avaliada
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuario { get; set; }
    /// <summary>
    /// Código da regra de sistema a qual será checada a permissão
    /// </summary>
    [Required(ErrorMessage = CodRegraSistemaRequired)]
    public int CodRegraSistema { get; set; }
}