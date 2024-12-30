using DDOT.MPS.Permit.Model.Request.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Request
{
    public class ProjectPaginatedRequest : GenericSearch
    {
        public ProjectPaginatedFilters Filters { get; set; }
    }

    public class ProjectPaginatedFilters : GenericPaginatedFilter
    {
        public string? ProjectName { get; set; }
        public string? ProjectApplicantName { get; set; }
        public bool? ProjectStatus { get; set; }
        public DateTime? ProjectStartDateFrom { get; set; }
        public DateTime? ProjectStartDateTo { get; set; }
        public DateTime? ProjectEndDateFrom { get; set; }
        public DateTime? ProjectEndDateTo { get; set; }
        public int? AgencyId { get; set; }
        public int? UserId { get; set; }
    }

}
