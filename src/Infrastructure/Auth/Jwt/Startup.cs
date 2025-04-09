using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Showmatics.Application.Identity.Users;

namespace Showmatics.Infrastructure.Auth.Jwt;

internal static class Startup
{
    internal static IServiceCollection AddJwtAuth(this IServiceCollection services)
    {
        services.AddOptions<JwtSettings>()
            .BindConfiguration($"SecuritySettings:{nameof(JwtSettings)}")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

        return services
            .AddAuthentication(authentication =>
            {
                authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, null!)
            .Services;
    }
}

//using Microsoft.AspNetCore.Authentication;
//using System.Security.Claims;
//using System.Text.Encodings.Web;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Identity.Client;
//using Microsoft.IdentityModel.Tokens;
//using Newtonsoft.Json;
//using System.Net;
//using System.Text.Json;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;
//using Showmatics.Application.Common.Exceptions;
//using Microsoft.AspNetCore.Identity;
//using Showmatics.Infrastructure.Identity;
//using Showmatics.Application.Identity.Users;
//using Mapster;

//namespace Showmatics.Infrastructure.Auth.Jwt;

//internal static class Startup
//{
//    internal static IServiceCollection AddJwtAuth(this IServiceCollection services)
//    {
//        services.AddOptions<JwtSettings>()
//            .BindConfiguration($"SecuritySettings:{nameof(JwtSettings)}")
//            .ValidateDataAnnotations()
//            .ValidateOnStart();

//        services.AddOptions<ApiJwtSettings>()
//            .BindConfiguration($"SecuritySettings:{nameof(ApiJwtSettings)}")
//            .ValidateDataAnnotations()
//            .ValidateOnStart();

//        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

//        services.AddAuthentication(options =>
//        {
//            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//        })
//        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, null!);
//        //.AddScheme<JwtBearerOptions, AppAuthJWTAuthenticationHandler>("ApiToken", options => { });

//        return services;
//    }
//}

//public class AppAuthJWTAuthenticationHandler : JwtBearerHandler
//{
//    private readonly IUserService _userService;

//    public AppAuthJWTAuthenticationHandler(
//        IOptionsMonitor<JwtBearerOptions> options,
//        ILoggerFactory logger,
//        UrlEncoder encoder,
//        ISystemClock clock,
//        IUserService userService)
//        : base(options, logger, encoder, clock)
//    {
//        _userService = userService;
//    }

//    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
//    {
//        string accessToken = ReadTokenFromHeader();

//        if (string.IsNullOrWhiteSpace(accessToken))
//        {
//            return AuthenticateResult.NoResult();
//        }

//        var jwtHandler = new JwtSecurityTokenHandler();

//        if (jwtHandler.CanReadToken(accessToken))
//        {
//            try
//            {
//                return await base.HandleAuthenticateAsync();
//                //var token = jwtHandler.ReadJwtToken(accessToken);
//                //if (token.Issuer == Options.TokenValidationParameters.ValidIssuer)
//                //{
//                //    return await base.HandleAuthenticateAsync();
//                //}
//            }
//            catch
//            {
//                // fallback to ApiToken flow
//            }
//        }

//        try
//        {
//            var user = await _userService.GetUserByApiToken(accessToken, CancellationToken.None);
//            if (user == null)
//            {
//                return AuthenticateResult.Fail("Invalid API Token");
//            }

//            var claims = new List<Claim>
//        {
//            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
//            new(ClaimTypes.Name, user.UserName ?? string.Empty),
//        };

//            var roles = await _userService.GetRolesAsync(user.Id.ToString(), CancellationToken.None);
//            foreach (var role in roles)
//            {
//                claims.Add(new Claim(ClaimTypes.Role, role.RoleName ?? string.Empty));
//            }

//            var identity = new ClaimsIdentity(claims, Scheme.Name);
//            var principal = new ClaimsPrincipal(identity);
//            var ticket = new AuthenticationTicket(principal, Scheme.Name);

//            return AuthenticateResult.Success(ticket);
//        }
//        catch (Exception ex)
//        {
//            Logger.LogError(ex, "Error while authenticating API token.");
//            return AuthenticateResult.Fail("Invalid API Token");
//        }
//    }

//    private string ReadTokenFromHeader()
//    {
//        string? authorization = Request?.Headers["Authorization"];

//        if (string.IsNullOrEmpty(authorization))
//        {
//            return string.Empty;
//        }

//        if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
//        {
//            return authorization["Bearer ".Length..].Trim();
//        }

//        return string.Empty;
//    }
//}