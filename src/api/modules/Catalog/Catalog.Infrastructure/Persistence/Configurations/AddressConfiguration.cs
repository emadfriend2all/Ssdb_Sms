using Finbuckle.MultiTenant;
using FSH.Starter.WebApi.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Persistence.Configurations;

internal sealed class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.IsMultiTenant();
        builder.HasKey(x => x.Id);

        builder.Property(x => x.StreetName).HasMaxLength(100);
        builder.Property(x => x.BuildingNumber).HasMaxLength(50);
        builder.Property(x => x.LandMark).HasMaxLength(50);
        builder.Property(x => x.Suburb).HasMaxLength(100);
        builder.Property(x => x.City).HasMaxLength(100);
        builder.Property(x => x.PostalCode).HasMaxLength(20);
        builder.Property(x => x.Province).HasMaxLength(100);
        builder.Property(x => x.CountryCode).HasMaxLength(10);
        builder.Property(x => x.FullAddress).HasMaxLength(255);
        builder.Property(x => x.ShortAddress).HasMaxLength(150);
        builder.Property(x => x.Country).HasMaxLength(50);
        builder.Property(x => x.StreetNumber).HasMaxLength(10);
    }
}
