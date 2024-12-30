using DDOT.MPS.Permit.Model.Request.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Request
{
    public class InspectionPaginatedRequest: GenericSearch
    {
        public InspectionPaginatedFilters Filters { get; set; }
    }

    public class InspectionPaginatedFilters : GenericPaginatedFilter
    {
        public int? EwrRequestId { get; set; }
    }
}
