using System.Collections.ObjectModel;

namespace FSH.Starter.Shared.Authorization;

public static class FshPermissions
{
    private static readonly FshPermission[] AllPermissions =
    [     
        //tenants
        new("View Tenants", FshActions.View, FshResources.Tenants, IsRoot: true),
        new("Create Tenants", FshActions.Create, FshResources.Tenants, IsRoot: true),
        new("Update Tenants", FshActions.Update, FshResources.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", FshActions.UpgradeSubscription, FshResources.Tenants, IsRoot: true),

        //identity
        new("View Users", FshActions.View, FshResources.Users),
        new("Search Users", FshActions.Search, FshResources.Users),
        new("Create Users", FshActions.Create, FshResources.Users),
        new("Update Users", FshActions.Update, FshResources.Users),
        new("Delete Users", FshActions.Delete, FshResources.Users),
        new("Export Users", FshActions.Export, FshResources.Users),
        new("View UserRoles", FshActions.View, FshResources.UserRoles),
        new("Update UserRoles", FshActions.Update, FshResources.UserRoles),
        new("View Roles", FshActions.View, FshResources.Roles),
        new("Create Roles", FshActions.Create, FshResources.Roles),
        new("Update Roles", FshActions.Update, FshResources.Roles),
        new("Delete Roles", FshActions.Delete, FshResources.Roles),
        new("View RoleClaims", FshActions.View, FshResources.RoleClaims),
        new("Update RoleClaims", FshActions.Update, FshResources.RoleClaims),



        new("Google Services", FshActions.View, FshResources.Google),

        //products
        new("View Customer", FshActions.View, FshResources.Customers, IsBasic: true),
        new("Search Customer", FshActions.Search, FshResources.Customers, IsBasic: true),
        new("Create Customer", FshActions.Create, FshResources.Customers),
        new("Update Customer", FshActions.Update, FshResources.Customers),
        new("Delete Customer", FshActions.Delete, FshResources.Customers),
        new("Export Customer", FshActions.Export, FshResources.Customers),
        
        //products
        new("View Products", FshActions.View, FshResources.Products, IsBasic: true),
        new("Search Products", FshActions.Search, FshResources.Products, IsBasic: true),
        new("Create Products", FshActions.Create, FshResources.Products),
        new("Update Products", FshActions.Update, FshResources.Products),
        new("Delete Products", FshActions.Delete, FshResources.Products),
        new("Export Products", FshActions.Export, FshResources.Products),

        //invoices
        new("View Invoices", FshActions.View, FshResources.Invoices, IsBasic: true),
        new("Search Invoices", FshActions.Search, FshResources.Invoices, IsBasic: true),
        new("Create Invoices", FshActions.Create, FshResources.Invoices),
        new("Update Invoices", FshActions.Update, FshResources.Invoices),
        new("Delete Invoices", FshActions.Delete, FshResources.Invoices),
        new("Export Invoices", FshActions.Export, FshResources.Invoices),

        //tax categories
        new("View Tax Categories", FshActions.View, FshResources.TaxCategories, IsBasic: true),
        new("Search Tax Categories", FshActions.Search, FshResources.TaxCategories, IsBasic: true),
        new("Create Tax Categories", FshActions.Create, FshResources.TaxCategories),
        new("Update Tax Categories", FshActions.Update, FshResources.TaxCategories),
        new("Delete Tax Categories", FshActions.Delete, FshResources.TaxCategories),
        new("Export Tax Categories", FshActions.Export, FshResources.TaxCategories),

        //zatca
        new("Clear Invoice", FshActions.Create, FshResources.Zatca, IsBasic: true),
        new("Onboard", FshActions.Create, FshResources.Zatca, IsBasic: true),
        new("Report Invoice", FshActions.Create, FshResources.Zatca),
        new("Submit Invoice", FshActions.Create, FshResources.Zatca),
        new("Validate Invoice", FshActions.Create, FshResources.Zatca),

        //organizations
        new("View Organizations", FshActions.View, FshResources.Organizations, IsBasic: true),
        new("Search Organizations", FshActions.Search, FshResources.Organizations, IsBasic: true),
        new("Create Organizations", FshActions.Create, FshResources.Organizations),
        new("Update Organizations", FshActions.Update, FshResources.Organizations),
        new("Delete Organizations", FshActions.Delete, FshResources.Organizations),
        new("Export Organizations", FshActions.Export, FshResources.Organizations),

        //todos
        new("View Todos", FshActions.View, FshResources.Todos, IsBasic: true),
        new("Search Todos", FshActions.Search, FshResources.Todos, IsBasic: true),
        new("Create Todos", FshActions.Create, FshResources.Todos),
        new("Update Todos", FshActions.Update, FshResources.Todos),
        new("Delete Todos", FshActions.Delete, FshResources.Todos),
        new("Export Todos", FshActions.Export, FshResources.Todos),

         new("View Hangfire", FshActions.View, FshResources.Hangfire),
         new("View Dashboard", FshActions.View, FshResources.Dashboard),

        //audit
        new("View Audit Trails", FshActions.View, FshResources.AuditTrails),
    ];

    public static IReadOnlyList<FshPermission> All { get; } = new ReadOnlyCollection<FshPermission>(AllPermissions);
    public static IReadOnlyList<FshPermission> Root { get; } = new ReadOnlyCollection<FshPermission>(AllPermissions.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<FshPermission> Admin { get; } = new ReadOnlyCollection<FshPermission>(AllPermissions.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<FshPermission> Basic { get; } = new ReadOnlyCollection<FshPermission>(AllPermissions.Where(p => p.IsBasic).ToArray());
}

public record FshPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource)
    {
        return $"Permissions.{resource}.{action}";
    }
}


