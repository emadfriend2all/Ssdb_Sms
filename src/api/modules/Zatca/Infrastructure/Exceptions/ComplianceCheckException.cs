using System.Net;

namespace FSH.Starter.WebApi.Zatca.Infrastructure.Exceptions;
public class ComplianceCheckException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public ComplianceCheckException(string message, HttpStatusCode statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
