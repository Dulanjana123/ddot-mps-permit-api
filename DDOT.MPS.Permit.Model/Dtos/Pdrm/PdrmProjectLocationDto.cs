namespace DDOT.MPS.Permit.Model.Dtos
{
    public class PdrmProjectLocationDto
    {
        public required int ProjectLocationId { get; set; }

        public required string Type { get; set; }

        public PdrmProjectLocationAddressDto? Address { get; set; }

        public PdrmProjectLocationBlockDto? Block { get; set; }

    }
}
