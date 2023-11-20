using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiFinal.Models;

public partial class JuegosDbContext : DbContext
{
    public JuegosDbContext()
    {
    }

    public JuegosDbContext(DbContextOptions<JuegosDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BitacoraJuego> BitacoraJuegos { get; set; }

    public virtual DbSet<Compañia> Compañias { get; set; }

    public virtual DbSet<Juego> Juegos { get; set; }

    public virtual DbSet<Personaje> Personajes { get; set; }

    public virtual DbSet<Plataforma> Plataformas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BitacoraJuego>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bitacora__3214EC0706D7AF97");

            entity.Property(e => e.FechaMod)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Mod");
            entity.Property(e => e.Host)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Tabla)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Transaccion)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Compañia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Compañia__3214EC27ECC5F1A3");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("DelCompañia");
                    tb.HasTrigger("InsCompañia");
                    tb.HasTrigger("UpdCompañia");
                });

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Juego>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Juego__3214EC270ECBC2BA");

            entity.ToTable("Juego", tb =>
                {
                    tb.HasTrigger("DelJuego");
                    tb.HasTrigger("InsJuego");
                    tb.HasTrigger("UpdJuego");
                });

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CompañiaId).HasColumnName("CompañiaID");
            entity.Property(e => e.FechaLanzamiento).HasColumnType("date");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.Compañia).WithMany(p => p.Juegos)
                .HasForeignKey(d => d.CompañiaId)
                .HasConstraintName("FK__Juego__CompañiaI__3D5E1FD2");
        });

        modelBuilder.Entity<Personaje>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personaj__3214EC275169DF74");

            entity.ToTable("Personaje", tb =>
                {
                    tb.HasTrigger("DelPersonaje");
                    tb.HasTrigger("InsPersonaje");
                    tb.HasTrigger("UpdPersonaje");
                });

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.JuegoId).HasColumnName("JuegoID");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.Juego).WithMany(p => p.Personajes)
                .HasForeignKey(d => d.JuegoId)
                .HasConstraintName("FK__Personaje__Juego__403A8C7D");
        });

        modelBuilder.Entity<Plataforma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Platafor__3214EC27BA784FAF");

            entity.ToTable("Plataforma", tb =>
                {
                    tb.HasTrigger("DelPlataforma");
                    tb.HasTrigger("InsPlataforma");
                    tb.HasTrigger("UpdPlataforma");
                });

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC27DDFD7B3F");

            entity.ToTable("Usuario", tb =>
                {
                    tb.HasTrigger("DelUsuario");
                    tb.HasTrigger("InsUsuario");
                    tb.HasTrigger("UpdUsuario");
                });

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Contraseña).HasMaxLength(255);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(100);
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
