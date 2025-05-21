using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SenaiNotes.Models;

namespace SenaiNotes.Context;

public partial class SenaiNotesDatabaseContext : DbContext
{
    public SenaiNotesDatabaseContext()
    {
    }

    private readonly IConfiguration _configuration;
    public SenaiNotesDatabaseContext(DbContextOptions<SenaiNotesDatabaseContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Anotacao> Anotacaos { get; set; }

    public virtual DbSet<AuditoriaGeral> AuditoriaGerals { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagAnotacao> TagAnotacaos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anotacao>(entity =>
        {
            entity.HasKey(e => e.IdAnotacao).HasName("PK__Anotacao__6973D8762A7E72E6");

            entity.ToTable("Anotacao", tb => tb.HasTrigger("trg_Audit_Anotacao"));

            entity.Property(e => e.IdAnotacao).HasColumnName("idAnotacao");
            entity.Property(e => e.AnotacaoArquivada).HasColumnName("anotacaoArquivada");
            entity.Property(e => e.DataCriacao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dataCriacao");
            entity.Property(e => e.DataEdicao)
                .HasColumnType("datetime")
                .HasColumnName("dataEdicao");
            entity.Property(e => e.DescricaoAnotacao).HasColumnName("descricaoAnotacao");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.ImagemAnotacao)
                .IsUnicode(false)
                .HasColumnName("imagemAnotacao");
            entity.Property(e => e.TituloAnotacao)
                .IsUnicode(false)
                .HasColumnName("tituloAnotacao");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Anotacaos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Anotacao__idUsua__619B8048");
        });

        modelBuilder.Entity<AuditoriaGeral>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Auditori__3214EC071B36AD03");

            entity.ToTable("AuditoriaGeral");

            entity.Property(e => e.DataAcao).HasColumnType("datetime");
            entity.Property(e => e.NomeTabela).HasMaxLength(128);
            entity.Property(e => e.TipoAcao).HasMaxLength(10);
            entity.Property(e => e.Usuario).HasMaxLength(100);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.IdTag).HasName("PK__Tag__020FEDB82F6A13F3");

            entity.ToTable("Tag", tb => tb.HasTrigger("trg_Audit_Tag"));

            entity.Property(e => e.IdTag).HasColumnName("idTag");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.NomeTag)
                .IsUnicode(false)
                .HasColumnName("nomeTag");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Tags)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Tag__idUsuario__6477ECF3");
        });

        modelBuilder.Entity<TagAnotacao>(entity =>
        {
            entity.HasKey(e => e.IdTagAnotacao).HasName("PK__TagAnota__CA2922495B5852CE");

            entity.ToTable("TagAnotacao", tb => tb.HasTrigger("trg_Audit_TagAnotacao"));

            entity.Property(e => e.IdTagAnotacao).HasColumnName("idTagAnotacao");
            entity.Property(e => e.IdTag).HasColumnName("idTag");

            entity.HasOne(d => d.IdAnotacaoNavigation).WithMany(p => p.TagAnotacaos)
                .HasForeignKey(d => d.IdAnotacao)
                .HasConstraintName("FK__TagAnotac__IdAno__6754599E");

            entity.HasOne(d => d.IdTagNavigation).WithMany(p => p.TagAnotacaos)
                .HasForeignKey(d => d.IdTag)
                .HasConstraintName("FK__TagAnotac__idTag__68487DD7");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A6B0D660F9");

            entity.ToTable("Usuario", tb => tb.HasTrigger("trg_Audit_Usuario"));

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.EmailUsuario)
                .IsUnicode(false)
                .HasColumnName("emailUsuario");
            entity.Property(e => e.NomeUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nomeUsuario");
            entity.Property(e => e.SenhaUsuario)
                .IsUnicode(false)
                .HasColumnName("senhaUsuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
