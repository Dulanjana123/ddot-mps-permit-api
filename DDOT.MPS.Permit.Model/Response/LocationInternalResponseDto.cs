using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Response
{
    public class LocationInternalResponseDto
    {
        [JsonPropertyName("Result")]
        public Result? Result { get; set; }

        [JsonPropertyName("Success")]
        public bool? Success { get; set; }

        [JsonPropertyName("Message")]
        public string? Message { get; set; }
    }

    public class Result
    {
        [JsonPropertyName("addresses")]
        public List<AddressInfo>? Addresses { get; set; }
    }

    public class AddressInfo
    {
        [JsonPropertyName("address")]
        public Address? Address { get; set; }
    }

    public class Address
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("properties")]
        public Properties? Properties { get; set; }

        public static implicit operator Address(AddressProperties v)
        {
            throw new NotImplementedException();
        }
    }

    public class Properties
    {
        [JsonPropertyName("Ward")]
        public string? Ward { get; set; }

        [JsonPropertyName("Anc")]
        public string? Anc { get; set; }

        [JsonPropertyName("SSL")]
        public string? SSL { get; set; }

        [JsonPropertyName("Zipcode")]
        public string? ZipCode { get; set; }

        [JsonPropertyName("FullAddress")]
        public string? Address { get; set; }

        [JsonPropertyName("_Score")]
        public double? Score { get; set; }

        [JsonPropertyName("MarId")]
        public string? MarId { get; set; }

        [JsonPropertyName("Latitude")]
        public double? XCoordinate { get; set; }

        [JsonPropertyName("Longitude")]
        public double? YCoordinate { get; set; }                 

    }
}
