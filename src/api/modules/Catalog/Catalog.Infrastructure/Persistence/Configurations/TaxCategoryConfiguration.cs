using Finbuckle.MultiTenant;
using FSH.Starter.WebApi.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Persistence.Configurations;

internal sealed class TaxCategoryConfiguration : IEntityTypeConfiguration<TaxCategory>
{
    public void Configure(EntityTypeBuilder<TaxCategory> builder)
    {
        builder.IsMultiTenant();
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasMaxLength(10);

        builder.Property(x => x.TaxSchemeId)
               .HasMaxLength(10);

        builder.Property(x => x.TaxSchemeName)
               .HasMaxLength(50);

        builder.Property(x => x.Percent)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.Property(x => x.TaxExemptionReason)
               .HasMaxLength(200);

        builder.Property(x => x.TaxExemptionReasonCode)
               .HasMaxLength(20);
    }
}
