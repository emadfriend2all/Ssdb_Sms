﻿namespace FSH.Starter.WebApi.Catalog.Application.Contracts.Zatca.GenerateCsr;

public class GenerateCsrResponse
{
    public string Csr { get; set; } = default!;
    public string PrivateKey { get; set; } = default!;
}
