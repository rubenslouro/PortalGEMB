

using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ConfiguracaoGeral.Input;

public class ConfiguracaoGeralRetornarInModel
{
    public const string ErroUsuarioInvalido = "O código do usuário que está solicitando os dados deve ser informado.";

    /// <summary>
    /// Código do usuário que está solicitando o dado de configuração
    /// </summary>
    [Required(ErrorMessage = ErroUsuarioInvalido)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = ErroUsuarioInvalido)]
    public int CodUsuarioSolicitacao { get; set; }
}