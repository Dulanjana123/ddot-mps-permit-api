

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DDOT.MPS.Permit.Model.Dtos
{
    public class SwoViolationDto
    {
        public string SwoNumber { get; set; }

        public int SwoViolationTypeId { get; set; }

        public int SwoTypeId { get; set; }

        public int SwoStatusId { get; set; }

        public string? ViolatedContrName { get; set; }

        public string? ViolatedContrRegNo { get; set; }

        public string? ViolatedContrRegAddr { get; set; }

        public string? ViolatedOwnerFname { get; set; }

        public string? ViolatedOwnerLname { get; set; }

        public string NoteCode { get; set; }

        public string ViolationComments { get; set; }

        public int IssuedBy { get; set; }

        public DateTime IssuedDate {  get; set; }
        
        public string IssuedTime { get; set; }

        public string WorkSiteForeman { get; set; }

        public string WorkSiteForemanPhone { get; set; }

        public string WeatherConditions { get; set; }

        public string WorkSiteConditions {  get; set; }

        public string InternalNotes { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
