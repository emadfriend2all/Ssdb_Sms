using Carter;
using FSH.Framework.Core.Persistence;
using FSH.Framework.Infrastructure.Persistence;
using FSH.Starter.WebApi.Catalog.Domain;
using FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1;
using FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Customers;
using FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Invoices;
using FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Organizations;
using FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Products;
using FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.TaxCategories;
using FSH.Starter.WebApi.Catalog.Infrastructure.Endpoints.v1.Zatca;
using FSH.Starter.WebApi.Catalog.Infrastructure.Persistence;
using FSH.Starter.WebApi.Zatca.Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Catalog.Infrastructure;
public static class CatalogModule
{
    public class Endpoints : CarterModule
    {
        public Endpoints() : base("catalog") { }
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            var customerGroup = app.MapGroup("customers").WithTags("customers");
            customerGroup.MapCustomerCreationEndpoint();
            customerGroup.MapGetCustomerEndpoint();
            customerGroup.MapGetCustomerListEndpoint();
            customerGroup.MapCustomerUpdateEndpoint();
            customerGroup.MapCustomerDeleteEndpoint();

            var invoiceGroup = app.MapGroup("invoices").WithTags("invoices");
            invoiceGroup.MapInvoiceCreationEndpoint();
            invoiceGroup.MapGetInvoiceEndpoint();
            invoiceGroup.MapPrintInvoiceEndpoint();
            invoiceGroup.MapGetInvoiceListEndpoint();
            invoiceGroup.MapInvoiceUpdateEndpoint();
            invoiceGroup.MapInvoiceDeleteEndpoint();

            var productGroup = app.MapGroup("products").WithTags("products");
            productGroup.MapProductCreationEndpoint();
            productGroup.MapGetProductEndpoint();
            productGroup.MapGetProductListEndpoint();
            productGroup.MapProductUpdateEndpoint();
            productGroup.MapProductDeleteEndpoint();

            var googleServicesGroup = app.MapGroup("googleServices").WithTags("googleServices");
            googleServicesGroup.MapGetPredictionsEndpoint();
            googleServicesGroup.MapGetPlaceByIdEndpoint();
            googleServicesGroup.MapGetPlaceByLocationEndpoint();

            var brandGroup = app.MapGroup("taxCategories").WithTags("taxCategories");
            brandGroup.MapTaxCategoryCreationEndpoint();
            brandGroup.MapGetTaxCategoryEndpoint();
            brandGroup.MapGetTaxCategoryListEndpoint();
            brandGroup.MapTaxCategoryUpdateEndpoint();
            brandGroup.MapTaxCategoryDeleteEndpoint();

            var zatcaGroup = app.MapGroup("zatca").WithTags("zatca");
            zatcaGroup.MapOnboardEndpoint();
            zatcaGroup.MapValidateInvoiceEndpoint();
            zatcaGroup.MapClearInvoiceEndpoint();
            zatcaGroup.MapReportInvoiceEndpoint();
            zatcaGroup.MapSubmitInvoiceEndpoint();

            var organizationGroup = app.MapGroup("organizations").WithTags("organizations");
            organizationGroup.MapOrganizationCreationEndpoint();
            organizationGroup.MapGetOrganizationEndpoint();
            organizationGroup.MapGetOrganizationListEndpoint();
            organizationGroup.MapOrganizationUpdateEndpoint();
            organizationGroup.MapOrganizationDeleteEndpoint();
        }
    }
    public static WebApplicationBuilder RegisterCatalogServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.BindDbContext<CatalogDbContext>();
        builder.Services.AddScoped<IDbInitializer, CatalogDbInitializer>();
        builder.Services.AddKeyedScoped<IRepository<Product>, CatalogRepository<Product>>("catalog:products");
        builder.Services.AddKeyedScoped<IReadRepository<Product>, CatalogRepository<Product>>("catalog:products");

        builder.Services.AddKeyedScoped<IRepository<Customer>, CatalogRepository<Customer>>("catalog:customers");
        builder.Services.AddKeyedScoped<IReadRepository<Customer>, CatalogRepository<Customer>>("catalog:customers");

        builder.Services.AddKeyedScoped<IRepository<TaxCategory>, CatalogRepository<TaxCategory>>("catalog:taxCategories");
        builder.Services.AddKeyedScoped<IReadRepository<TaxCategory>, CatalogRepository<TaxCategory>>("catalog:taxCategories");

        builder.Services.AddKeyedScoped<IRepository<Invoice>, CatalogRepository<Invoice>>("catalog:invoices");
        builder.Services.AddKeyedScoped<IReadRepository<Invoice>, CatalogRepository<Invoice>>("catalog:invoices");

        builder.Services.AddKeyedScoped<IRepository<Organization>, CatalogRepository<Organization>>("catalog:organizations");
        builder.Services.AddKeyedScoped<IReadRepository<Organization>, CatalogRepository<Organization>>("catalog:organizations");

        builder.Services.AddAppServices();
        return builder;
    }
    public static WebApplication UseCatalogModule(this WebApplication app)
    {
        return app;
    }

    internal static IServiceCollection AddAppServices(this IServiceCollection services) =>
        services
            .AddServices(typeof(ITransientService), ServiceLifetime.Transient)
            .AddServices(typeof(IScopedService), ServiceLifetime.Scoped);

    internal static IServiceCollection AddServices(this IServiceCollection services, Type interfaceType, ServiceLifetime lifetime)
    {
        var interfaceTypes =
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => interfaceType.IsAssignableFrom(t)
                            && t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterfaces().FirstOrDefault(),
                    Implementation = t
                })
                .Where(t => t.Service is not null
                            && interfaceType.IsAssignableFrom(t.Service));

        foreach (var type in interfaceTypes)
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(type.Service!, type.Implementation);
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped(type.Service!, type.Implementation);
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(type.Service!, type.Implementation);
                    break;
            }
        }

        return services;
    }
}
