using AutoMapper;
using DDOT.MPS.Permit.DataAccess.DBContexts;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.DataAccess.Repositories.UserRepository;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using static DDOT.MPS.Permit.Core.Exceptions.UserDefinedException;

namespace DDOT.MPS.Permit.DataAccess.Repositories
{
    public class MpsProjectRepository : IMpsProjectRepository
    {
        private readonly MpsDbContext _userDbContext;
        private readonly IMapper _mapper;
        private readonly IMpsUserRepository _userRepository;
        private readonly ILogger<MpsProjectRepository> _logger;

        public MpsProjectRepository(MpsDbContext userDbContext, IMapper mapper, IMpsUserRepository userRepository, ILogger<MpsProjectRepository> logger)
        {
            _userDbContext = userDbContext ?? throw new ArgumentNullException(nameof(userDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ProjectResponseDto> Update(int id, ProjectUpdateDto request)
        {
            _logger.LogError("DDOT.MPS.Permit.DataAccess.Repositories.ProjectRepository.Update | Method in progress. Project ID: {ProjectId}", id);
            Project? project = await _userDbContext.Projects
                .Include(p => p.ProjectCoreTeams.Where(ct => ct.IsActive))
                .Include(p => p.ProjectSupportTeams.Where(st => st.IsActive))
                .Where(ct => ct.IsActive)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null)
            {
                _logger.LogError("DDOT.MPS.Permit.DataAccess.Repositories.ProjectRepository.Update | Project not found in the database. Project ID: {ProjectId}", id);
                throw new UDNotFoundException("PROJECT_NOT_FOUND");
            }

            UpdateCoreTeamMembers(project.ProjectCoreTeams, request.CoreTeamMembers);
            UpdateSupportTeamMembers(project.ProjectSupportTeams, request.SupportiveTeamMembers);

            project.ProjectName = request.ProjectName;
            project.ProjectApplicantName = request.ProjectApplicantName;
            project.ProjectDescription = request.ProjectDescription;
            project.ProjectStartDate = request.ProjectStartDate;
            project.ProjectEndDate = request.ProjectEndDate;
            project.ProjectContactUserId = request.ProjectContactUserId;
            project.BzaZcNumber = request.BzaZcNumber;
            project.SoSpLtrNumber = request.SoSpLtrNumber;
            project.DobNumber = request.DobNumber;
            project.EisfNumber = request.EisfNumber;
            project.EisfSubmissionDate = request.EisfSubmissionDate;
            project.IsEisfApproved = request.IsEisfApproved;
            project.ProjectLocationJson = JsonSerializer.Serialize(request.Locations);
            project.ModifiedDate = DateTime.UtcNow;

            _userDbContext.Projects.Update(project);
            await _userDbContext.SaveChangesAsync();

            return _mapper.Map<ProjectResponseDto>(project);
        }

        private void UpdateSupportTeamMembers(ICollection<ProjectSupportTeam> dbSupportTeamMembers, int[] requestSupportTeamUserIds)
        {
            foreach (ProjectSupportTeam dbSupportTeamMember in dbSupportTeamMembers)
            {
                if (!requestSupportTeamUserIds.Contains(dbSupportTeamMember.UserId))
                {
                    dbSupportTeamMember.IsActive = false;
                    dbSupportTeamMember.ModifiedDate = DateTime.UtcNow;
                }
            }

            foreach (int supportTeamUserId in requestSupportTeamUserIds)
            {
                ProjectSupportTeam? selectedSupportTeamUser = dbSupportTeamMembers.Where(u => u.UserId == supportTeamUserId).FirstOrDefault();
                if (selectedSupportTeamUser == null)
                {
                    dbSupportTeamMembers.Add(new ProjectSupportTeam
                    {
                        UserId = supportTeamUserId,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        IsActive = true
                    });
                }
                else
                {
                    selectedSupportTeamUser.ModifiedDate = DateTime.UtcNow;
                }
            }
        }

        private static void UpdateCoreTeamMembers(ICollection<ProjectCoreTeam> dbCoreTeamMembers, int[] requestCoreTeamUserIds)
        {
            foreach (ProjectCoreTeam dbCoreTeamMember in dbCoreTeamMembers)
            {
                if (!requestCoreTeamUserIds.Contains(dbCoreTeamMember.UserId))
                {
                    dbCoreTeamMember.IsActive = false;
                    dbCoreTeamMember.ModifiedDate = DateTime.UtcNow;
                }
            }

            foreach (int coreTeamUserId in requestCoreTeamUserIds)
            {
                ProjectCoreTeam? selectedCoreTeamUser = dbCoreTeamMembers.Where(u => u.UserId == coreTeamUserId).FirstOrDefault();
                if (selectedCoreTeamUser == null)
                {
                    dbCoreTeamMembers.Add(new ProjectCoreTeam
                    {
                        UserId = coreTeamUserId,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        IsActive = true
                    });
                }
                else
                {
                    selectedCoreTeamUser.ModifiedDate = DateTime.UtcNow;
                }
            }
        }

        public async Task<List<ProjectCoreTeam?>> GetProjectCoreTeamByProjectId(int projectId)
        {
            List<ProjectCoreTeam?> projectCoreTeams = await _userDbContext.ProjectCoreTeams
                .Where(x => x.ProjectId == projectId && x.IsActive)
                .ToListAsync();
            return projectCoreTeams;
        }

        public async Task<List<ProjectSupportTeam?>> GetSupportiveTeamByProjectId(int projectId)
        {
            List<ProjectSupportTeam?> projectSupportTeams = await _userDbContext.ProjectSupportTeams
                .Where(x => x.ProjectId == projectId && x.IsActive)
                .ToListAsync();
            return projectSupportTeams;
        }

        public async Task<List<int>> GetCoreTeamMembers(int projectId)
        {
            List<int> coreTeamMembers = await _userDbContext.ProjectCoreTeams
                .Where(x => x.ProjectId == projectId && x.IsActive)
                .Select(x => x.UserId)
                .ToListAsync();
            return coreTeamMembers;
        }

        public async Task<List<int>> GetSupportTeamMembers(int projectId)
        {
            List<int> supportTeamMembers = await _userDbContext.ProjectSupportTeams
                .Where(x => x.ProjectId == projectId && x.IsActive)
                .Select(x => x.UserId)
                .ToListAsync();
            return supportTeamMembers;
        }

        public async Task<ProjectAddDto> Create(Project project)
        {
            ProjectAddDto? projectDto = _mapper.Map<ProjectAddDto>(project);
            await _userDbContext.Projects.AddAsync(project);
            await _userDbContext.SaveChangesAsync();
            return projectDto;
        }

        //private checkIfProjectCompleted 

        public async Task<Project> GetById(int id)
        {
            Project? project = await _userDbContext.Projects
                .Include(p => p.ProjectCoreTeams.Where(ct => ct.IsActive))
                    .ThenInclude(ct => ct.User)
                .Include(p => p.ProjectSupportTeams.Where(st => st.IsActive))
                    .ThenInclude(st => st.User)
                .Include(p => p.ProjectContactUser)
                .Include(p => p.ProjectLocations)
                .Where(p => p.IsActive)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            // check if project is completed - If end date is less than current date, then project is completed
            if (project != null)
            {
                if (project.ProjectEndDate < DateTime.UtcNow)
                {
                    project.IsActive = false;
                    await _userDbContext.SaveChangesAsync();
                }
            }

            return _mapper.Map<Project>(project);
        }

        public async Task<IQueryable<Project>> GetByIdQueryable(int id)
        {
            IQueryable<Project> queryable = _userDbContext.Projects
                .Include(p => p.ProjectCoreTeams.Where(ct => ct.IsActive))
                    .ThenInclude(ct => ct.User)
                .Include(p => p.ProjectSupportTeams.Where(st => st.IsActive))
                    .ThenInclude(st => st.User)
                .Include(p => p.ProjectContactUser)
                .Include(p => p.ProjectLocations)
                .Where(p => p.IsActive && p.ProjectId == id);

            // check if project is completed - If end date is less than current date, then project is completed
            var project = queryable.FirstOrDefault();
            if (project != null && project.ProjectEndDate < DateTime.UtcNow)
            {
                project.IsActive = false;
                await _userDbContext.SaveChangesAsync();
            }

            return queryable;
        }

        public List<KeyValuePair<int, string>> GetAncs()
        {
            List<KeyValuePair<int, string>> ancs = _userDbContext.Ancs
                .Select(x => new KeyValuePair<int, string>(x.AncId, x.AncName))
                .ToList();
            return ancs;
        }

        public List<KeyValuePair<int, string>> GetWards()
        {
            List<KeyValuePair<int, string>> wards = _userDbContext.Wards
                .Select(x => new KeyValuePair<int, string>(x.WardId, x.WardName))
                .ToList();
            return wards;
        }


        public async Task<bool> CheckIfProjectNameUser(string projectName)
        {
            return await _userDbContext.Projects.AnyAsync(x => x.ProjectName == projectName);
        }

        public async Task<ProjectCoreTeamDto> CreateCoreTeamMember(ProjectCoreTeamDto project)
        {
            ProjectCoreTeam? projectCoreTeam = _mapper.Map<ProjectCoreTeam>(project);
            await _userDbContext.ProjectCoreTeams.AddAsync(projectCoreTeam);
            await _userDbContext.SaveChangesAsync();
            return project;
        }

        private List<int> GetUsersInAgency(int agencyId)
        {
            List<int> userIds = _userDbContext.Users
                .Include(userIds => userIds.Agency)
                .Where(x => x.AgencyId == agencyId)
                .Select(x => x.UserId)
                .ToList();

            return userIds;
        }

        private List<int> GetProjectsHavingApplicantTeamMember(int userId)
        {
            List<int> distinctProjectIds = _userDbContext.ProjectCoreTeams
                .Where(x => x.UserId == userId && x.IsActive)
                .Select(x => x.ProjectId)
                .Distinct()
                .ToList();

            return distinctProjectIds;
        }

        public async Task<List<Project>> GetPaginatedList(ProjectPaginatedRequest request)
        {
            string projectName = request.Filters.ProjectName.Trim().ToLower();
            //string applicantName = request.Filters.ProjectApplicantName.Trim().ToLower();
            string applicantName = request.Filters.ProjectApplicantName == null ? null : request.Filters.ProjectApplicantName.Trim().ToLower();

            DateTime? projectStartDateFrom = request.Filters.ProjectStartDateFrom.HasValue
                ? DateTime.SpecifyKind(request.Filters.ProjectStartDateFrom.Value, DateTimeKind.Unspecified)
                : (DateTime?)null;

            DateTime? projectStartDateTo = request.Filters.ProjectStartDateTo.HasValue
                ? DateTime.SpecifyKind(request.Filters.ProjectStartDateTo.Value, DateTimeKind.Unspecified)
                : (DateTime?)null;

            DateTime? projectEndDateFrom = request.Filters.ProjectEndDateFrom.HasValue
                ? DateTime.SpecifyKind(request.Filters.ProjectEndDateFrom.Value, DateTimeKind.Unspecified)
                : (DateTime?)null;

            DateTime? projectEndDateTo = request.Filters.ProjectEndDateTo.HasValue
                ? DateTime.SpecifyKind(request.Filters.ProjectEndDateTo.Value, DateTimeKind.Unspecified)
                : (DateTime?)null;

            IQueryable<Project> projects = _userDbContext.Projects
                    .Where(x => x.ProjectName.ToLower().Contains(projectName) &&
                                (applicantName == null || x.ProjectApplicantName.ToLower().Contains(applicantName))&&
                                (projectStartDateFrom == null || x.ProjectStartDate >= projectStartDateFrom) &&
                                (projectStartDateTo == null || x.ProjectStartDate <= projectStartDateTo) &&
                                (projectEndDateFrom == null || x.ProjectEndDate >= projectEndDateFrom) &&
                                (projectEndDateTo == null || x.ProjectEndDate <= projectEndDateTo) &&
                                (request.Filters.ProjectStatus == null || x.IsActive == request.Filters.ProjectStatus)) // true if project is active, else false (implies the project is complete)
                    .OrderByDescending(x => x.CreatedDate)
                    .OrderByDescending(x => x.ModifiedDate);

            if (request.Filters.AgencyId != null)
            {
                List<int> userIds = GetUsersInAgency(request.Filters.AgencyId.Value);

                projects = projects
                    .Where(x => userIds.Contains(x.CreatedBy.Value));
            }
            else
            {
                List<int> projectIds = GetProjectsHavingApplicantTeamMember(request.Filters.UserId.Value);

                projects = projects
                    .Where(x => projectIds.Contains(x.ProjectId) && x.CreatedBy == request.Filters.UserId.Value);
            }            

            if (request.PagingAndSortingInfo.Paging.PageSize > 0)
            {
                projects = projects
                    .Skip((request.PagingAndSortingInfo.Paging.PageNo - 1) * request.PagingAndSortingInfo.Paging.PageSize)
                    .Take(request.PagingAndSortingInfo.Paging.PageSize);
            }

            // check if project is completed - If end date is less than current date, then project is completed
            if (projects.Any())
            {
                foreach (var project in projects)
                {
                    if (project.ProjectEndDate < DateTime.UtcNow)
                    {
                        project.IsActive = false;
                    }
                }
                await _userDbContext.SaveChangesAsync();
            }

            return await projects.ToListAsync();
        }

    }
}
