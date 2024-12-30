using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Response
{
    public class SslQueryResponseDto
    {
        [JsonPropertyName("Result")]
        public ResultData Result { get; set; }

        [JsonPropertyName("Success")]
        public bool Success { get; set; }

        [JsonPropertyName("Message")]
        public string Message { get; set; }
    }

    public class ResultData
    {
        [JsonPropertyName("ssls")]
        public List<SslData> Ssls { get; set; } = new List<SslData>();
    }

    public class SslData
    {
        [JsonPropertyName("MarId")]
        public string MarId { get; set; }

        [JsonPropertyName("FullAddress")]
        public string FullAddress { get; set; }

        [JsonPropertyName("SSL")]
        public string SSL { get; set; }

        [JsonPropertyName("Square")]
        public string Square { get; set; }

        [JsonPropertyName("Suffix")]
        public string? Suffix { get; set; }

        [JsonPropertyName("Lot")]
        public string Lot { get; set; }

        [JsonPropertyName("Lot_type")]
        public string LotType { get; set; }

        [JsonPropertyName("Col")]
        public string Col { get; set; }
    }
}
