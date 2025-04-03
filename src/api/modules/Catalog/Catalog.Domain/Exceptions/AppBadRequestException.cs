using FSH.Framework.Core.Exceptions;

namespace FSH.Starter.WebApi.Catalog.Domain.Exceptions;

public sealed class AppBadRequestException : NotFoundException
{
    public AppBadRequestException(string message)
        : base(message)
    {
    }
}

