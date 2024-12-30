using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Response
{
    public class BlockInternalResponseDto
    {
        [JsonPropertyName("Result")]
        public BlockResult? Result { get; set; }

        [JsonPropertyName("Success")]
        public bool? Success { get; set; }

        [JsonPropertyName("Message")]
        public object? Message { get; set; }
    }

    public class BlockResult
    {
        [JsonPropertyName("blocks")]
        public List<Block>? Blocks { get; set; }

        [JsonPropertyName("blockAddresses")]
        public List<BlockAddress>? BlockAddresses { get; set; }
    }

    public class Block
    {
        [JsonPropertyName("block")]
        public BlockDetail? BlockDetail { get; set; }
    }

    public class BlockDetail
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("properties")]
        public BlockProperties? Properties { get; set; }
    }

    public class BlockProperties
    {
        [JsonPropertyName("Ward")]
        public string? Ward { get; set; }

        [JsonPropertyName("FullBlock")]
        public string? FullBlock { get; set; }

        [JsonPropertyName("BlockName")]
        public string? BlockName { get; set; }

        [JsonPropertyName("MarId")]
        public string? MarId { get; set; }

        [JsonPropertyName("_Score")]
        public double? Score { get; set; }

        [JsonPropertyName("Latitude")]
        public double? XCoordinate { get; set; }

        [JsonPropertyName("Longitude")]
        public double? YCoordinate { get; set; }

    }

    public class BlockAddress
    {
        [JsonPropertyName("address")]
        public NestedBlockAddress? Address { get; set; }
    }

    public class NestedBlockAddress
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("properties")]
        public AddressProperties? Properties { get; set; }
    }

    public class AddressProperties
    {
        [JsonPropertyName("Ward")]
        public string? Ward { get; set; }

        [JsonPropertyName("Anc")]
        public string? Anc { get; set; }

        [JsonPropertyName("FullAddress")]
        public string? FullAddress { get; set; }

        [JsonPropertyName("SSL")]
        public string? Ssl { get; set; }

        [JsonPropertyName("Zipcode")]
        public string? ZipCode { get; set; }

        [JsonPropertyName("Latitude")]
        public double? XCoordinate { get; set; }

        [JsonPropertyName("Longitude")]
        public double? YCoordinate { get; set; }

    }
}
