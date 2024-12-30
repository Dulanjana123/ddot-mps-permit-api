using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Response;

namespace DDOT.MPS.Permit.DataAccess.Repositories.UserRepository
{
    public interface IMpsUserRepository
    {
        Task<List<User?>> GetUserList(List<int> userIds);

        Task<User?> GetUserById(int userId);

        Task<bool> CheckAllUsersExist(IList<int> userIds);

        IQueryable<UserOption> GetAllInspectors();

        IQueryable<UserOption> GetAllUsersNames();

    }
}
