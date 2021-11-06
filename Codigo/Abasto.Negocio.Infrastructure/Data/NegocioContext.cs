using System;
using Abasto.Negocio.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Abasto.Negocio.Infrastructure.Data
{
    public partial class NegocioContext : DbContext
    {
        public NegocioContext()
        {
        }

        public NegocioContext(DbContextOptions<NegocioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IngComentario> IngComentario { get; set; }
        public virtual DbSet<IngPublicacion> IngPublicacion { get; set; }
        public virtual DbSet<IngUsuario> IngUsuario { get; set; }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");           
        }

    }
}
