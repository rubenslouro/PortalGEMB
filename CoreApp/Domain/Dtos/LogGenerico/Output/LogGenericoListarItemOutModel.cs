namespace Domain.Dtos.LogGenerico.Output;

public class LogGenericoListarItemOutModel
{
    public LogGenericoListarItemOutModel(Entities.LogGenerico logGenerico)
    {
        Tabela = logGenerico.Tabela;
        Campo = logGenerico.Campo;
        ValorAnterior = logGenerico.ValorAnterior;
        ValorAlterado = logGenerico.ValorAlterado;
        DataHoraAcao = logGenerico.DataHoraAcao;
        CodUsuarioAcao = logGenerico.CodUsuarioAcao;
        UsuarioAcao = $"{logGenerico.CodUsuarioAcao} - {logGenerico.CodUsuarioAcaoNavigation.Nome}";
    }

    /// <summary>
    /// Tabelas/entidade na qual o log foi pesquisado
    /// </summary>
    public string Tabela { get; private set; }
    /// <summary>
    /// Campo chave para a pesquisa do log
    /// </summary>
    public string Campo { get; private set; }
    /// <summary>
    /// Valor encontrado antes da alteração
    /// </summary>
    public string ValorAnterior { get; private set; }
    /// <summary>
    /// Valor encontrado após a alteração
    /// </summary>
    public string ValorAlterado { get; private set; }
    /// <summary>
    /// Data e hora no qual a alteração ocorreu
    /// </summary>
    public DateTime DataHoraAcao { get; private set; }
    /// <summary>
    /// Código fo usuário do sistema que realizou a alteração
    /// </summary>
    public int CodUsuarioAcao { get; private set; }
    /// <summary>
    /// Usuário do sistema que realizou a alteração
    /// </summary>
    public string UsuarioAcao { get; private set; }

}