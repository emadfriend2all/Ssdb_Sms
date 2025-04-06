using FSH.Framework.Core.Caching;
using FSH.Framework.Core.Persistence;
using FSH.Starter.WebApi.Todo.Domain;
using FSH.Starter.WebApi.Todo.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FSH.Starter.WebApi.Todo.Features.Get.v1;
public sealed class GetBalanceHandler(
    [FromKeyedServices("todo")] IReadRepository<TodoItem> repository,
    ICacheService cache)
    : IRequestHandler<GetBalanceRequest, GetBalanceResponse>
{
    public async Task<GetBalanceResponse> Handle(GetBalanceRequest request, CancellationToken cancellationToken)
    {
      var response = await ApiHelper.GetAsync<GetBalanceResponse>("/balance");
        return response;
    }
}
