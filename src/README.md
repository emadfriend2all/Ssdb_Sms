#For Migrations 
#Navigate to ./api/server and run the following EF CLI commands.
#E:\WorkSpace\FSH\dotnet-starter-kit\src\api\server

dotnet ef migrations add "Add Identity Schema" --project .././migrations/mssql/ --context IdentityDbContext -o Identity
dotnet ef migrations add "Add Tenant Schema" --project .././migrations/mssql/ --context TenantDbContext -o Tenant
dotnet ef migrations add "Add Todo Schema" --project .././migrations/mssql/ --context TodoDbContext -o Todo
dotnet ef migrations add "Add Catalog Schema" --project .././migrations/mssql/ --context CatalogDbContext -o Catalog


dotnet ef database update --project ../migrations/mssql/ --context IdentityDbContext
dotnet ef database update --project ../migrations/mssql/ --context ZatcaDbContext
dotnet ef database update --project ../migrations/mssql/ --context CatalogDbContext
dotnet ef database update --project ../migrations/mssql/ --context TenantDbContext
dotnet ef database update --project ../migrations/mssql/ --context TodoDbContext


dotnet ef migrations remove  --project .././migrations/mssql/ --context CatalogDbContext