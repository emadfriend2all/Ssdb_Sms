using FSH.Framework.Core.Tenant.Abstractions;
using MediatR;

namespace FSH.Framework.Core.Tenant.Features.TopUp;

public class TopUpHandler : IRequestHandler<TopUpCommand, TopUpResponse>
{
    private readonly ITenantService _tenantService;

    public TopUpHandler(ITenantService tenantService) => _tenantService = tenantService;

    public async Task<TopUpResponse> Handle(TopUpCommand request, CancellationToken cancellationToken)
    {
        var balance = await _tenantService.TopUp(request.Tenant, request.Balance);
        return new TopUpResponse(balance, request.Tenant);
    }
}
