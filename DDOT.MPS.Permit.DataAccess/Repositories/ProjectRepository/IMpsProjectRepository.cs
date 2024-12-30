using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;

namespace DDOT.MPS.Permit.DataAccess.Repositories
{
    public interface IMpsProjectRepository
    {
        Task<ProjectAddDto> Create(Project project);
        Task<Project> GetById(int id);
        Task<bool> CheckIfProjectNameUser(string projectName);
        Task<ProjectCoreTeamDto> CreateCoreTeamMember(ProjectCoreTeamDto project);
        Task<List<Project>> GetPaginatedList(ProjectPaginatedRequest request);
        Task<List<int>> GetCoreTeamMembers(int projectId);
        Task<List<int>> GetSupportTeamMembers(int projectId);
        Task<ProjectResponseDto> Update(int id, ProjectUpdateDto request);
        Task<List<ProjectCoreTeam?>> GetProjectCoreTeamByProjectId(int projectId);
        Task<List<ProjectSupportTeam?>> GetSupportiveTeamByProjectId(int projectId);
        Task<IQueryable<Project>> GetByIdQueryable(int id);
        List<KeyValuePair<int, string>> GetWards();
        List<KeyValuePair<int, string>> GetAncs();
    }
}
