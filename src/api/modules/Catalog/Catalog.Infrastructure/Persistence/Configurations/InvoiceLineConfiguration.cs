using Finbuckle.MultiTenant;
using FSH.Starter.WebApi.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Persistence.Configurations;

internal sealed class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
{
    public void Configure(EntityTypeBuilder<InvoiceLine> builder)
    {
        builder.IsMultiTenant();
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(x => x.Quantity)
               .HasPrecision(18, 6)
               .IsRequired();

        builder.Property(x => x.LineExtensionAmount)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(x => x.VatAmount)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(x => x.NetAmount)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(x => x.AmountInclusiveVAT)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.HasOne(x => x.TaxCategory)
               .WithMany()
               .HasForeignKey("TaxCategoryId")
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Allowances)
               .WithOne()
               .HasForeignKey("InvoiceLineId")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
