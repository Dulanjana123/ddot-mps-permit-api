using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Model.Response
{
    public class InspectionResponseDto
    {
        public int InspDetailId { get; set; }

        public int ApplicationId { get; set; }

        public int ApplicationTypeId { get; set; }

        public int InspectedBy { get; set; }

        public int? InspTypeId { get; set; }

        public int? InspStatusId { get; set; }

        public DateTime? InspectionDate { get; set; }

        public int? MinutesSpent { get; set; }

        public string? InternalNotes { get; set; }

        public string? ExternalNotes { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? Inspector { get; set; }
    }
}
