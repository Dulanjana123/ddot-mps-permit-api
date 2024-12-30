namespace DDOT.MPS.Permit.Model.Dtos
{
    public class ProjectBaseDto
    {
        public int ApplicantId { get; set; }
        public string ProjectName { get; set; }
        public string? ProjectApplicantName { get; set; }
        public string? ProjectDescription { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public int ProjectContactUserId { get; set; }
        public int[] CoreTeamMembers { get; set; }
        public int[] SupportiveTeamMembers { get; set; }
        public Location Locations { get; set; }
        public string? BzaZcNumber { get; set; }
        public string? SoSpLtrNumber { get; set; }
        public string? DobNumber { get; set; }
        public string? EisfNumber { get; set; }
        public DateTime? EisfSubmissionDate { get; set; }
        public bool? IsEisfApproved { get; set; }
    }

    public class ProjectAddDto : ProjectBaseDto
    {
        public int? ProjectId { get; set; }
    }

    public class ProjectUpdateDto : ProjectBaseDto
    {
        public int ProjectId { get; set; }
    }

    public class Location
    {
        string Type { get; set; }
        public IList<ProjectLocationAddress> Addresses { get; set; }
        public IList<ProjectLocationIntersection> Intersections { get; set; }
        public IList<ProjectLocationBlock> Blocks { get; set; }
    }

    public class ProjectLocationAddress
    {
        public string? Ward { get; set; }
        public string? Anc { get; set; }
        public string? Square { get; set; }
        public string? SquareLot { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
    }

    public class ProjectLocationIntersectionBase
    {
        public string? StreetOne { get; set; }
        public string? StreetTwo { get; set; }
        public string? Square { get; set; }
        public string? SquareLot { get; set; }

    }

    public class ProjectLocationIntersection : ProjectLocationIntersectionBase
    {
        public IList<KeyValuePair<string, string>>? Wards { get; set; }
        public IList<KeyValuePair<string, string>>? AdvisoryNeighborhoodCommissions { get; set; }

    }

    public class ProjectLocationBlock
    {
        public IList<BlockDetails> Details { get; set; }
        public string? Description { get; set; }
    }

    public class BlockDetails
    {
        public string? BlockName { get; set; }
        public string? Address { get; set; }
        public string? Anc { get; set; }
        public string? Square { get; set; }
        public string? SquareLot { get; set; }
        public string? Ward { get; set; }
    }
}
