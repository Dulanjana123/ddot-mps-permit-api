using AutoMapper;
using DDOT.MPS.Permit.DataAccess.DBContexts;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Repositories.UserRepository
{
    public class MpsUserRepository : IMpsUserRepository
    {
        private readonly MpsDbContext _userDbContext;
        private readonly IMapper _mapper;

        public MpsUserRepository(MpsDbContext userDbContext, IMapper mapper)
        {
            _userDbContext = userDbContext;
            _mapper = mapper;
        }

        public async Task<List<User?>> GetUserList(List<int> userIds)
        {
            List<User?> mpsUsers = await _userDbContext.Users
                .Where(x => userIds.Contains(x.UserId))
                .ToListAsync();
            return mpsUsers;
        }

        public async Task<User?> GetUserById(int userId)
        {
            User mpsUser = await _userDbContext.Users
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();
            return mpsUser;
        }

        public async Task<bool> CheckAllUsersExist(IList<int> userIds)
        {
            return await _userDbContext.Users
                .Where(u => userIds.Contains(u.UserId) && u.IsActive)
                .CountAsync() == userIds.Count;
        }

        public IQueryable<UserOption> GetAllInspectors()
        {
            IQueryable<UserOption> inspectors = _userDbContext.Users
            .Select(res => new UserOption
            {
                UserId = res.UserId,
                FullName = string.Concat(res.FirstName, " ", res.LastName),
            }).AsQueryable();

            return inspectors;

        }

        public IQueryable<UserOption> GetAllUsersNames()
        {
            IQueryable<UserOption> inspectors = _userDbContext.Users
            .Select(res => new UserOption
            {
                UserId = res.UserId,
                FullName = string.Concat(res.FirstName, " ", res.LastName),
            }).AsQueryable();

            return inspectors;
        }
    }
}
