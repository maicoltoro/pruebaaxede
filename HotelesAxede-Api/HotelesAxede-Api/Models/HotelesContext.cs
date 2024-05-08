using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HotelesAxede_Api.Models
{
    public partial class HotelesContext : DbContext
    {
        public HotelesContext()
        {
        }

        public HotelesContext(DbContextOptions<HotelesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CantidadMaximaPersona> CantidadMaximaPersonas { get; set; } = null!;
        public virtual DbSet<Sede> Sedes { get; set; } = null!;
        public virtual DbSet<Solicitude> Solicitudes { get; set; } = null!;
        public virtual DbSet<SpSolicitudes> Sp_Solicitude { get; set; } = null!;
        public virtual DbSet<Temporadum> Temporada { get; set; } = null!;
        public virtual DbSet<TpoAlojamiento> TpoAlojamientos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-ICNGR4U;Database=Hoteles;User Id=maicol;Password=maicol123; Trusted_Connection=True; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sede>(entity =>
            {
                entity.ToTable("Sede");

                entity.Property(e => e.Sede1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sede");

                entity.HasOne(d => d.IdTipoAlojaminetoNavigation)
                    .WithMany(p => p.Sedes)
                    .HasForeignKey(d => d.IdTipoAlojamineto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sede_TpoAlojamiento");
            });

            modelBuilder.Entity<Solicitude>(entity =>
            {
                entity.HasOne(d => d.IdSedeNavigation)
                    .WithMany(p => p.Solicitudes)
                    .HasForeignKey(d => d.IdSede)
                    .HasConstraintName("FK_Solicitudes_Sede");

                entity.HasOne(d => d.IdTemporadaNavigation)
                    .WithMany(p => p.Solicitudes)
                    .HasForeignKey(d => d.IdTemporada)
                    .HasConstraintName("FK_Solicitudes_Temporada");
            });

            modelBuilder.Entity<Temporadum>(entity =>
            {
                entity.Property(e => e.Temporada)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("temporada");
            });

            modelBuilder.Entity<TpoAlojamiento>(entity =>
            {
                entity.ToTable("TpoAlojamiento");

                entity.Property(e => e.Alojamiento)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
