using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Prueba.Model
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<gavAnimal> gavAnimal { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<gavAnimal>()
                .Property(e => e.aniCodigo)
                .IsUnicode(false);

            modelBuilder.Entity<gavAnimal>()
                .Property(e => e.aniEstado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<gavAnimal>()
                .Property(e => e.aniSexo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<gavAnimal>()
                .Property(e => e.empNombre)
                .IsUnicode(false);

            modelBuilder.Entity<gavAnimal>()
                .Property(e => e.camNombre)
                .IsUnicode(false);

            modelBuilder.Entity<gavAnimal>()
                .Property(e => e.razNombre)
                .IsUnicode(false);

            modelBuilder.Entity<gavAnimal>()
                .Property(e => e.catNombre)
                .IsUnicode(false);

            modelBuilder.Entity<gavAnimal>()
                .Property(e => e.troNombre)
                .IsUnicode(false);

            modelBuilder.Entity<gavAnimal>()
                .Property(e => e.GDPV)
                .HasPrecision(18, 4);
        }
    }
}
