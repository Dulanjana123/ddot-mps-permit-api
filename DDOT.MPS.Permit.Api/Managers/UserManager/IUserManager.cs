using DDOT.MPS.Permit.DataAccess.Entities;

namespace DDOT.MPS.Permit.Api.Managers.UserManager
{
    public interface IUserManager
    {
        Task<List<User?>> GetUserList(List<int> userIds);
        Task<User?> GetUserById(int userId);

        Task<bool> CheckAllUsersExist(IList<int> userIds);
    }
}
