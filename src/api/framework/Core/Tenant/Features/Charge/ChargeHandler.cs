using FSH.Framework.Core.Tenant.Abstractions;
using MediatR;

namespace FSH.Framework.Core.Tenant.Features.Charge;

public class ChargeHandler : IRequestHandler<ChargeCommand, ChargeResponse>
{
    private readonly ITenantService _tenantService;

    public ChargeHandler(ITenantService tenantService) => _tenantService = tenantService;

    public async Task<ChargeResponse> Handle(ChargeCommand request, CancellationToken cancellationToken)
    {
        var balance = await _tenantService.Charge(request.Tenant, request.Amount);
        return new ChargeResponse(balance, request.Tenant);
    }
}
