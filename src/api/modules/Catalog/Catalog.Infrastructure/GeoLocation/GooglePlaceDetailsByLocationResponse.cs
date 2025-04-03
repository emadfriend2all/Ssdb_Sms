using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Starter.WebApi.Catalog.Infrastructure.GeoLocation;

public class GooglePlaceDetailsByLocationResponse
{
    [JsonProperty("plus_code")]
    public PlusCode? PlusCode { get; set; }

    [JsonProperty("results")]
    public List<Result>? Results { get; set; }

    [JsonProperty("status")]
    public string? Status { get; set; }
}

public class Bounds
{
    [JsonProperty("northeast")]
    public Northeast? Northeast { get; set; }

    [JsonProperty("southwest")]
    public Southwest? Southwest { get; set; }
}

public class Northeast
{
    [JsonProperty("lat")]
    public double? Lat { get; set; }

    [JsonProperty("lng")]
    public double? Lng { get; set; }
}

public class Southwest
{
    [JsonProperty("lat")]
    public double? Lat { get; set; }

    [JsonProperty("lng")]
    public double? Lng { get; set; }
}
