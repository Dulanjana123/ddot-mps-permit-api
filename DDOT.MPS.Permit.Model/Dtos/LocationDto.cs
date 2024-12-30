using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Dtos
{
    public class LocationDto
    {
        public string Type { get; set; }
        public string? AddressStreetNumber { get; set; }
        public string? AddressStreetName { get; set; }
        public string? AddressStreetNameQuadrant { get; set; }
        public string? BlockStreetName { get; set; }
        public string? BlockStreetNameQuadrant { get; set; }
        public string? BlockStreet1 { get; set; }
        public string? BlockStreet1Quadrant { get; set; }
        public string? BlockStreet2 { get; set; }
        public string? BlockStreet2Quadrant { get; set; }
        public string? IntersectionStreet1 { get; set; }
        public string? IntersectionStreet1Quadrant { get; set; }
        public string? IntersectionStreet2 { get; set; }
        public string? IntersectionStreet2Quadrant { get; set; }
        public string? MarId { get; set; }
        public int? Score { get; set; }
        public string? FullBlock { get; set; }
        public string? BlockName { get; set; }
    }
}
