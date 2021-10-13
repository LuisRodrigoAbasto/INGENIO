using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Abasto.Lib.Core.Entities;

namespace Abasto.Lib.Infrastructure.Data.Configurations
{
    public class ContratoConfiguration : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> builder)
        {
            builder.ToTable("Contrato");
            builder.HasKey(e => e.CodigoContrato);
        }
    }
}
