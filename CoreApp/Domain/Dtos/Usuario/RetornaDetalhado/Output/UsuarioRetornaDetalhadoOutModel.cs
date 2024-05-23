using System.ComponentModel;
using Domain.Dtos.LogGenerico.Output;
using Domain.Dtos.Usuario.Retorna.Output;

namespace Domain.Dtos.Usuario.RetornaDetalhado.Output;

/// <summary>
/// Classe DTO
/// </summary>
public class UsuarioRetornaDetalhadoOutModel
{
    /// <summary>
    /// Construtor
    /// </summary>
    public UsuarioRetornaDetalhadoOutModel() 
    {
    }

    /// <summary>
    /// Construtor
    /// </summary>
    /// <param name="usuario"></param>
    /// <param name="usuarioAfastamento"></param>
    /// <param name="permiteLog"></param>
    /// <param name="logGenericoListarLogOutModel"></param>
    /// <param name="regrasAtribuidas"></param>
    /// <param name="regrasNaoAtribuidas"></param>
    /// <param name="usuarioSolicitacao"></param>
    /// <param name="listaTiposUsuarioEdicaoBloqueada"></param>
    public UsuarioRetornaDetalhadoOutModel(Entities.Usuario usuario,
        UsuarioRetornaOutModel? usuarioAfastamento,
        bool permiteLog,
        LogGenericoListarLogOutModel? logGenericoListarLogOutModel,
        List<RegraSistema.Retorna.Output.RegraSistemaRetornaOutModel> regrasAtribuidas,
        List<RegraSistema.Retorna.Output.RegraSistemaRetornaOutModel> regrasNaoAtribuidas,
        UsuarioRetornaOutModel usuarioSolicitacao,
        int[] listaTiposUsuarioEdicaoBloqueada)
    {
        Codigo = usuario.Codigo;
        Email = usuario.Email;
        Nome = usuario.Nome;
        CodTipoUsuario = usuario.CodTipoUsuario;
        TipoUsuarioDescricao = usuario.CodTipoUsuarioNavigation.Descricao;
        DataAfastamento = usuario?.DataAfastamento;
        DataCadastro = usuario.DataCadastro;
        UsuarioCadastro = $"{usuario.CodUsuarioCadastro} - {usuario.CodUsuarioCadastroNavigation?.Nome}";
        UsuarioAfastamento = usuarioAfastamento != null ? $"{usuarioAfastamento.Codigo} - {usuarioAfastamento.Nome}" : string.Empty;
        PermiteLog = permiteLog;
        Log = new LogGenericoListarLogOutModel{ListaLogGenerico = new List<LogGenericoListarItemOutModel>()};
        RegrasAtribuidas = regrasAtribuidas;
        RegrasNaoAtribuidas = regrasNaoAtribuidas;
        EhUsuarioLogado = usuario.Codigo == usuarioSolicitacao.Codigo;
        Editavel = !listaTiposUsuarioEdicaoBloqueada.Contains(usuario.CodTipoUsuario);
    }

    /// <summary>
    /// Código do usuário retornado
    /// </summary>
    [DisplayName("Código")]
    public int Codigo { get; private set; }
    /// <summary>
    /// Email do usuário retornado
    /// </summary>
    [DisplayName("E-mail")]
    public string Email { get; private set; }
    /// <summary>
    /// Nome do usuário retornado
    /// </summary>
    [DisplayName("Nome")]
    public string Nome { get; private set; }
    /// <summary>
    /// Código do tipo usuário retornado
    /// </summary>
    [DisplayName("Cód. Tipo Usuário")]
    public int CodTipoUsuario { get; private set; }
    /// <summary>
    /// Tipo do usuário/perfil do usuário retornado
    /// </summary>
    [DisplayName("Tipo Usuário")]
    public string TipoUsuarioDescricao { get; private set; }
    /// <summary>
    /// Data de afastamento do usuário
    /// </summary>
    [DisplayName("Data de afastamento")]
    public DateTime? DataAfastamento { get; private set; }
    /// <summary>
    /// Data e hora de cadastro do usuário
    /// </summary>
    [DisplayName("Data de cadastro")]
    public DateTime DataCadastro { get; private set; }
    /// <summary>
    /// Usuário que cadastrou o usuário
    /// </summary>
    [DisplayName("Usuário de cadastro")]
    public string UsuarioCadastro { get; private set; }
    /// <summary>
    /// Usuário que realizou o afastamento do usuário
    /// </summary>
    [DisplayName("Usuário de afastamento")]
    public string UsuarioAfastamento { get; private set; }
    /// <summary>
    /// Informa se o log do usuário será exibido
    /// </summary>
    public bool PermiteLog { get; private set; }
    /// <summary>
    /// Log de alterações do usuário
    /// </summary>
    public LogGenericoListarLogOutModel Log { get; private set; }
    /// <summary>
    /// Regras de sistema vinculadas ao usuário
    /// </summary>
    public List<RegraSistema.Retorna.Output.RegraSistemaRetornaOutModel> RegrasAtribuidas { get; private set; }
    /// <summary>
    /// Regras de sistema não vinculadas ao usuário
    /// </summary>
    public List<RegraSistema.Retorna.Output.RegraSistemaRetornaOutModel> RegrasNaoAtribuidas { get; private set; }
    /// <summary>
    /// Informa se o usuário retornado é o mesmo que está logado no sistema
    /// </summary>
    public bool EhUsuarioLogado { get; private set; }
    /// <summary>
    /// Informa se o usuário é um tipo editável
    /// </summary>
    public bool Editavel { get; private set; }

}