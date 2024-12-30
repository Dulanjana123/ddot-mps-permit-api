using DDOT.MPS.Permit.Model.Request.Generic;

namespace DDOT.MPS.Permit.Model.Request
{
    public class EwrPaginatedRequest: GenericSearch
    {
        public EwrPaginatedFilters Filters { get; set; }

    }

    public class EwrPaginatedFilters : GenericPaginatedFilter
    {
        public string RequestNumber { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Ward { get; set; } = string.Empty;

        public DateTime? EffectiveDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string EmergencyType { get; set; } = string.Empty;

        public string EmergencyCause { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string AppliedBy { get; set; } = string.Empty;

        public DateTime? CreationDate { get; set; }

        public string UtilityCompany { get; set; } = string.Empty;

        public string InternalUtilityTrackingNumber { get; set; } = string.Empty;

        public string AssignedInspector { get; set; } = string.Empty;

        public string InspectionStatus { get; set; } = string.Empty;

        public DateTime? LastInspectionDate { get; set; }

        public int ConstructionPermitNumberIfFiled { get; set; } = int.MinValue;

        public DateTime? RequestedDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<int>? SwoStatusIds { get; set; }

        public List<int>? IssuedByIds { get; set; }

        //ID Search with equeal filters
        public int? LocationId {  get; set; } 

        public int? ExceptEwrRequestId { get; set; }

        public int? AssignedInspectorId { get; set; }

        public int? EwrStatusId { get; set; }

        public int? InspStatusId { get; set; }

    }
}
