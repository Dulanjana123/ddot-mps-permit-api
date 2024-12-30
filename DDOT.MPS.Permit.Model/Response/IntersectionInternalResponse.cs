using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Response
{
    public class IntersectionInternalResponse
    {
        [JsonPropertyName("Result")]
        public IntersectionResult? Result { get; set; }

        [JsonPropertyName("Success")]
        public bool? Success { get; set; }

        [JsonPropertyName("Message")]
        public string? Message { get; set; }
    }

    public class IntersectionResult
    {
        [JsonPropertyName("intersections")]
        public List<IntersectionWrapper>? Intersections { get; set; }
    }

    public class IntersectionWrapper
    {
        [JsonPropertyName("intersection")]
        public Intersection? Intersection { get; set; }
    }

    public class Intersection
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("properties")]
        public IntersectionProperties? Properties { get; set; }

        [JsonPropertyName("geometry")]
        public Geometry? Geometry { get; set; }
    }

    public class IntersectionProperties
    {
        [JsonPropertyName("FullStreet1Display")]
        public string? St1Name { get; set; }

        [JsonPropertyName("FullStreet2Display")]
        public string? St2Name { get; set; }

        [JsonPropertyName("FullIntersection")]
        public string? FullIntersection { get; set; }

        [JsonPropertyName("MarId")]
        public string? MarId { get; set; }

        [JsonPropertyName("_Score")]
        public double? Score { get; set; }

        [JsonPropertyName("Latitude")]
        public double? XCoordinate { get; set; }

        [JsonPropertyName("Longitude")]
        public double? YCoordinate { get; set; }

    }

    public class Geometry
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<double>? Coordinates { get; set; }
    }
}
