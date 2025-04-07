using System.Security.Claims;
using System.Text.Encodings.Web;
using Finbuckle.MultiTenant.Abstractions;
using FSH.Starter.Shared.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FSH.Framework.Infrastructure.Identity.Users;

namespace FSH.Framework.Infrastructure.Auth.Api;

public class ApiTokenHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IPasswordHasher<string> _passwordHasher;
    private readonly UserManager<FshUser> _userManager;
    public ApiTokenHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IPasswordHasher<string> passwordHasher,
        UserManager<FshUser> userManager)
        : base(options, logger, encoder)
    {
        _passwordHasher = passwordHasher;
        _userManager = userManager;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("X-Api-Token", out var apiKeyHeaderValues))
        {
            return AuthenticateResult.NoResult();
        }


        var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

        if (apiKeyHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(providedApiKey))
        {
            return AuthenticateResult.NoResult();
        }


        var token = apiKeyHeaderValues.ToString();
        if (string.IsNullOrEmpty(token))
        {
            return AuthenticateResult.Fail("Empty API token");
        }

        var user = await _userManager.GetUserAsync(Context.User);

        if (user == null)
        {
            return AuthenticateResult.Fail("Invalid user");
        }

        bool matched = _passwordHasher.VerifyHashedPassword(user.Id, user.ApiToken ?? string.Empty, token) == PasswordVerificationResult.Success;

        if (!matched)
        {
            return AuthenticateResult.Fail("Invalid Token");
        }

        var claims = new[]
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, "SmsSender"),
            new Claim(FshClaims.ApiToken, "true"),
            new Claim(ClaimTypes.Role, "true")
        };

        var identity = new ClaimsIdentity(claims, nameof(ApiTokenHandler));
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}
