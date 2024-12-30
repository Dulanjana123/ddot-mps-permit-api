using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Response
{
    public class LocationResponseDto
    {
        public string? Ward { get; set; }
        public string? Anc { get; set; }
        public string? Square { get; set; }
        public string? SquareLot { get; set; }
        public string? Zipcode { get; set; }
        public string? Address { get; set; }
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        public string? MarId { get; set; }
        public double? Score { get; set; }
        public string? FullBlock { get; set; }
        public string? BlockName { get; set; }
        public double? XCoordinate { get; set; }
        public double? YCoordinate { get; set; }
    }
}
