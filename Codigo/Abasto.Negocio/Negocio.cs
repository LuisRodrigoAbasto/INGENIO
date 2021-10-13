using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Abasto.Negocio
{
    public partial class Negocio : DbContext
    {
        public Negocio()
            : base("name=ContextNegocio")
        {
        }

        public virtual DbSet<BCPContrado> BCPContrado { get; set; }
        public virtual DbSet<Seguridad> Seguridad { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.CodigoContrato)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.Paterno)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.Materno)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.Nombres)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.Testimonio)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.PaternoProveedor)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.MaternoProveedor)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.NombresProveedor)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.DocumentoProveedor)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.Domicilio)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.Direccion)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.Ciudad)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.NumeroDireccion)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.Importe)
                .HasPrecision(15, 2);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.Literal)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.Cuenta)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.Mes)
                .IsUnicode(false);

            modelBuilder.Entity<BCPContrado>()
                .Property(e => e.Anio)
                .IsUnicode(false);

            modelBuilder.Entity<Seguridad>()
                .Property(e => e.Usuario)
                .IsUnicode(false);

            modelBuilder.Entity<Seguridad>()
                .Property(e => e.NombreUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<Seguridad>()
                .Property(e => e.Contrasena)
                .IsUnicode(false);

            modelBuilder.Entity<Seguridad>()
                .Property(e => e.Rol)
                .IsUnicode(false);
        }
    }
}
