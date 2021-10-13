using Microsoft.EntityFrameworkCore;
using Abasto.Lib.Core.Entities;
using System.Reflection;

namespace Abasto.Lib.Infrastructure.Data
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

        //public virtual DbSet<Comment> Comments { get; set; }
        //public virtual DbSet<Post> Posts { get; set; }
        //public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Contrato> Contrato { get; set; }
        public virtual DbSet<Security> Securities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
