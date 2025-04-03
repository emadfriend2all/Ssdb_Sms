using Finbuckle.MultiTenant;
using FSH.Starter.WebApi.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Persistence.Configurations;
internal sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.IsMultiTenant();
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CommonName).HasMaxLength(100);
        builder.Property(x => x.SerialNumber).HasMaxLength(50);
        builder.Property(x => x.OrganizationIdentifier).HasMaxLength(30);
        builder.Property(x => x.OrganizationUnitName).HasMaxLength(100);
        builder.Property(x => x.OrganizationName).HasMaxLength(100);
        builder.Property(x => x.InvoiceType).HasMaxLength(20);
        builder.Property(x => x.IndustryBusinessCategory).HasMaxLength(50);
    }
}
