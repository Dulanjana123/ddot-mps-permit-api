using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Response
{
    public class ProjectListResponseDto
    {
        public List<ProjectListResponseDto>? Projects { get; set; }
    }

    public class ProjectResponseDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string? ProjectApplicantName { get; set; }
        public string? ProjectDescription { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public int? ProjectContactUserId { get; set; }
        public string? BzaZcNumber { get; set; }
        public string? SoSpLtrNumber { get; set; }
        public string? DobNumber { get; set; }
        public string? EisfNumber { get; set; }
        public DateTime? EisfSubmissionDate { get; set; }
        public bool? IsEisfApproved { get; set; }
        public short? ProjectStatusId { get; set; }
        public bool? IsActive { get; set; }
        public int? ProjectSortId { get; set; }
        public int? ProjectCreatedBy { get; set; }
        public DateTime? ProjectCreatedDate { get; set; }
        public int? ProjectLastUpdatedBy { get; set; }
        public DateTime? ProjectLastUpdatedDate { get; set; }
        public string? ProjectLocationJson { get; set; }
    }
}
