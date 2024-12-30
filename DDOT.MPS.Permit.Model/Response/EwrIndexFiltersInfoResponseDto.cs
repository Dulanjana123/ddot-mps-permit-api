
namespace DDOT.MPS.Permit.Model.Response
{
    public class EwrIndexFiltersInfoResponseDto
    {
        public List<EwrStatusOption>? EwrStatuses { get; set; }

        public List<EwrEmergencyTypeOption>? EwrEmergencyTypes { get; set; }

        public List<EwrEmergencyCauseOption>? EwrEmergencyCauses { get; set; }

        public List<EwrEmergencyCategoryOption>? EwrEmergencyCategories { get; set; }

        public List<UserOption>? Users { get; set; }

        public List<SwoStatusOption>? SwoStatuses { get; set; }
        
    }
    public class EwrEmergencyCategoryOption
    {
        public int EmergencyCategoryId { get; set; }

        public string EmergencyCategoryDesc { get; set; }
    }

    public class EwrEmergencyTypeOption
    {
        public int EmergencyTypeId { get; set; }

        public string EmergencyTypeDesc { get; set; }
    }

    public class EwrEmergencyCauseOption
    {
        public int EmergencyCauseId { get; set; }

        public string EmergencyCauseDesc { get; set; }
    }

    public class SwoStatusOption
    {
        public int StatusId { get; set; }

        public string StatusDesc { get; set; }
    }
}
