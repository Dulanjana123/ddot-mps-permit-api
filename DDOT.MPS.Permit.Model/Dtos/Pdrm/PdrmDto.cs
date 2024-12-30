namespace DDOT.MPS.Permit.Model.Dtos
{
    public class PdrmDto
    {
        /// <summary>
        /// Meeting Id
        /// </summary>
        public int Id { get; set; }

        public PdrmProjectDto? Project { get; set; }

        public List<KeyValuePair<int, string>> LocationTypes { get; set; } = new List<KeyValuePair<int, string>>();

        public List<KeyValuePair<string, string>> Quadrants { get; set; } = new List<KeyValuePair<string, string>>();

        public List<int>? SelectedProjectLocations { get; set; } = new List<int>();

    }
}
