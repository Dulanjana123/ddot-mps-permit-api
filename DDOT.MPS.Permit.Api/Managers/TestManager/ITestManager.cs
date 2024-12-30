namespace DDOT.MPS.Permit.Api.Managers.TestManager
{
    public interface ITestManager
    {
        Task<string> Create(string name); 
        Task<string> Update(string name); 
        Task<string> GetById(int id); 
        Task<string> GetAll(string filters); 
    }
}
