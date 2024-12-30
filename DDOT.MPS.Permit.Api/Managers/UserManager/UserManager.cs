using AutoMapper;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.DataAccess.Repositories.UserRepository;

namespace DDOT.MPS.Permit.Api.Managers.UserManager
{
    public class UserManager : IUserManager
    {
        private readonly IMpsUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserManager(IMpsUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<User?>> GetUserList(List<int> userIds)
        {
            List<User?> mpsUsers = await _userRepository.GetUserList(userIds);
            return mpsUsers;
        }

        public async Task<User?> GetUserById(int userId)
        {
            User? mpsUser = await _userRepository.GetUserById(userId);
            return mpsUser;
        }

        public async Task<bool> CheckAllUsersExist(IList<int> userIds)
        {
            if (!userIds.Any()) return false;
            return await _userRepository.CheckAllUsersExist(userIds);
        }
    }
}
