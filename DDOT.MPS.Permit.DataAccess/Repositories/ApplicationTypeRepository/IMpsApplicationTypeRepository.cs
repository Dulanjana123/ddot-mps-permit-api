using DDOT.MPS.Permit.DataAccess.Entities;


namespace DDOT.MPS.Permit.DataAccess.Repositories.ApplicationTypeRepository
{
    public interface IMpsApplicationTypeRepository
    {
        Task<ApplicationType> GetApplicationTypeByTypeCode(string applicationTypeCode);
    }
}
