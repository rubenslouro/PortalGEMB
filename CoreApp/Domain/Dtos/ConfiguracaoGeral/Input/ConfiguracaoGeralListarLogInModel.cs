using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.ConfiguracaoGeral.Input;

public class ConfiguracaoGeralListarLogInModel
{
    public const string CodUsuarioSolicitacaoLogRequired = "O código do usuário que está solicitando log é obrigatório para realizar a pesquisa do log.";
    public const string CodUsuarioSolicitacaoLogRange = "O código do usuário que está solicitando log é obrigatório para realizar a pesquisa do log.";

    /// <summary>
    /// Código do usuário que está solicitando o log
    /// </summary>
    [Required(ErrorMessage = CodUsuarioSolicitacaoLogRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioSolicitacaoLogRange)]
    public int CodUsuarioSolicitacaoLog { get; set; }
}