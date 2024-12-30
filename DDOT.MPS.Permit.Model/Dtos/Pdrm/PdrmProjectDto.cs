namespace DDOT.MPS.Permit.Model.Dtos
{
    public class PdrmProjectDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<PdrmProjectLocationDto>? Locations { get; set; }        
    }
}
