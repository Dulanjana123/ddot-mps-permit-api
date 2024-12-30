namespace DDOT.MPS.Permit.Model.Dtos
{
    public class PdrmProjectLocationBlockDto: PdrmProjectLocationBaseDto
    {
        public required List<PdrmProjectLocationBlockDataDto> PdrmProjectLocationBlockDataList { get; set; }
    }
}
