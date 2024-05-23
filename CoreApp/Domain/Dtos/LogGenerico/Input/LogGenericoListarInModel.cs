
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.LogGenerico.Input;

public class LogGenericoListarInModel
{
    public const string ReferenciaRequired = "O campo de referência é obrigatório para realizar a busca por logs.";
    public const string TabelaRequired = "A tabela é obrigatória para realizar a busca por logs.";
    public const string ReferenciaStringLength = "O campo de referência deve ter até 255 caracteres.";
    public const string TabelaStringLength = "A tabela deve ter até 255 caracteres.";

    /// <summary>
    /// Campo chave para busca na tabelas de Logs. Caso deseje recuperar alterações de um determinado'usuário', utilize
    /// aqui o campo código o qual será a chave da pesquisa e assim o sistema retonará todas as alterações
    /// do usuário referentes ao código informado. Ex: new LogGenericoListarInModel{Referencia = model.CodUsuario.ToString(), Tabela = "Usuario"}
    /// </summary>
    [Required(ErrorMessage = ReferenciaRequired)]
    [MinLength(1, ErrorMessage = ReferenciaRequired)]
    [StringLength(255, ErrorMessage = ReferenciaStringLength)]
    public string Referencia { get; set; }
    /// <summary>
    /// Tabela (entidade) na qual será realizada a busca de alterações
    /// </summary>
    [Required(ErrorMessage = TabelaRequired)]
    [StringLength(255, ErrorMessage = TabelaStringLength)]
    public string Tabela { get; set; }
}