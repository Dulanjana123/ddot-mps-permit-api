using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Response
{
    public class BlockAddressResponseDto
    {
        public string? Address { get; set; }
        public string? Ward { get; set; }
        public string? Anc { get; set; }
        public string? Square { get; set; }
        public string? SquareLot { get; set; }

    }
}
