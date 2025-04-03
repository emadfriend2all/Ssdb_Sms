using Finbuckle.MultiTenant;
using FSH.Starter.WebApi.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Persistence.Configurations;

internal sealed class TaxSubtotalConfiguration : IEntityTypeConfiguration<TaxSubtotal>
{
    public void Configure(EntityTypeBuilder<TaxSubtotal> builder)
    {
        builder.IsMultiTenant();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TaxableAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.TaxAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasOne(x => x.TaxCategory)
            .WithMany()
            .HasForeignKey(x => x.TaxCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
