namespace DDOT.MPS.Permit.Model.Dtos
{
    public class PdrmProjectLocationBaseDto
    {
        public required string CreatedUserName { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public string? Description { get; set; }

        //Common for Address and Block
        public string? Category { get; set; }

    }
}
