using Finbuckle.MultiTenant;
using FSH.Starter.WebApi.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Persistence.Configurations;

internal sealed class BuyerConfiguration : IEntityTypeConfiguration<InvoiceBuyer>
{
    public void Configure(EntityTypeBuilder<InvoiceBuyer> builder)
    {
        builder.IsMultiTenant();
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.TaxNumber)
               .HasMaxLength(50);

        builder.HasOne(x => x.Address)
               .WithMany()
               .HasForeignKey("AddressId")
               .OnDelete(DeleteBehavior.SetNull);
    }
}
