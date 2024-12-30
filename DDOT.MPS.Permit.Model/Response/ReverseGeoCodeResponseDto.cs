using System.Text.Json.Serialization;

namespace DDOT.MPS.Permit.Model.Response
{
    public class ReverseGeoCodeResponseDto
    {
        [JsonPropertyName("Result")]
        public List<ReverseGeoCodeResult>? Result { get; set; }

        [JsonPropertyName("Success")]
        public bool? Success { get; set; }

        [JsonPropertyName("Message")]
        public string? Message { get; set; }
    }

    public class ReverseGeoCodeResult
    {
        [JsonPropertyName("address")]
        public ReverseGeoCodeAddress? Address { get; set; }

        [JsonPropertyName("distance")]
        public double? Distance { get; set; }

        [JsonPropertyName("units")]
        public string? Units { get; set; }

        [JsonPropertyName("zones")]
        public Dictionary<string, List<string>>? Zones { get; set; }
    }

    public class ReverseGeoCodeAddress
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("properties")]
        public ReverseGeoCodeProperties? Properties { get; set; }

        [JsonPropertyName("geometry")]
        public ReverseGeoCodeGeometry? Geometry { get; set; }
    }

    public class ReverseGeoCodeProperties
    {
        [JsonPropertyName("_Score")]
        public double? _Score { get; set; }

        [JsonPropertyName("MarId")]
        public string? MarId { get; set; }

        [JsonPropertyName("FullAddress")]
        public string? FullAddress { get; set; }

        [JsonPropertyName("SSL")]
        public string? SSL { get; set; }

        [JsonPropertyName("Alias")]
        public string? Alias { get; set; }

        [JsonPropertyName("Xcoord")]
        public double? Xcoord { get; set; }

        [JsonPropertyName("Ycoord")]
        public double? Ycoord { get; set; }

        [JsonPropertyName("Latitude")]
        public double? Latitude { get; set; }

        [JsonPropertyName("Longitude")]
        public double? Longitude { get; set; }

        [JsonPropertyName("AddrNum")]
        public string? AddrNum { get; set; }

        [JsonPropertyName("AddrNumSuffix")]
        public string? AddrNumSuffix { get; set; }

        [JsonPropertyName("StName")]
        public string? StName { get; set; }

        [JsonPropertyName("StreetType")]
        public string? StreetType { get; set; }

        [JsonPropertyName("Quadrant")]
        public string? Quadrant { get; set; }

        [JsonPropertyName("Zipcode")]
        public string? Zipcode { get; set; }

        [JsonPropertyName("BlockKey")]
        public string? BlockKey { get; set; }

        [JsonPropertyName("SubBlockKey")]
        public string? SubBlockKey { get; set; }

        [JsonPropertyName("Ward")]
        public string? Ward { get; set; }

        [JsonPropertyName("Anc")]
        public string? Anc { get; set; }

        [JsonPropertyName("CensusTract")]
        public string? CensusTract { get; set; }

        [JsonPropertyName("ResidenceType")]
        public string? ResidenceType { get; set; }

        [JsonPropertyName("HasCondoUnit")]
        public string? HasCondoUnit { get; set; }

        [JsonPropertyName("HasResUnit")]
        public string? HasResUnit { get; set; }

        [JsonPropertyName("Status")]
        public string? Status { get; set; }

        [JsonPropertyName("NationalGrid")]
        public string? NationalGrid { get; set; }

    }

    public class ReverseGeoCodeGeometry
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<double>? Coordinates { get; set; }
    }

    public class ReverseGeoCodeSslResponseDto: ReverseGeoCodeResult
    {
        public SslData sslData { get; set; }
    }
}
