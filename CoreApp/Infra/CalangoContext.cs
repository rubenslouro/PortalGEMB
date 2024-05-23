using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InfraDatabase;

public partial class CalangoContext : DbContext
{
    private readonly IConfiguration _configuration;
    public CalangoContext(DbContextOptions<CalangoContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected CalangoContext()
    {
    }

    public virtual DbSet<ConfiguracaoGeral> ConfiguracaoGerals { get; set; }

    public virtual DbSet<LogGenerico> LogGenericos { get; set; }

    public virtual DbSet<LogUsuarioRegraSistema> LogUsuarioRegraSistemas { get; set; }

    public virtual DbSet<RegraSistema> RegraSistemas { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<TipoUsuarioRegraSistema> TipoUsuarioRegraSistemas { get; set; }

    public virtual DbSet<Uf> Ufs { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioRegraSistema> UsuarioRegraSistemas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        var connectinString = _configuration.GetSection("SqlServer").GetSection("ConnectionString").Value;

        optionsBuilder.UseLazyLoadingProxies().UseSqlServer(connectinString ?? throw new InvalidOperationException("ConnectionString"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //SISTEMA

        modelBuilder.Entity<ConfiguracaoGeral>(entity =>
        {
            entity.ToTable("GEMB_ConfiguracaoGeral");

            entity.HasKey(e => e.Codigo).HasName("PK_Configuracao");

            entity.Property(e => e.UrlSite)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<LogGenerico>(entity =>
        {
            entity.ToTable("GEMB_LogGenerico");

            entity.HasKey(e => e.Codigo).HasName("PK_LogGenerico");

            entity.HasIndex(e => e.CodUsuarioAcao, "IX_LogGenerico_Usuario");

            entity.Property(e => e.Campo)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Referencia)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Tabela)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.ValorAlterado).IsRequired();
            entity.Property(e => e.ValorAnterior).IsRequired();

            entity.HasOne(d => d.CodUsuarioAcaoNavigation).WithMany(p => p.LogGenericos)
                .HasForeignKey(d => d.CodUsuarioAcao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogGenerico_UsuarioAcao");
        });

        modelBuilder.Entity<LogUsuarioRegraSistema>(entity =>
        {
            entity.ToTable("GEMB_LogUsuarioRegraSistema");

            entity.HasKey(e => e.Codigo).HasName("PK_LogUsuarioRegraSistema");

            entity.HasIndex(e => e.CodRegraSistema, "IX_LogUsuarioRegraSistema_CodRegraSistema");

            entity.HasIndex(e => e.CodUsuario, "IX_LogUsuarioRegraSistema_CodUsuario");

            entity.HasIndex(e => e.CodUsuarioAcao, "IX_LogUsuarioRegraSistema_CodUsuarioAcao");

            entity.HasOne(d => d.CodRegraSistemaNavigation).WithMany(p => p.LogUsuarioRegraSistemas)
                .HasForeignKey(d => d.CodRegraSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogUsuarioRegraSistema_RegraSistema");

            entity.HasOne(d => d.CodUsuarioNavigation).WithMany(p => p.LogUsuarioRegraSistemaCodUsuarioNavigations)
                .HasForeignKey(d => d.CodUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogUsuarioRegraSistema_Usuario");

            entity.HasOne(d => d.CodUsuarioAcaoNavigation).WithMany(p => p.LogUsuarioRegraSistemaCodUsuarioAcaoNavigations)
                .HasForeignKey(d => d.CodUsuarioAcao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LogUsuarioRegraSistema_UsuarioAcao");
        });

        modelBuilder.Entity<RegraSistema>(entity =>
        {
            entity.ToTable("GEMB_RegraSistema");

            entity.HasKey(e => e.Codigo).HasName("PK_RegraSistema");

            entity.Property(e => e.Codigo).ValueGeneratedNever();
            entity.Property(e => e.Detalhamento)
                .IsRequired()
                .HasMaxLength(500);
            entity.Property(e => e.RegraSistemaDescricao)
                .IsRequired()
                .HasMaxLength(60);
        });

        modelBuilder.Entity<Uf>(entity =>
        {
            entity.ToTable("GEMB_UF");

            entity.HasKey(e => e.Codigo);

            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("GEMB_Usuario");

            entity.HasKey(e => e.Codigo).HasName("PK_Usuario");

            entity.HasIndex(e => e.CodTipoUsuario, "IX_Usuario_CodTipoUsuario");

            entity.HasIndex(e => e.CodUsuarioCadastro, "fki_f");

            entity.Property(e => e.DataAfastamento).HasColumnType("date");
            entity.Property(e => e.DataCadastro).HasColumnType("date");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(e => e.Senha)
                .IsRequired()
                .HasMaxLength(1000);

            entity.HasOne(d => d.CodTipoUsuarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.CodTipoUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_TipoUsuario");

            entity.HasOne(d => d.CodUsuarioCadastroNavigation).WithMany(p => p.InverseCodUsuarioCadastroNavigation)
                .HasForeignKey(d => d.CodUsuarioCadastro)
                .HasConstraintName("FK_Usuario_Usuario");
        });

        modelBuilder.Entity<UsuarioRegraSistema>(entity =>
        {
            entity.ToTable("GEMB_UsuarioRegraSistema");

            entity.HasKey(e => e.Codigo).HasName("PK_UsuarioRegraSistema");

            entity.HasIndex(e => e.CodRegraSistema, "IX_UsuarioRegraSistema_RegraSistema");

            entity.HasIndex(e => e.CodUsuario, "IX_UsuarioRegraSistema_Usuario");

            entity.HasIndex(e => e.CodUsuarioInclusao, "IX_UsuarioRegraSistema_UsuarioInclusao");

            entity.HasOne(d => d.CodRegraSistemaNavigation).WithMany(p => p.UsuarioRegraSistemas)
                .HasForeignKey(d => d.CodRegraSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioRegraSistema_RegraSistema");

            entity.HasOne(d => d.CodUsuarioNavigation).WithMany(p => p.UsuarioRegraSistemaCodUsuarioNavigations)
                .HasForeignKey(d => d.CodUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioRegraSistema_Usuario");

            entity.HasOne(d => d.CodUsuarioInclusaoNavigation).WithMany(p => p.UsuarioRegraSistemaCodUsuarioInclusaoNavigations)
                .HasForeignKey(d => d.CodUsuarioInclusao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsuarioRegraSistema_UsuarioInclusao");
        });

        //CADASTRO

        modelBuilder.Entity<Assistido>(entity =>
        {
            entity.ToTable("GEMB_Assistido");

            entity.HasKey(e => e.Assi_ID_Assistido)
                .HasName("PK_Assistido");

            entity.Property(e => e.Assi_MM_Imagem)
                .HasColumnType("text");

            entity.Property(e => e.Assi_NM_Nome)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.Assi_CD_Sexo)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Assi_NR_RG)
                .HasMaxLength(14)
                .IsUnicode(false);

            entity.Property(e => e.Assi_NR_CPF)
                .HasMaxLength(11)
                .IsUnicode(false);

            entity.Property(e => e.Assi_DT_Nascimento)
                .HasColumnType("datetime");

            entity.Property(e => e.Assi_NR_Idade)
                .IsUnicode(false);

            entity.Property(e => e.Assi_NM_Mae)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.Assi_NM_Profissao)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.Assi_NM_Endereco)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.Assi_NR_Telefone)
                .HasMaxLength(25)
                .IsUnicode(false);

            //entity.Property(e => e.Assi_ID_Moradia)
            //    .HasMaxLength(1)
            //    .IsUnicode(false);

            entity.HasOne(d => d.Assi_ID_TipoMoradiaCadastroNavigation)
                .WithMany(p => p.Assistidos)
                .HasForeignKey(d => d.Assi_ID_Moradia)
                .HasConstraintName("FK_Assistido_TipoMoradia");

            //entity.Property(e => e.Assi_ID_Escolaridade)
            //    .HasMaxLength(1)
            //    .IsUnicode(false);

            entity.HasOne(d => d.Assi_ID_TipoEscolaridadeCadastroNavigation)
                .WithMany(p => p.Assistidos)
                .HasForeignKey(d => d.Assi_ID_Escolaridade)
                .HasConstraintName("FK_Assistido_TipoEscolaridade");

            //entity.Property(e => e.Assi_ID_EstadoCivil)
            //    .HasMaxLength(1)
            //    .IsUnicode(false);

            entity.HasOne(d => d.Assi_ID_TipoEstadoCivilCadastroNavigation)
                .WithMany(p => p.Assistidos)
                .HasForeignKey(d => d.Assi_ID_EstadoCivil)
                .HasConstraintName("FK_Assistido_TipoEstadoCivil");

            entity.Property(e => e.Assi_CD_DeficienteFisico)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Assi_CD_DeficienteMental)
                .HasMaxLength(1)
                .IsUnicode(false);

            //entity.Property(e => e.Assi_ID_Dependente)
            //    .HasMaxLength(1)
            //    .IsUnicode(false);

            entity.HasOne(d => d.Assi_ID_TipoDependenteCadastroNavigation)
                .WithMany(p => p.Assistidos)
                .HasForeignKey(d => d.Assi_ID_Dependente)
                .HasConstraintName("FK_Assistido_TipoDependente");

            entity.Property(e => e.Assi_CD_ImpossibilidadeTrabalho)
                .HasMaxLength(1)
                .IsUnicode(false);

            //entity.Property(e => e.Assi_ID_AtividadeRemunerada)
            //    .HasMaxLength(1)
            //    .IsUnicode(false);

            entity.HasOne(d => d.Assi_ID_TipoAtividadeRemuneradaCadastroNavigation)
                .WithMany(p => p.Assistidos)
                .HasForeignKey(d => d.Assi_ID_AtividadeRemunerada)
                .HasConstraintName("FK_Assistido_TipoAtividadeRemunerada");

            entity.Property(e => e.Assi_CD_Score)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Assi_NR_Score)
                .HasMaxLength(1)
                .IsUnicode(false);

            entity.Property(e => e.Assi_TX_Observacao)
                .HasColumnType("text");

            entity.Property(e => e.Assi_DT_Cadastro)
                .HasColumnType("datetime");

            entity.HasOne(d => d.Assi_ID_UsuarioCadastroNavigation)
                .WithMany(p => p.Assistidos)
                .HasForeignKey(d => d.Assi_ID_UsuarioCadastro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Assistidos_Usuario");
        });

        modelBuilder.Entity<Atendimento>(entity =>
        {
            entity.ToTable("GEMB_Atendimento");

            entity.HasKey(e => e.Aten_ID_Atendimento)
                .HasName("PK_Atendimento");

            entity.HasOne(d => d.Aten_ID_AssistidoNavigation)
                .WithMany(p => p.Atendimentos)
                .HasForeignKey(d => d.Aten_ID_Assistido)
                .HasConstraintName("FK_Atendimento_Assistido");

            //entity.HasOne(d => d.Aten_ID_TipoAtendimentoNavigation)
            //    .WithMany(p => p.Atendimentos)
            //    .HasForeignKey(d => d.Aten_ID_TipoAtendimento)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Atendimento_TipoAtendimento");

            entity.Property(e => e.Aten_TX_Observacao)
                .HasColumnType("text");

            entity.Property(e => e.Aten_DT_Cadastro)
                .HasColumnType("datetime");

            entity.HasOne(d => d.Aten_ID_UsuarioCadastroNavigation)
                .WithMany(p => p.Atendimentos)
                .HasForeignKey(d => d.Aten_ID_UsuarioCadastro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Atendimento_Usuario");
        });

        modelBuilder.Entity<Atendimento_TipoAtendimento>(entity =>
        {
            entity.ToTable("GEMB_Atendimento_TipoAtendimento");

            entity.HasKey(e => new { e.AtTA_ID_Atendimento, e.AtTA_ID_TipoAtendimento })
                .HasName("PK_AtendimentoTipoAtendimento");

            entity.HasOne(d => d.AtTA_ID_AtendimentoNavigation)
                .WithMany(p => p.TipoAtendimentos)
                .HasForeignKey(d => d.AtTA_ID_Atendimento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AtendimentoTipoAtendimento_Atendimento");

            entity.HasOne(d => d.AtTA_ID_TipoAtendimentoNavigation)
                .WithMany(p => p.TipoAtendimentos)
                .HasForeignKey(d => d.AtTA_ID_TipoAtendimento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AtendimentoTipoAtendimento_TipoAtendimento");
        });

        modelBuilder.Entity<Disciplina>(entity =>
        {
            entity.ToTable("GEMB_Disciplina");

            entity.HasKey(e => e.Disc_ID_Disciplina)
                .HasName("PK_Disciplina");

            entity.Property(e => e.Disc_NM_Nome)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.Disc_TX_Observacao)
                .HasColumnType("text");

            entity.Property(e => e.Disc_DT_Cadastro)
                .HasColumnType("datetime");

            entity.HasOne(d => d.Disc_ID_UsuarioCadastroNavigation)
                .WithMany(p => p.Disciplinas)
                .HasForeignKey(d => d.Disc_ID_UsuarioCadastro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Disciplina_Usuario");
        });

        modelBuilder.Entity<Presenca>(entity =>
        {
            entity.ToTable("GEMB_Presenca");

            entity.HasKey(e => e.Pres_ID_Presenca)
                .HasName("PK_Presenca");

            entity.HasOne(d => d.Pres_ID_AssistidoNavigation)
                .WithMany(p => p.Presencas)
                .HasForeignKey(d => d.Pres_ID_Assistido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Presenca_Assistido");

            entity.HasOne(d => d.Pres_ID_TurmaNavigation)
                .WithMany(p => p.Presencas)
                .HasForeignKey(d => d.Pres_ID_Turma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Presenca_Turma");

            entity.Property(e => e.Pres_DT_Aula)
                .HasColumnType("datetime");

            entity.Property(e => e.Pres_CD_Presenca)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Turma>(entity =>
        {
            entity.ToTable("GEMB_Turma");

            entity.HasKey(e => e.Turm_ID_Turma)
                .HasName("PK_Turma");

            entity.HasOne(d => d.Turm_ID_DisciplinaNavigation)
                .WithMany(p => p.Turmas)
                .HasForeignKey(d => d.Turm_ID_Disciplina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Turma_Disciplina");

            entity.Property(e => e.Turm_NR_Turma)
                .IsUnicode(false);

            entity.Property(e => e.Turm_TX_Descricao)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.Turm_DT_Inicio)
                .HasColumnType("datetime");

            entity.Property(e => e.Turm_DT_Final)
                .HasColumnType("datetime");

            entity.Property(e => e.Turm_CD_PeriodoLetivo)
                .IsUnicode(false);

            entity.Property(e => e.Turm_NR_AnoLetivo)
                .HasMaxLength(4)
                .IsUnicode(false);

            entity.Property(e => e.Turm_TX_Observacao)
                .HasColumnType("text");

            entity.Property(e => e.Turm_IN_AbertaMatrícula)
                .HasColumnType("text");

            entity.HasOne(d => d.Turm_ID_UsuarioCadastroNavigation)
                .WithMany(p => p.Turmas)
                .HasForeignKey(d => d.Turm_ID_UsuarioCadastro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Turma_Usuario");
        });

        modelBuilder.Entity<TurmaAluno>(entity =>
        {
            entity.ToTable("GEMB_TurmaAluno");

            entity.Property(e => e.TuAl_ID_Turma)
                .IsUnicode(false);

            //entity.HasKey(e => new { e.TuAl_ID_Assistido, e.TuAl_CD_PeriodoLetivo, e.TuAl_NR_AnoLetivo })
            entity.HasKey(e => new { e.TuAl_ID_Assistido, e.TuAl_ID_Turma })
                .HasName("PK_TurmaAluno");

            entity.HasOne(d => d.TuAl_ID_TurmaNavigation)
                .WithMany(p => p.TurmaAlunos)
                .HasForeignKey(d => d.TuAl_ID_Turma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TurmaAluno_Turma");

            entity.HasOne(d => d.TuAl_ID_AssistidoNavigation)
                .WithMany(p => p.TurmaAlunos)
                .HasForeignKey(d => d.TuAl_ID_Assistido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TurmaAluno_Assistido");

            //entity.Property(e => e.TuAl_CD_PeriodoLetivo)
            //    .IsUnicode(false);

            //entity.Property(e => e.TuAl_NR_AnoLetivo)
            //    .HasMaxLength(4)
            //    .IsUnicode(false);
        });

        //ENUNCIADOS

        modelBuilder.Entity<TipoAtendimento>(entity =>
        {
            entity.ToTable("GEMB_TipoAtendimento");

            entity.HasKey(e => e.TpAt_ID_TipoAtendimento)
                .HasName("PK_TipoAtendimento");

            entity.Property(e => e.TpAt_NM_Descricao)
                .IsRequired()
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.Property(e => e.TpAt_ID_Disciplina)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoAtividadeRemunerada>(entity =>
        {
            entity.ToTable("GEMB_TipoAtividadeRemunerada");

            entity.HasKey(e => e.TpAR_ID_TipoAtividadeRemunerada)
                .HasName("PK_TipoAtividadeRemunerada");

            //entity.Property(e => e.TpAR_ID_TipoAtividadeRemunerada)
            //    .IsRequired()
            //    .HasMaxLength(2)
            //    .IsUnicode(false);

            entity.Property(e => e.TpAR_NM_Descricao)
                .IsRequired()
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoDependente>(entity =>
        {
            entity.ToTable("GEMB_TipoDependente");

            entity.HasKey(e => e.TpDe_ID_TipoDependente)
                .HasName("PK_TipoDependente");

            //entity.Property(e => e.TpDe_ID_TipoDependente)
            //    .IsRequired()
            //    .HasMaxLength(2)
            //    .IsUnicode(false);

            entity.Property(e => e.TpDe_NM_Descricao)
                .IsRequired()
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoEstadoCivil>(entity =>
        {
            entity.ToTable("GEMB_TipoEstadoCivil");

            entity.HasKey(e => e.TpEC_ID_TipoEstadoCivil)
                .HasName("PK_TipoEstadoCivil");

            //entity.Property(e => e.TpEC_ID_TipoEstadoCivil)
            //    .IsRequired()
            //    .HasMaxLength(2)
            //    .IsUnicode(false);

            entity.Property(e => e.TpEC_NM_Descricao)
                .IsRequired()
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoEscolaridade>(entity =>
        {
            entity.ToTable("GEMB_TipoEscolaridade");

            entity.HasKey(e => e.TpEs_ID_TipoEscolaridade)
                .HasName("PK_TipoEscolaridade");

            //entity.Property(e => e.TpEs_ID_TipoEscolaridade)
            //    .IsRequired()
            //    .HasMaxLength(2)
            //    .IsUnicode(false);

            entity.Property(e => e.TpEs_NM_Descricao)
                .IsRequired()
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoMoradia>(entity =>
        {
            entity.ToTable("GEMB_TipoMoradia");

            entity.HasKey(e => e.TpMo_ID_TipoMoradia)
                .HasName("PK_TipoMoradia");

            //entity.Property(e => e.TpMo_ID_TipoMoradia)
            //    .IsRequired()
            //    .HasMaxLength(2)
            //    .IsUnicode(false);

            entity.Property(e => e.TpMo_NM_Descricao)
                .IsRequired()
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.ToTable("GEMB_TipoUsuario");

            entity.HasKey(e => e.Codigo)
                .HasName("PK_TipoUsuario");

            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(30);
        });

        modelBuilder.Entity<TipoUsuarioRegraSistema>(entity =>
        {
            entity.ToTable("GEMB_TipoUsuarioRegraSistema");

            entity.HasKey(e => e.Codigo).
                HasName("PK_TipoUsuarioRegraSistema");

            entity.HasIndex(e => e.CodRegraSistema, "IX_TipoUsuarioRegraSistema_RegraSistema");

            entity.HasIndex(e => e.CodTipoUsuario, "IX_TipoUsuarioRegraSistema_TipoUsuario");

            entity.HasIndex(e => e.CodUsuarioInclusao, "IX_TipoUsuarioRegraSistema_UsuarioInclusao");

            entity.HasOne(d => d.CodRegraSistemaNavigation).WithMany(p => p.TipoUsuarioRegraSistemas)
                .HasForeignKey(d => d.CodRegraSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TipoUsuarioRegraSistema_RegraSistema");

            entity.HasOne(d => d.CodTipoUsuarioNavigation).WithMany(p => p.TipoUsuarioRegraSistemas)
                .HasForeignKey(d => d.CodTipoUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TipoUsuarioRegraSistema_TipoUsuario");

            entity.HasOne(d => d.CodUsuarioInclusaoNavigation).WithMany(p => p.TipoUsuarioRegraSistemas)
                .HasForeignKey(d => d.CodUsuarioInclusao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TipoUsuarioRegraSistema_UsuarioInclusao");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}