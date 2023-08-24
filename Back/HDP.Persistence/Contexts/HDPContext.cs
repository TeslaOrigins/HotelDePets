using Microsoft.EntityFrameworkCore;
using HDP.Domain.Models;
using HDP.Domain.Models.Constantes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HDP.Domain.Usuario;

namespace HDP.Persistence.Contexts;

public class HDPContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Caminhao> Caminhoes { get; set; }
    public DbSet<TipoCaminhao> TiposCaminhoes { get; set; }
    public DbSet<Material> Materiais { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<TipoPagamento> TiposPagamentos { get; set; }
    public DbSet<AdicionaisVenda> AdicionaisVendas { get; set; }
    public DbSet<User> Usuarios{get;set;}
    public HDPContext()
    {
    }

    public HDPContext(DbContextOptions<HDPContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

   // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseNpgsql("Host=localhost;Database=HDPLocal;Username=postgres;Password=");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Cliente>().Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
        modelBuilder.Entity<Caminhao>().Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
        modelBuilder.Entity<TipoCaminhao>().Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
        modelBuilder.Entity<Material>().Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
        modelBuilder.Entity<Venda>().Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
        modelBuilder.Entity<TipoPagamento>().Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
        modelBuilder.Entity<AdicionaisVenda>().Property(x => x.Id).HasDefaultValueSql("uuid_generate_v4()");
        modelBuilder.Entity<Caminhao>()
            .HasOne(e => e.Cliente)
            .WithMany(c => c.Caminhoes)
            .HasForeignKey(e => e.ClienteId);
        modelBuilder.Entity<Caminhao>()
            .HasOne(e => e.TipoCaminhao)
            .WithMany(c => c.Caminhoes)
            .HasForeignKey(e => e.TipoCaminhaoId);
        modelBuilder.Entity<Material>()
            .HasOne(e => e.TipoCaminhao)
            .WithMany(c => c.Materiais)
            .HasForeignKey(e => e.TipoCaminhaoId);
        modelBuilder.Entity<Venda>()
            .HasOne(e => e.AdicionaisVenda)
            .WithMany(c => c.Vendas)
            .HasForeignKey(e => e.AdicionaisVendaId);
        modelBuilder.Entity<Venda>()
            .HasOne(e => e.Caminhao)
            .WithMany(c => c.Vendas)
            .HasForeignKey(e => e.CaminhaoId);
        modelBuilder.Entity<Venda>()
            .HasOne(e => e.Cliente)
            .WithMany(c => c.Vendas)
            .HasForeignKey(e => e.ClienteId);
        modelBuilder.Entity<Venda>()
            .HasOne(e => e.Material)
            .WithMany(c => c.Vendas)
            .HasForeignKey(e => e.MaterialId);
         modelBuilder.Entity<Venda>()
            .HasOne(e => e.TipoPagamento)
            .WithMany(c => c.Vendas)
            .HasForeignKey(e => e.TipoPagamentoId);
    }
}
