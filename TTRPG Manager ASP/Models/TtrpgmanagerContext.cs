using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TTRPG_Manager_ASP.Models;

public partial class TtrpgmanagerContext : DbContext
{
    public TtrpgmanagerContext()
    {
    }

    public TtrpgmanagerContext(DbContextOptions<TtrpgmanagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aventura> Aventuras { get; set; }

    public virtual DbSet<Campana> Campanas { get; set; }

    public virtual DbSet<Entrada> Entrada { get; set; }

    public virtual DbSet<ManualReglas> ManualesReglas { get; set; }

    public virtual DbSet<Partida> Partida { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Personaje> Personajes { get; set; }

    public virtual DbSet<Sistema> Sistemas { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aventura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_aventura");

            entity.ToTable("aventura");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EnProceso).HasColumnName("en_proceso");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdCampana).HasColumnName("id_campana");
            entity.Property(e => e.Imagen)
                .HasColumnType("image")
                .HasColumnName("imagen");
            entity.Property(e => e.ListaEtiquetas)
                .HasMaxLength(1)
                .HasColumnName("lista_etiquetas");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdCampanaNavigation).WithMany(p => p.Aventuras)
                .HasForeignKey(d => d.IdCampana)
                .HasConstraintName("fk_aventura_campana");

            entity.HasMany(d => d.ManualReglas).WithMany(p => p.Aventuras)
                .UsingEntity<Dictionary<string, object>>(
                    "AventuraManual",
                    r => r.HasOne<ManualReglas>().WithMany()
                        .HasForeignKey("ManualReglas")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_aventura-manual_manual"),
                    l => l.HasOne<Aventura>().WithMany()
                        .HasForeignKey("Aventura")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_aventura-manual_aventura"),
                    j =>
                    {
                        j.HasKey("Aventura", "ManualReglas").HasName("pk_aventura-manual");
                        j.ToTable("aventura_manual");
                        j.IndexerProperty<int>("Aventura").HasColumnName("aventura");
                        j.IndexerProperty<int>("ManualReglas").HasColumnName("manual_reglas");
                    });
        });

        modelBuilder.Entity<Campana>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_campana");

            entity.ToTable("campana");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EnProceso).HasColumnName("en_proceso");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Entrada>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_entrada");

            entity.ToTable("entrada");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Aventura).HasColumnName("aventura");
            entity.Property(e => e.Contenido)
                .HasMaxLength(1)
                .HasColumnName("contenido");
            entity.Property(e => e.EntradaPadre).HasColumnName("entrada_padre");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Tipo)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("tipo");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titulo");

            entity.HasOne(d => d.AventuraNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.Aventura)
                .HasConstraintName("fk_entrada_aventura");

            entity.HasOne(d => d.EntradaPadreNavigation).WithMany(p => p.InverseEntradaPadreNavigation)
                .HasForeignKey(d => d.EntradaPadre)
                .HasConstraintName("fk_entrada_entrada");
        });

        modelBuilder.Entity<ManualReglas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_manual");

            entity.ToTable("manual_reglas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Archivo)
                .HasMaxLength(1)
                .HasColumnName("archivo");
            entity.Property(e => e.DicBookmarks)
                .HasMaxLength(1)
                .HasColumnName("dic_bookmarks");
            entity.Property(e => e.EdicionSistema)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("edicion_sistema");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.NombreSistema)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("nombre_sistema");
            entity.Property(e => e.Tipo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tipo");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titulo");

            entity.HasOne(d => d.Sistema).WithMany(p => p.ManualReglas)
                .HasForeignKey(d => new { d.NombreSistema, d.EdicionSistema })
                .HasConstraintName("fk_manual_sistema");
        });

        modelBuilder.Entity<Partida>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.NumPartida }).HasName("pk_partida");

            entity.ToTable("partida");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NumPartida).HasColumnName("num_partida");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.Partida)
                .HasForeignKey(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_partida_entrada");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_persona");

            entity.ToTable("persona");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apodo)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apodo");
            entity.Property(e => e.Contacto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contacto");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Etiquetas)
                .HasMaxLength(1)
                .HasColumnName("etiquetas");
            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Objetivo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("objetivo");
        });

        modelBuilder.Entity<Personaje>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_personaje");

            entity.ToTable("personaje");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Arquetipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("arquetipo");
            entity.Property(e => e.DicPuntos)
                .HasMaxLength(1)
                .HasColumnName("dic_puntos");
            entity.Property(e => e.EsJugable).HasColumnName("es_jugable");
            entity.Property(e => e.Ficha)
                .HasMaxLength(1)
                .HasColumnName("ficha");
            entity.Property(e => e.Imagen)
                .HasColumnType("image")
                .HasColumnName("imagen");
            entity.Property(e => e.Motivacion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("motivacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PersonaJugadora).HasColumnName("persona_jugadora");

            entity.HasOne(d => d.PersonaJugadoraNavigation).WithMany(p => p.Personajes)
                .HasForeignKey(d => d.PersonaJugadora)
                .HasConstraintName("fk_persona");

            entity.HasMany(d => d.IdAventuras).WithMany(p => p.IdPersonajes)
                .UsingEntity<Dictionary<string, object>>(
                    "PersonajeAventura",
                    r => r.HasOne<Aventura>().WithMany()
                        .HasForeignKey("IdAventura")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_aventura"),
                    l => l.HasOne<Personaje>().WithMany()
                        .HasForeignKey("IdPersonaje")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_personaje"),
                    j =>
                    {
                        j.HasKey("IdPersonaje", "IdAventura").HasName("pk_personaje-aventura");
                        j.ToTable("personaje_aventura");
                        j.IndexerProperty<int>("IdPersonaje").HasColumnName("id_personaje");
                        j.IndexerProperty<int>("IdAventura").HasColumnName("id_aventura");
                    });
        });

        modelBuilder.Entity<Sistema>(entity =>
        {
            entity.HasKey(e => new { e.Nombre, e.Edicion }).HasName("pk_sistema");

            entity.ToTable("sistema");

            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Edicion)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("edicion");
            entity.Property(e => e.Clase)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("clase");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.ListaEtiquetas)
                .HasMaxLength(1)
                .HasColumnName("lista_etiquetas");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
