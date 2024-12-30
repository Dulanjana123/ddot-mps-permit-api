

using DDOT.MPS.Permit.Model.Dtos;

namespace DDOT.MPS.Permit.DataAccess.Repositories
{
    public interface IPdrmRepository
    {
        Task<int> CreatePdrm(int pdrMeetingType);

        Task<bool> PdrmExists(int pdrmId);

        Task<PdrmDto> GetPdrmById(int pdrmId);
    }
}
