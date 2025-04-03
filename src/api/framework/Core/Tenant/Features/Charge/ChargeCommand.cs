using MediatR;

namespace FSH.Framework.Core.Tenant.Features.Charge;
public class ChargeCommand : IRequest<ChargeResponse>
{
    public string Tenant { get; set; } = default!;
    public int Amount { get; set; }
}
