
namespace DDOT.MPS.Permit.Model.Dtos
{
    public class EwrBulkAssigningDto
    {
        public List<int> EwrApplicationIds { get; set; }

        public int? AssigneeId { get; set; }

        public int? InspStatusId { get; set; }

        public int? EwrStatusId { get; set; }

        public string? Comments {  get; set; } = "";
    }
}
