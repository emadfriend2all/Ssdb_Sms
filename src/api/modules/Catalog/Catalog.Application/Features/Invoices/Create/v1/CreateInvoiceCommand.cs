using System.ComponentModel;
using FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.Invoices;
using MediatR;
using Zatca.EInvoice.SDK.Contracts.Models;

namespace FSH.Starter.WebApi.Catalog.Application.Features.Invoices.Create.v1;
public class CreateInvoiceCommand: StandardInvoice, IRequest<CreateInvoiceResponse>{
    public string Title { get; set; }
    public EnvironmentType EnvironmentType { get; set; }
}
