using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TurmaAluno.Input;

public class AlunoListarLogInModel
{
    public const string CodUsuarioRequired = "O código do usuário é obrigatório para realizar a pesquisa do log.";
    public const string CodUsuarioRange = "O código do usuário é obrigatório para realizar a pesquisa do log.";
    public const string CodUsuarioSolicitacaoLogRequired = "O código do usuário que está solicitando log é obrigatório para realizar a pesquisa do log.";
    public const string CodUsuarioSolicitacaoLogRange = "O código do usuário que está solicitando log é obrigatório para realizar a pesquisa do log.";

    /// <summary>
    /// Código do usuário no qual o log de alterações será pesquisado
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuario { get; set; }

    /// <summary>
    /// Código do usuário que solicitou o log
    /// </summary>
    [Required(ErrorMessage = CodUsuarioSolicitacaoLogRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioSolicitacaoLogRange)]
    public int CodUsuarioSolicitacaoLog { get; set; }
}