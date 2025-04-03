using Finbuckle.MultiTenant.Abstractions;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Infrastructure.Persistence;
using FSH.Framework.Infrastructure.Tenant;
using FSH.Starter.WebApi.Catalog.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Constants;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.Persistence;

public sealed class CatalogDbContext : FshDbContext
{
    public CatalogDbContext(IMultiTenantContextAccessor<FshTenantInfo> multiTenantContextAccessor, DbContextOptions<CatalogDbContext> options, IPublisher publisher, IOptions<DatabaseOptions> settings)
        : base(multiTenantContextAccessor, options, publisher, settings)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Brand> Brands { get; set; } = null!;
    public DbSet<Organization> Organizations { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Contact> Contacts { get; set; } = null!;
    public DbSet<InvoiceAllowance> InvoiceAllowances { get; set; } = null!;
    public DbSet<InvoiceLineAllowance> InvoiceLineAllowances { get; set; } = null!;
    public DbSet<InvoiceBuyer> Buyers { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;
    public DbSet<InvoiceLine> InvoiceLines { get; set; } = null!;
    public DbSet<LegalMonetaryTotal> LegalMonetaryTotals { get; set; } = null!;
    public DbSet<InvoiceSeller> Sellers { get; set; } = null!;
    public DbSet<TaxCategory> TaxCategories { get; set; } = null!;
    public DbSet<TaxSubtotal> TaxSubtotals { get; set; } = null!;
    public DbSet<TaxTotal> TaxTotal { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        modelBuilder.HasDefaultSchema(SchemaNames.Catalog);
    }
}
