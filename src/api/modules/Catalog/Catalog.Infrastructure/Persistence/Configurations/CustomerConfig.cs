using Finbuckle.MultiTenant;
using FSH.Starter.WebApi.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Persistence.Configurations;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.IsMultiTenant();
        builder.HasIndex(e => e.IdentityNumber).IsUnique();
        builder.HasIndex(e => e.RegistrationNo).IsUnique();
        builder.HasIndex(e => e.TaxNumber).IsUnique();

        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.TaxNumber).HasMaxLength(50);
        builder.Property(x => x.IdentityNumber).HasMaxLength(50);
        builder.Property(x => x.RegistrationNo).HasMaxLength(50);

        builder
        .HasOne(c => c.DefaultAddress)
        .WithMany()
        .HasForeignKey(c => c.DefaultAddressId)
        .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(c => c.DefaultContact)
            .WithMany()
            .HasForeignKey(c => c.DefaultContactId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(c => c.Addresses)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(c => c.Contacts)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
