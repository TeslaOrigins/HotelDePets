using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HDP.Persistence
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

        public virtual DbSet<Alimento> Alimento { get; set; } = null!;
        public virtual DbSet<CuidadoEspecial> CuidadoEspecial { get; set; } = null!;
        public virtual DbSet<CuidadoEspecialMedicamento> CuidadoEspecialMedicamento { get; set; } = null!;
        public virtual DbSet<DietaAlimento> DietaAlimento { get; set; } = null!;
        public virtual DbSet<Dieta> Dieta { get; set; } = null!;
        public virtual DbSet<Endereco> Endereco { get; set; } = null!;
        public virtual DbSet<Hospedagem> Hospedagem { get; set; } = null!;
        public virtual DbSet<Medicamento> Medicamento { get; set; } = null!;
        public virtual DbSet<Pet> Pet { get; set; } = null!;
        public virtual DbSet<Reserva> Reserva { get; set; } = null!;
        public virtual DbSet<Servico> Servico { get; set; } = null!;
        public virtual DbSet<ServicoHospedagem> ServicoHospedagem { get; set; } = null!;
        public virtual DbSet<Tutor> Tutor { get; set; } = null!;
        public virtual DbSet<Veterinario> Veterinario { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost:5432;Database=HotelPet;Username=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Alimento>(entity =>
            {
                entity.ToTable("Alimento");

                entity.Property(e => e.AlimentoId)
                    .HasColumnName("alimentoid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DataEntrada).HasColumnName("dataentrada");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");

                entity.Property(e => e.PrecoReabastecimento).HasColumnName("precoreabastecimento");

                entity.Property(e => e.QuantidadeEstoque).HasColumnName("quantidadeestoque");
            });

            modelBuilder.Entity<CuidadoEspecial>(entity =>
            {
                entity.ToTable("CuidadoEspecial");

                entity.Property(e => e.CuidadoEspecialId)
                    .HasColumnName("cuidadoespecialid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Alergias)
                    .HasMaxLength(255)
                    .HasColumnName("alergias");

                entity.Property(e => e.CondicoesSaude)
                    .HasMaxLength(255)
                    .HasColumnName("condicoessaude");

                entity.Property(e => e.HospedagemId).HasColumnName("hospedagemid");

                entity.Property(e => e.InstrucoesEspeciais)
                    .HasMaxLength(255)
                    .HasColumnName("instrucoesespeciais");

                entity.HasOne(d => d.Hospedagem)
                    .WithMany(p => p.CuidadoEspecials)
                    .HasForeignKey(d => d.HospedagemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_hospedagem");
            });

            modelBuilder.Entity<CuidadoEspecialMedicamento>(entity =>
            {
                entity.HasKey(e => e.CemId)
                    .HasName("CuidadoEspecialMedicamento_pkey");

                entity.ToTable("CuidadoEspecialMedicamento");

                entity.Property(e => e.CemId)
                    .HasColumnName("cemid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CuidadoespecialId).HasColumnName("cuidadoespecialid");

                entity.Property(e => e.MedicamentoId).HasColumnName("medicamentoid");

                entity.HasOne(d => d.Cuidadoespecial)
                    .WithMany(p => p.CuidadoEspecialMedicamentos)
                    .HasForeignKey(d => d.CuidadoespecialId)
                    .HasConstraintName("CuidadoEspecialMedicamento_cuidadoespecialid_fkey");

                entity.HasOne(d => d.Medicamento)
                    .WithMany(p => p.CuidadoEspecialMedicamentos)
                    .HasForeignKey(d => d.MedicamentoId)
                    .HasConstraintName("CuidadoEspecialMedicamento_MedicamentoId_fkey");
            });

            modelBuilder.Entity<DietaAlimento>(entity =>
            {
                entity.HasKey(e => new { e.DietaId, e.AlimentoId })
                    .HasName("pk_dietaalimento");

                entity.ToTable("DietaAlimento");

                entity.Property(e => e.DietaId).HasColumnName("dietaid");

                entity.Property(e => e.AlimentoId).HasColumnName("alimentoid");

                entity.Property(e => e.DietaAlimentoId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("dietaalimentoid")
                    .UseIdentityAlwaysColumn();

                entity.HasOne(d => d.Alimento)
                    .WithMany(p => p.DietaAlimentos)
                    .HasForeignKey(d => d.AlimentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DietaAlimento_AlimentoId_fkey");

                entity.HasOne(d => d.Dieta)
                    .WithMany(p => p.DietaAlimentos)
                    .HasForeignKey(d => d.DietaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DietaAlimento_DietaId_fkey");
            });

            modelBuilder.Entity<Dieta>(entity =>
            {
                entity.HasKey(e => e.DietaId)
                    .HasName("Dieta_pkey");

                entity.Property(e => e.DietaId)
                    .HasColumnName("dietaid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.HorarioAlimentacao)
                    .HasColumnName("horarioAlimentacao")
                    .HasColumnType("timestamp with time zone");
                
                entity.Property(e => e.Observacoes)
                    .HasMaxLength(255)
                    .HasColumnName("observacoes");

                entity.Property(e => e.PetId).HasColumnName("petid");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.Dieta)
                    .HasForeignKey(d => d.PetId)
                    .HasConstraintName("fk_pet");
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.ToTable("Endereco");

                entity.Property(e => e.EnderecoId)
                    .HasColumnName("enderecoid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Cidade)
                    .HasMaxLength(255)
                    .HasColumnName("cidade");

                entity.Property(e => e.Estado)
                    .HasMaxLength(255)
                    .HasColumnName("estado");

                entity.Property(e => e.Logradouro)
                    .HasMaxLength(255)
                    .HasColumnName("logradouro");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.TutorId).HasColumnName("tutorid");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.Enderecos)
                    .HasForeignKey(d => d.TutorId)
                    .HasConstraintName("fk_tutor");
            });

            modelBuilder.Entity<Hospedagem>(entity =>
            {
                entity.ToTable("Hospedagem");

                entity.Property(e => e.HospedagemId)
                    .HasColumnName("hospedagemid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CheckIn).HasColumnName("checkin");

                entity.Property(e => e.DataEntrada).HasColumnName("dataentrada");

                entity.Property(e => e.DataSaida).HasColumnName("datasaida");

                entity.Property(e => e.Observacoes)
                    .HasMaxLength(255)
                    .HasColumnName("observacoes");

                entity.Property(e => e.PetId).HasColumnName("petid");

                entity.Property(e => e.PrecoHospedagem).HasColumnName("precohospedagem");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.Hospedagems)
                    .HasForeignKey(d => d.PetId)
                    .HasConstraintName("fk_pet");
            });

            modelBuilder.Entity<Medicamento>(entity =>
            {
                entity.ToTable("Medicamento");

                entity.Property(e => e.MedicamentoId)
                    .HasColumnName("medicamentoid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DataEntrada).HasColumnName("dataentrada");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");

                entity.Property(e => e.Preco).HasColumnName("preco");
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.ToTable("Pet");

                entity.Property(e => e.PetId)
                    .HasColumnName("petid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Especie)
                    .HasMaxLength(255)
                    .HasColumnName("especie");

                entity.Property(e => e.FotoUrl)
                    .HasMaxLength(255)
                    .HasColumnName("fotourl");

                entity.Property(e => e.Idade).HasColumnName("idade");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");

                entity.Property(e => e.Peso).HasColumnName("peso");

                entity.Property(e => e.Raca)
                    .HasMaxLength(255)
                    .HasColumnName("raca");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(255)
                    .HasColumnName("sexo");

                entity.Property(e => e.TutorId).HasColumnName("tutorid");

                entity.HasOne(d => d.Tutor)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.TutorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_tutor");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.ToTable("Reserva");

                entity.Property(e => e.ReservaId)
                    .HasColumnName("reservaid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DataReserva).HasColumnName("datareserva");

                entity.Property(e => e.HospedagemId).HasColumnName("hospedagemid");

                entity.HasOne(d => d.Hospedagem)
                    .WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.HospedagemId)
                    .HasConstraintName("Reserva_HospedagemId_fkey");
            });

            modelBuilder.Entity<Servico>(entity =>
            {
                entity.ToTable("Servico");

                entity.Property(e => e.ServicoId)
                    .HasColumnName("servicoid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.DataServico).HasColumnName("dataservico");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");

                entity.Property(e => e.Preco).HasColumnName("preco");
            });

            modelBuilder.Entity<ServicoHospedagem>(entity =>
            {
                entity.ToTable("ServicoHospedagem");

                entity.Property(e => e.ServicoHospedagemId)
                    .HasColumnName("servicohospedagemid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.HospedagemId).HasColumnName("hospedagemid");

                entity.Property(e => e.ServicoId).HasColumnName("servicoid");

                entity.HasOne(d => d.Hospedagem)
                    .WithMany(p => p.ServicoHospedagems)
                    .HasForeignKey(d => d.HospedagemId)
                    .HasConstraintName("ServicoHospedagem_HospedagemId_fkey");

                entity.HasOne(d => d.Servico)
                    .WithMany(p => p.ServicoHospedagems)
                    .HasForeignKey(d => d.ServicoId)
                    .HasConstraintName("ServicoHospedagem_ServicoId_fkey");
            });

            modelBuilder.Entity<Tutor>(entity =>
            {
                entity.ToTable("Tutor");

                entity.Property(e => e.TutorId)
                    .HasColumnName("tutorid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Cpf)
                    .HasMaxLength(255)
                    .HasColumnName("cpf");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");

                entity.Property(e => e.NomeNormalizado)
                    .HasMaxLength(255)
                    .HasColumnName("nomenormalizado");

                entity.Property(e => e.Telefone)
                    .HasMaxLength(255)
                    .HasColumnName("telefone");
            });

            modelBuilder.Entity<Veterinario>(entity =>
            {
                entity.ToTable("Veterinario");

                entity.Property(e => e.VeterinarioId)
                    .HasColumnName("veterinarioid")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .HasColumnName("nome");

                entity.Property(e => e.PetId).HasColumnName("petid");

                entity.Property(e => e.Telefone)
                    .HasMaxLength(255)
                    .HasColumnName("telefone");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.Veterinarios)
                    .HasForeignKey(d => d.PetId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_pet");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
