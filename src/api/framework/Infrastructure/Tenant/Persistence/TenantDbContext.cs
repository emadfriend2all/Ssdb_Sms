using Finbuckle.MultiTenant.EntityFrameworkCore.Stores.EFCoreStore;
using Microsoft.EntityFrameworkCore;

namespace FSH.Framework.Infrastructure.Tenant.Persistence;
public class TenantDbContext : EFCoreStoreDbContext<FshTenantInfo>
{
    public const string Schema = "tenant";
    public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FshTenantInfo>().ToTable("Tenants", Schema);
        modelBuilder.Entity<FshTenantOrganization>().ToTable("TenantOrganizations", Schema);

        modelBuilder.Entity<FshTenantInfo>()
            .HasOne(t => t.Organization)
            .WithOne(o => o.Tenant)
            .HasForeignKey<FshTenantOrganization>(o => o.TenantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
