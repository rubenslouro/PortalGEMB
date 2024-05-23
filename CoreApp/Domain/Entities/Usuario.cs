namespace Domain.Entities;

public partial class Usuario
{
    public int Codigo { get; set; }

    public string Email { get; set; }

    public string Senha { get; set; }

    public DateTime DataCadastro { get; set; }

    public DateTime? DataAfastamento { get; set; }

    public int? CodUsuarioCadastro { get; set; }

    public string Nome { get; set; }

    public int CodTipoUsuario { get; set; }

    /// <summary>
    /// Definição de relacionamento com outras tabelas
    /// </summary>

    public virtual TipoUsuario CodTipoUsuarioNavigation { get; set; }

    public virtual Usuario CodUsuarioCadastroNavigation { get; set; }

    /// <summary>
    /// Relacionamento das classes
    /// </summary>
    /// 
    public virtual ICollection<Usuario> InverseCodUsuarioCadastroNavigation { get; set; } = new List<Usuario>();

    public virtual ICollection<LogGenerico> LogGenericos { get; set; } = new List<LogGenerico>();

    public virtual ICollection<LogUsuarioRegraSistema> LogUsuarioRegraSistemaCodUsuarioAcaoNavigations { get; set; } = new List<LogUsuarioRegraSistema>();

    public virtual ICollection<LogUsuarioRegraSistema> LogUsuarioRegraSistemaCodUsuarioNavigations { get; set; } = new List<LogUsuarioRegraSistema>();

    public virtual ICollection<TipoUsuarioRegraSistema> TipoUsuarioRegraSistemas { get; set; } = new List<TipoUsuarioRegraSistema>();

    public virtual ICollection<UsuarioRegraSistema> UsuarioRegraSistemaCodUsuarioInclusaoNavigations { get; set; } = new List<UsuarioRegraSistema>();

    public virtual ICollection<UsuarioRegraSistema> UsuarioRegraSistemaCodUsuarioNavigations { get; set; } = new List<UsuarioRegraSistema>();

    public virtual ICollection<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();

    public virtual ICollection<Turma> Turmas { get; set; } = new List<Turma>();

    public virtual ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();

    public virtual ICollection<Assistido> Assistidos { get; set; } = new List<Assistido>();

}