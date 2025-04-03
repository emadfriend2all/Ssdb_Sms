using MediatR;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Organizations.Delete.v1;
public sealed record DeleteOrganizationCommand(
    int Id) : IRequest;
