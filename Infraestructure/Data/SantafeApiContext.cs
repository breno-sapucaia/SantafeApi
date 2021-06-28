using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SantafeApi.Entities;
using SantafeApi.Infraestrucutre.Data;

namespace SantafeApi.Infraestrucutre.Data
{
    public partial class SantafeApiContext : IdentityDbContext<SantafeApiUser>
    {
        public SantafeApiContext(DbContextOptions<SantafeApiContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<ControleO> ControleO { get; set; }
        public virtual DbSet<ItemsVistorium> ItemsVistoria { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Local> Locals { get; set; }
        public virtual DbSet<LocalItem> LocalItems { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Vistorium> Vistoria { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CodCliente);

                entity.ToTable("tbCliente");

                entity.Property(e => e.CnpjCliente)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.DataCad)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EnderecoCliente)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NomeCliente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TecResponsavel)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TipoDoLocal)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(c => c.CodUseruarioNavigation)
                .WithOne(b => b.ClienteNavigation)
                .HasForeignKey<SantafeApiUser>(b => b.CodCliente);



            });

            modelBuilder.Entity<ControleO>(entity =>
            {
                entity.HasKey(e => e.Cod);

                entity.ToTable("tbControleOs");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodClienteNavigation)
                    .WithMany(p => p.ControleOs)
                    .HasForeignKey(d => d.CodCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbControleOs_tbCliente");
            });

            modelBuilder.Entity<ItemsVistorium>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tbItemsVistoria");

                entity.Property(e => e.CodItemVis).ValueGeneratedOnAdd();

                entity.Property(e => e.NomeItemVis)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomeLocal)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParamItem)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.CodItem);

                entity.ToTable("tbItens");

                entity.Property(e => e.NomeItem)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Norma)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Local>(entity =>
            {
                entity.HasKey(e => e.CodLocal);

                entity.ToTable("tbLocal");

                entity.Property(e => e.NomeLocal)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LocalItem>(entity =>
            {
                entity.HasKey(e => e.CodLocalItem);

                entity.ToTable("tbLocalItem");

                entity.Property(e => e.NomeLocalItem)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tbStatus");

                entity.Property(e => e.CodStatus).ValueGeneratedOnAdd();

                entity.Property(e => e.Gravidade)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.NomeStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodItemNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbStatus_tbItens");
            });

            modelBuilder.Entity<Vistorium>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tbVistoria");

                entity.Property(e => e.Cod).ValueGeneratedOnAdd();

                entity.Property(e => e.Conformidade)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Medidas)
                    .IsRequired()
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.NomeCliente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomeImg)
                    .IsRequired()
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.NomeItem)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomeLocal)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NomeStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Param)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TipoLocal)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodControleNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodControle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbVistoria_tbControleOs1");

                entity.HasOne(d => d.CodItemNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodItem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbVistoria_tbItens");

                entity.HasOne(d => d.CodLocalNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.CodLocal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbVistoria_tbLocal");
            });

            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost, 1401;Database=DbSantaHelena;User Id=SA;Password=rootAdmin123;");
            }
        }


    }
}

