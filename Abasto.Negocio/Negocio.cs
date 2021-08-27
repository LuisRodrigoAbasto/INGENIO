using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Abasto.Negocio
{
    public partial class Negocio : DbContext
    {
        public Negocio()
            : base("name=ConexionNegocio")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = true;
        }

        public virtual DbSet<parVentaTxn> parVentaTxn { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
