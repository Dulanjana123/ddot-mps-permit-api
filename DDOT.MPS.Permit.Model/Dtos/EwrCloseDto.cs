
using Microsoft.AspNetCore.Http;

namespace DDOT.MPS.Permit.Model.Dtos
{
    public class EwrCloseDto
    {
        public string ReasonForClose { get; set; }

        public List<IFormFile>? EvidenceDocuments {  get; set; }

        public DateTime? CancelledDate { get; set; } = DateTime.Now;

        public int? CancelledBy { get; set; }

    }
}
