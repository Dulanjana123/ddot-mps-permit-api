using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDOT.MPS.Permit.Model.Request.Generic;

namespace DDOT.MPS.Permit.Model.Request
{
    public class EwrLocationFilterRequest : GenericSearch
    {
        public int LocationId { get; set; }
    }
}
