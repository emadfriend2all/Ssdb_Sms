using System.Net;

namespace FSH.Starter.WebApi.Catalog.Domain.Exceptions;
public class ComplianceCheckException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public ComplianceCheckException(string message, HttpStatusCode statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
