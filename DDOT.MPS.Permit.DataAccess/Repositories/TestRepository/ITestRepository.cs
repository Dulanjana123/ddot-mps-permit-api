namespace DDOT.MPS.Permit.DataAccess.Repositories.TestRepository
{
    public interface ITestRepository
    {
        Task<string> Create(string name);
        Task<string> Update(string name);
        Task<string> GetById(int id);
        Task<string> GetAll(string filters);
    }
}
