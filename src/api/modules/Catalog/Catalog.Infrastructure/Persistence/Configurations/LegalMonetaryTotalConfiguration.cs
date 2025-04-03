using Finbuckle.MultiTenant;
using FSH.Starter.WebApi.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Persistence.Configurations;

internal sealed class LegalMonetaryTotalConfiguration : IEntityTypeConfiguration<LegalMonetaryTotal>
{
    public void Configure(EntityTypeBuilder<LegalMonetaryTotal> builder)
    {
        builder.IsMultiTenant();
        builder.HasKey(x => x.Id);

        builder.Property(x => x.LineExtensionAmount)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(x => x.TaxExclusiveAmount)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(x => x.TaxInclusiveAmount)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(x => x.AllowanceTotalAmount)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(x => x.PrepaidAmount)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(x => x.PayableAmount)
               .HasPrecision(18, 2)
               .IsRequired();
    }
}
