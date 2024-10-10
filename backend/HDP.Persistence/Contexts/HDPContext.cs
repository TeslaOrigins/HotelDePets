using System;
using System.Collections.Generic;
using HDP.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HDP.Persistence.Contexts
{
    public partial class HDPContext : DbContext
    {
        public HDPContext()
        {
        }

        public HDPContext(DbContextOptions<HDPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CuidadosEspeciais> CuidadosEspeciais { get; set; } = null!;
        public virtual DbSet<Dieta> Dieta { get; set; } = null!;
        public virtual DbSet<Hospedagem> Hospedagens { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Pet> Pets { get; set; } = null!;
        public virtual DbSet<Servico> Servicos { get; set; } = null!;
        public virtual DbSet<Tutor> Tutores { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=hdp;Username=postgres;Password=frustrafamepeste");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<CuidadosEspeciais>(entity =>
            {
                entity.HasKey(e => e.CuidadosEspeciaisId)
                    .HasName("CuidadosEspeciais_pkey");

                entity.Property(e => e.CuidadosEspeciaisId)
                    .HasColumnName("cuidadosespeciaisid")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.DescricaoUsoMedicamento).HasColumnName("descricaousomedicamento");

                entity.Property(e => e.PorcaoPorDia).HasColumnName("porcoespordia");
            });

            modelBuilder.Entity<Dieta>(entity =>
            {
                entity.HasKey(e => e.Dietaid)
                    .HasName("Dieta_pkey");

                entity.Property(e => e.Dietaid)
                    .HasColumnName("dietaid")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Hospedagemid).HasColumnName("hospedagemid");

                entity.Property(e => e.Preco)
                    .HasPrecision(12, 2)
                    .HasColumnName("preco");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.HasOne(d => d.Hospedagem)
                    .WithMany(p => p.Dietas)
                    .HasForeignKey(d => d.Hospedagemid)
                    .HasConstraintName("fk_hospedagem");

                entity.HasMany(d => d.Itens)
                    .WithMany(p => p.Dieta)
                    .UsingEntity<Dictionary<string, object>>(
                        "DietaItem",
                        l => l.HasOne<Item>().WithMany().HasForeignKey("Itemid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("DietaItem_itemid_fkey"),
                        r => r.HasOne<Dieta>().WithMany().HasForeignKey("Dietaid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("DietaItem_dietaid_fkey"),
                        j =>
                        {
                            j.HasKey("Dietaid", "Itemid").HasName("pk_dietaitem");

                            j.ToTable("DietaItem");

                            j.IndexerProperty<Guid>("Dietaid").HasColumnName("dietaid");

                            j.IndexerProperty<Guid>("Itemid").HasColumnName("itemid");
                        });
            });

            modelBuilder.Entity<Hospedagem>(entity =>
            {
                entity.ToTable("Hospedagem");

                entity.Property(e => e.Hospedagemid)
                    .HasColumnName("hospedagemid")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Datacheckin).HasColumnName("datacheckin");

                entity.Property(e => e.Datacheckout).HasColumnName("datacheckout");

                entity.Property(e => e.Datafim).HasColumnName("datafim");

                entity.Property(e => e.Datainicio).HasColumnName("datainicio");

                entity.Property(e => e.Paga).HasColumnName("paga");

                entity.Property(e => e.Petid).HasColumnName("petid");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("status");

                entity.Property(e => e.Usuarioid).HasColumnName("usuarioid");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.Hospedagens)
                    .HasForeignKey(d => d.Petid)
                    .HasConstraintName("fk_pet");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Hospedagens)
                    .HasForeignKey(d => d.Usuarioid)
                    .HasConstraintName("fk_usuario");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.Property(e => e.Itemid)
                    .HasColumnName("itemid")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");

                entity.Property(e => e.Preco)
                    .HasPrecision(12, 2)
                    .HasColumnName("preco");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(255)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("Pet");

                entity.Property(e => e.Petid)
                    .HasColumnName("petid")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Datanascimento).HasColumnName("datanascimento");

                entity.Property(e => e.Motivobloqueio)
                    .HasMaxLength(255)
                    .HasColumnName("motivobloqueio");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(255)
                    .HasColumnName("sexo");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(255)
                    .HasColumnName("tipo");

                entity.Property(e => e.Tutorid).HasColumnName("tutorid");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.Tutorid)
                    .HasConstraintName("fk_tutor");
            });

            modelBuilder.Entity<Servico>(entity =>
            {
                entity.ToTable("Servico");

                entity.Property(e => e.Servicoid)
                    .HasColumnName("servicoid")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Hospedagemid).HasColumnName("hospedagemid");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");

                entity.Property(e => e.Preco)
                    .HasPrecision(12, 2)
                    .HasColumnName("preco");

                entity.HasOne(d => d.Hospedagem)
                    .WithMany(p => p.Servicos)
                    .HasForeignKey(d => d.Hospedagemid)
                    .HasConstraintName("fk_hospedagem");
            });

            modelBuilder.Entity<Tutor>(entity =>
            {
                entity.ToTable("Tutor");

                entity.Property(e => e.Tutorid)
                    .HasColumnName("tutorid")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Bairro)
                    .HasMaxLength(255)
                    .HasColumnName("bairro");

                entity.Property(e => e.Cep)
                    .HasMaxLength(255)
                    .HasColumnName("cep");

                entity.Property(e => e.Cpf)
                    .HasMaxLength(255)
                    .HasColumnName("cpf");

                entity.Property(e => e.Datanascimento).HasColumnName("datanascimento");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.Rua)
                    .HasMaxLength(255)
                    .HasColumnName("rua");

                entity.Property(e => e.Telefone)
                    .HasMaxLength(255)
                    .HasColumnName("telefone");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Usuarioid)
                    .HasColumnName("usuarioid")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Admin).HasColumnName("admin");

                entity.Property(e => e.Cpf)
                    .HasMaxLength(11)
                    .HasColumnName("cpf");

                entity.Property(e => e.Datanascimento).HasColumnName("datanascimento");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
