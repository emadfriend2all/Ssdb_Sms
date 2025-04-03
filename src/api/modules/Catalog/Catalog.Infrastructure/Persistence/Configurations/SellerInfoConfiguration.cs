using Finbuckle.MultiTenant;
using FSH.Starter.WebApi.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Persistence.Configurations;

internal sealed class SellerInfoConfiguration : IEntityTypeConfiguration<InvoiceSeller>
{
    public void Configure(EntityTypeBuilder<InvoiceSeller> builder)
    {
        builder.IsMultiTenant();
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Identifier)
               .HasMaxLength(50);

        builder.Property(x => x.IdentifierType)
               .HasMaxLength(20);

        builder.Property(x => x.Name)
               .HasMaxLength(100);

        builder.HasOne(x => x.Address)
               .WithOne()
               .HasForeignKey<InvoiceSeller>(x => x.AddressId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
