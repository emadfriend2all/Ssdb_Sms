using FSH.Framework.Core.Auth.Jwt;
using FSH.Framework.Infrastructure.Auth.Api;
using FSH.Framework.Infrastructure.Auth.Policy;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FSH.Framework.Infrastructure.Auth.Jwt;
internal static class Extensions
{
    internal static IServiceCollection ConfigureJwtAuth(this IServiceCollection services)
    {
        services.AddOptions<JwtOptions>()
            .BindConfiguration(nameof(JwtOptions))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
        services.AddAuthentication(authentication =>
        {
            authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            authentication.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, null!)
            .AddScheme<AuthenticationSchemeOptions, ApiTokenHandler>("ApiToken", options => { })
            ;



        services.AddAuthorizationBuilder().AddRequiredPermissionPolicy();
        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.GetPolicy(RequiredPermissionDefaults.PolicyName);
            options.AddPolicy("ApiTokenPolicy", policy =>
                policy.AddAuthenticationSchemes("ApiToken")
                      .RequireAuthenticatedUser());

            //// Add a combined policy
            //options.AddPolicy("ApiTokenWithCreatePermission", policy =>
            //{
            //    policy.AddAuthenticationSchemes("ApiToken")
            //          .RequireAuthenticatedUser()
            //          .RequireClaim("permission", "Permissions.Todos.Create");
            //});
        });

        return services;
    }
}
