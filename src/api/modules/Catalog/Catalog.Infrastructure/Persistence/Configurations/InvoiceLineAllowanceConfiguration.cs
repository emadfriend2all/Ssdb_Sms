using Finbuckle.MultiTenant;
using FSH.Starter.WebApi.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Persistence.Configurations;

internal sealed class InvoiceLineAllowanceConfiguration : IEntityTypeConfiguration<InvoiceLineAllowance>
{
    public void Configure(EntityTypeBuilder<InvoiceLineAllowance> builder)
    {
        builder.IsMultiTenant();
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ChargeIndicator).IsRequired();
        builder.Property(x => x.AllowanceChargeReason)
               .HasMaxLength(200)
               .IsRequired();
        builder.Property(x => x.Amount)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

        builder.HasOne(x => x.TaxCategory)
               .WithMany()
               .HasForeignKey("TaxCategoryId")
               .IsRequired();
    }
}
