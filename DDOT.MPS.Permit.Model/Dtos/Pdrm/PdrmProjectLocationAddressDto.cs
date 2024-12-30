namespace DDOT.MPS.Permit.Model.Dtos
{
    public class PdrmProjectLocationAddressDto : PdrmProjectLocationBaseDto
    {
        public required string Ward { get; set; }
        public required string Anc { get; set; }
        public required string Square { get; set; }
        public required string SquareLot { get; set; }
    }
}
