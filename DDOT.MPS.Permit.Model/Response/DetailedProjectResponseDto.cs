using DDOT.MPS.Permit.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Response
{
    public class DetailedProjectResponseDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string? ProjectApplicantName { get; set; }
        public string? ProjectDescription { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public ProjectTeamMember? ProjectContactUser { get; set; }
        public List<ProjectTeamMember>? ProjectCoreTeams { get; set; }
        public List<ProjectTeamMember>? ProjectSupportTeams { get; set; }
        public string? BzaZcNumber { get; set; }
        public string? SoSpLtrNumber { get; set; }
        public string? DobNumber { get; set; }
        public string? EisfNumber { get; set; }
        public DateTime? EisfSubmissionDate { get; set; }
        public bool? IsEisfApproved { get; set; }
        public int? ProjectStatusId { get; set; }
        public bool? IsActive { get; set; }
        public int? ProjectSortId { get; set; }
        public int? ProjectCreatedBy { get; set; }
        public DateTime? ProjectCreatedDate { get; set; }
        //public GeneralProjectUser? ProjectLastUpdatedBy { get; set; }
        public DateTime? ProjectLastUpdatedDate { get; set; }
        public string? ProjectLocationJson { get; set; }
        public ResponseLocationData ProjectLocationData { get; set; }
    }

    public class ResponseLocationData
    {
        public List<ProjectLocationAddress> Addresses { get; set; }
        public List<ProjectLocationIntersectionReq> Intersections { get; set; }
        public List<ProjectLocationBlock> Blocks { get; set; }
    }

    public class ProjectLocationIntersectionReq : ProjectLocationIntersectionBase
    {
        public IList<string> Wards { get; set; }
        public IList<string> Ancs { get; set; }
    }

    public class ProjectTeamMember
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Organization { get; set; }
        public string? Role { get; set; }
        public bool? IsActive { get; set; }
        public string? MobileNumber { get; set; }
    }

    //public class GeneralProjectUser
    //{
    //    public string UserId { get; set; }
        
    //}
}
