
namespace DDOT.MPS.Permit.Model.Response
{
    public class SwoViolationTypesResponseDto
    {
        public List<ViolationTypeOption> ViolationTypes { get; set; }
    }

    public class ViolationTypeOption
    {
        public int ViolationTypeId {  get; set; }

        public string ViolationTypeDesc { get; set; }
    }

}
