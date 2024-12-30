using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;

namespace DDOT.MPS.Permit.Api.Managers
{
    public interface IProjectManager
    {
        public Task<BaseResponse<ProjectResponseDto>> Create(ProjectAddDto request);
        public Task<BaseResponse<DetailedProjectResponseDto>> GetById(int id);
        public Task<BaseResponse<Result<ProjectResponseDto>>> GetPaginatedList(ProjectPaginatedRequest request);
        public Task<BaseResponse<ProjectResponseDto>> Update(int id, ProjectUpdateDto request);
    }
}
