
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.TipoUsuario.ListarLog.Input;

public class TipoUsuarioListarLogInModel
{
    public const string CodTipoUsuarioRequired = "O tipo do usuário é obrigatório para realizar a pesquisa do log.";
    public const string CodTipoUsuarioRange = "O tipo do usuário é obrigatório para realizar a pesquisa do log.";
    public const string CodUsuarioSolicitacaoLogRequired = "O usuário que está solicitando log é obrigatório para realizar a pesquisa do log.";
    public const string CodUsuarioSolicitacaoLogRange = "O usuário que está solicitando log é obrigatório para realizar a pesquisa do log.";

    /// <summary>
    /// Código do tipo de usuário que será realizada a pesquisa de Log
    /// </summary>
    [Required(ErrorMessage = CodTipoUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodTipoUsuarioRange)]
    public int CodTipoUsuario { get; set; }
    /// <summary>
    /// Código do usuário que está realizando a pesquisa de log
    /// </summary>
    [Required(ErrorMessage = CodUsuarioSolicitacaoLogRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioSolicitacaoLogRange)]
    public int CodUsuarioSolicitacaoLog { get; set; }
}