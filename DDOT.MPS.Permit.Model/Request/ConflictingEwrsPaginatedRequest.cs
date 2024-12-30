using DDOT.MPS.Permit.Model.Request.Generic;

namespace DDOT.MPS.Permit.Model.Request
{
    public class ConflictingEwrsPaginatedRequest: GenericSearch
    {
        public string EwrRequestId { get; set; }
    }

    
}
