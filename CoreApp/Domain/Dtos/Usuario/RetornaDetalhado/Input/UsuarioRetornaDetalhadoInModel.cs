

using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Usuario.RetornaDetalhado.Input;

public class UsuarioRetornaDetalhadoInModel
{
    public const string CodUsuarioRequired = "O código do usuário é necessário para realizar a busca.";
    public const string CodUsuarioRange = "O código do usuário é necessário para realizar a busca.";
    public const string CodUsuarioSolicitacaoRequired = "O código do usuário que solicitou a consulta é obrigatório.";
    public const string CodUsuarioSolicitacaoRange = "O código do usuário que solicitou a consulta é obrigatório.";

    /// <summary>
    /// Código do usuário a ser retornado
    /// </summary>
    [Required(ErrorMessage = CodUsuarioRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioRange)]
    public int CodUsuario { get; set; }
    /// <summary>
    /// Código do usuário que solicitou a consulta
    /// </summary>
    [Required(ErrorMessage = CodUsuarioSolicitacaoRequired)]
    [Range(1, maximum: int.MaxValue, ErrorMessage = CodUsuarioSolicitacaoRange)]
    public int CodUsuarioSolicitacao { get; set; }
}