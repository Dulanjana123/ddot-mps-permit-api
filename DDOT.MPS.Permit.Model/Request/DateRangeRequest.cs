using DDOT.MPS.Permit.Model.Request.Generic;

namespace DDOT.MPS.Permit.Model.Request
{
    public class DateRangeRequest : GenericSearch
    {
        public DateTime StartDate { get; set; } = DateTime.MinValue;

        public DateTime EndDate { get; set; } = DateTime.MaxValue;
    }
}
