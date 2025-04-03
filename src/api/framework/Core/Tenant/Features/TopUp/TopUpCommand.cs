using MediatR;

namespace FSH.Framework.Core.Tenant.Features.TopUp;
public class TopUpCommand : IRequest<TopUpResponse>
{
    public string Tenant { get; set; } = default!;
    public int Balance { get; set; }
}
