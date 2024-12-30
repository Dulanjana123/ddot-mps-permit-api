using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Response
{
    public class ProjectCoreTeamDto
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public short? RoleId { get; set; }
        public int? AgencyId { get; set; }
        public bool? IsActive { get; set; }
        public int? SortId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
