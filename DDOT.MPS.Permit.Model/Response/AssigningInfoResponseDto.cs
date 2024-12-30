
namespace DDOT.MPS.Permit.Model.Response
{
    public class AssigningInfoResponseDto
    {
        public List<UserOption>? Inspectors { get; set; }

        public List<InspStatusOption>? InspStatuses { get; set; }

        public List<EwrStatusOption>? EwrStatuses { get; set; }
    }

    public class UserOption
    {
        public int UserId {  get; set; }

        public string FullName { get; set; }
    }

    public class InspStatusOption
    {
        public int InspStatusId { get; set; }

        public string InspStatusDesc { get; set; }
    }

    public class EwrStatusOption
    {
        public int StatusId { get; set; }

        public string StatusDesc { get; set; }
    }
}
