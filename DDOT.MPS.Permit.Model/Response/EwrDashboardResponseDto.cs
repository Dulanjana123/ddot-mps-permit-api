using DDOT.MPS.Permit.Model.Dtos;

namespace DDOT.MPS.Permit.Model.Response
{
    public class EwrDashboardResponseDto
    {
        public long TotalEwrCount { get; set; } = 0;

        public long EwrCountWithoutCP { get; set; } = 0;

        public List<AgencyEwrCountDto>? EwrVsAgencyChartData { get; set; }

        public List<WardEwrCountDto>? WardVsEwrChartData { get; set; }

        public List<EmergencyTypeEwrCountDto>? EmergencyTypeVsEwrChartData { get; set; }

        public List<StatusEwrCountDto>? StatusVsEwrChartData { get; set; }

        public List<EwrResponseDto>? EwrData { get; set; }
        public List<InspectorCountDto> InspectorCountChartData { get; set; } 
    }
}
