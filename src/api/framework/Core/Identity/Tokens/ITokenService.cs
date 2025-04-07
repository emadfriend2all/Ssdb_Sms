using FSH.Framework.Core.Identity.Tokens.Features.Generate;
using FSH.Framework.Core.Identity.Tokens.Features.Refresh;
using FSH.Framework.Core.Identity.Tokens.Models;

namespace FSH.Framework.Core.Identity.Tokens;
public interface ITokenService
{
    Task<StaticTokenGenerationResponse> GenerateStaticTokenAsync(StaticTokenGenerationCommand request , CancellationToken cancellationToken);
    Task<TokenResponse> GenerateTokenAsync(TokenGenerationCommand request, string ipAddress, CancellationToken cancellationToken);
    Task<TokenResponse> RefreshTokenAsync(RefreshTokenCommand request, string ipAddress, CancellationToken cancellationToken);

}
