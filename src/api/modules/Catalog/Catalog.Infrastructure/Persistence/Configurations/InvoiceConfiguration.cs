using Finbuckle.MultiTenant;
using FSH.Starter.WebApi.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Persistence.Configurations;

internal sealed class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.IsMultiTenant();
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Number)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(x => x.InvoiceType)
               .HasMaxLength(30)
               .IsRequired();

        builder.Property(x => x.Uuid)
               .HasMaxLength(100);

        builder.Property(x => x.InvoiceTypeCode)
               .HasMaxLength(10);

        builder.Property(x => x.DocumentCurrencyCode)
               .HasMaxLength(3)
               .IsRequired();

        builder.Property(x => x.TaxCurrencyCode)
               .HasMaxLength(3);

        builder.Property(x => x.PreviousInvoiceHash)
               .HasMaxLength(256);

        builder.Property(x => x.AdjustmentReason)
               .HasMaxLength(500);

        builder.Property(x => x.AdjsutmentInvoiceNumber)
               .HasMaxLength(50);

        builder.HasOne(x => x.InvoiceSeller)
               .WithMany()
               .HasForeignKey("InvoiceSellerId")
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.InvoiceBuyer)
               .WithMany()
               .HasForeignKey("InvoiceBuyerId")
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Allowances)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.InvoiceLines)
               .WithOne()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.LegalMonetaryTotal)
               .WithOne()
               .HasForeignKey<Invoice>("LegalMonetaryTotalId")
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.TaxTotal)
               .WithOne()
               .HasForeignKey<Invoice>("TaxTotalId")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
