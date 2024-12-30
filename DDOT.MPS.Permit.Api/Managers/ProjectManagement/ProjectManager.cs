using AutoMapper;
using DDOT.MPS.Permit.Api.Managers.UserManager;
using DDOT.MPS.Permit.Core.Utilities;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.DataAccess.Repositories;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static DDOT.MPS.Permit.Core.Exceptions.UserDefinedException;

namespace DDOT.MPS.Permit.Api.Managers
{
    public class ProjectManager : IProjectManager
    {
        private readonly IMpsProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IUserManager _userManager;
        private readonly ILogger<ProjectManager> _logger;
        private readonly IStringUtils _stringUtils;

        public ProjectManager(IMpsProjectRepository projectRepository, IMapper mapper, IUserManager userManager, ILogger<ProjectManager> logger, IStringUtils stringUtils)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _stringUtils = stringUtils ?? throw new ArgumentNullException(nameof(stringUtils));

            _logger.LogInformation("DDOT.MPS.Permit.Api.Managers.ProjectManager | Ctor");
        }

        public async Task<BaseResponse<ProjectResponseDto>> Update(int id, ProjectUpdateDto request)
        {
            _logger.LogInformation("DDOT.MPS.Permit.Api.Managers.ProjectManager.Update | Request in progress. | Project ID: {ProjectId}", id);

            //Check CoreTeamMembers are Valid
            int[] coreTeamMembers = request.CoreTeamMembers;            
            if (!coreTeamMembers.Any())
            {
                _logger.LogError("DDOT.MPS.Permit.Api.Managers.ProjectManager.Update | Core team members array is empty. | Project ID: {ProjectId}", id);
                throw new UDValiationException("NO_CORE_TEAM_MEMBER");
            }

            //Check SupportiveTeamMembers are Valid
            var supportiveTeamMembers = request.SupportiveTeamMembers;            
            if (!supportiveTeamMembers.Any())
            {
                _logger.LogError("DDOT.MPS.Permit.Api.Managers.ProjectManager.Update | Supportive team members array is empty. | Project ID: {ProjectId}", id);
                throw new UDValiationException("NO_SUPPORTIVE_TEAM_MEMBER");
            }

            if(!await _userManager.CheckAllUsersExist(coreTeamMembers))
            {
                _logger.LogError("DDOT.MPS.Permit.Api.Managers.ProjectManager.Update | Invalid core team member(s) detected. | Project ID: {ProjectId}", id);
                throw new UDArgumentException("INVALID_CORE_TEAM_MEMBER");
            }

            if (!await _userManager.CheckAllUsersExist(supportiveTeamMembers))
            {
                _logger.LogError("DDOT.MPS.Permit.Api.Managers.ProjectManager.Update | Supportive team member(s) detected. | Project ID: {ProjectId}", id);
                throw new UDArgumentException("INVALID_SUPPORTIVE_TEAM_MEMBER");
            }

            if (request.Locations.Addresses.Count() <= 0 && request.Locations.Blocks.Count() <= 0 && request.Locations.Intersections.Count() <= 0)
            {
                _logger.LogError("DDOT.MPS.Permit.Api.Managers.ProjectManager.Create | Invalid location data. | Project: {request}", JsonSerializer.Serialize(request));
                throw new UDValiationException("INVALID_LOCATION_DATA");
            }

            return new BaseResponse<ProjectResponseDto> 
            { 
                Success = true, 
                Data = await _projectRepository.Update(id, request), 
                Message = "PROJECT_UPDATED_SUCCESSFULLY" 
            };
        }

        private ICollection<ProjectLocation> ProjectLocationMapper (Location locations)
        {

            ICollection<ProjectLocation> projectLocations = new List<ProjectLocation>();

            if (locations.Addresses != null || locations.Addresses.Count > 0)
            {
                foreach (ProjectLocationAddress address in locations.Addresses)
                {
                    ProjectLocation projectLocation = _mapper.Map<ProjectLocation>(address);
                    projectLocation.LocationTypeId = (int)DDOT.MPS.Permit.Core.Enums.LocationType.Address;
                    projectLocations.Add(projectLocation);
                }
            }

            if (locations.Blocks != null || locations.Blocks.Count > 0)
            {
                foreach (ProjectLocationBlock block in locations.Blocks)
                {
                    ProjectLocation? projectLocation = null;

                    string randomId = _stringUtils.GenerateRandomString();

                    foreach (BlockDetails blockDetails in block.Details)
                    {
                        projectLocation = new ProjectLocation();
                        projectLocation.BlockNo = randomId;
                        projectLocation.MultipleAddress = blockDetails.Address;
                        projectLocation.BlockOfAddress = blockDetails.BlockName;
                        projectLocation.Anc = blockDetails.Anc;
                        projectLocation.ASquare = blockDetails.Square;
                        projectLocation.ALot = blockDetails.SquareLot;
                        projectLocation.Ward = blockDetails.Ward;
                        projectLocation.LocationTypeId = (int)DDOT.MPS.Permit.Core.Enums.LocationType.Block;
                        projectLocation.FullDescription = block.Description;
                    }

                    if (projectLocation != null)
                    {
                        projectLocations.Add(projectLocation);
                    }
                }
            }

            if (locations.Intersections != null || locations.Intersections.Count > 0)
            {
                int index = 0;
                List<KeyValuePair<int, string>> wards = _projectRepository.GetWards();
                List<KeyValuePair<int, string>> ancs = _projectRepository.GetAncs();
                foreach (ProjectLocationIntersection intersection in locations.Intersections)
                {
                    index += 1;
                    if (intersection.Wards != null)
                    {
                        foreach (KeyValuePair<string, string> ward in intersection.Wards)
                        {
                            ProjectLocation? projectLocation = new ProjectLocation()
                            {
                                StName = intersection.StreetOne,
                                StName2 = intersection.StreetTwo,
                                ASquare = intersection.Square,
                                ALot = intersection.SquareLot,
                                Intersection1Id = index,
                                LocationTypeId = (int)DDOT.MPS.Permit.Core.Enums.LocationType.Intersection,
                                ProjectLocationWardAncs = new List<ProjectLocationWardAnc>
                                {
                                    new ProjectLocationWardAnc
                                    {
                                        WardId = wards.FirstOrDefault(x => x.Value == ward.Value).Key,
                                    }
                                }
                            };

                            projectLocations.Add(projectLocation);
                        }
                    }

                    if (intersection.AdvisoryNeighborhoodCommissions != null)
                    {
                        foreach (KeyValuePair<string, string> anc in intersection.AdvisoryNeighborhoodCommissions)
                        {
                            ProjectLocation? projectLocation = new ProjectLocation()
                            {
                                StName = intersection.StreetOne,
                                StName2 = intersection.StreetTwo,
                                ASquare = intersection.Square,
                                ALot = intersection.SquareLot,
                                Intersection1Id = index,
                                LocationTypeId = (int)DDOT.MPS.Permit.Core.Enums.LocationType.Intersection,
                                ProjectLocationWardAncs = new List<ProjectLocationWardAnc>
                                {
                                    new ProjectLocationWardAnc
                                    {
                                        AncId = ancs.FirstOrDefault(x => x.Value == anc.Value).Key,
                                    }
                                }
                            };
                            projectLocations.Add(projectLocation);
                        }
                    }
                }

                    
            }
            

            return projectLocations;
        }

        public async Task<BaseResponse<ProjectResponseDto>> Create(ProjectAddDto request)
        {
            _logger.LogInformation("DDOT.MPS.Permit.Api.Managers.ProjectManager.Create | Request in progress. | Project: {request}", JsonSerializer.Serialize(request));

            int[] coreTeamMembers = request.CoreTeamMembers;
            if (!coreTeamMembers.Any())
            {
                _logger.LogError("DDOT.MPS.Permit.Api.Managers.ProjectManager.Update | Core team members array is empty. | Project: {request}", JsonSerializer.Serialize(request));
                throw new UDValiationException("NO_CORE_TEAM_MEMBER");
            }

            var supportiveTeamMembers = request.SupportiveTeamMembers;
            if (!supportiveTeamMembers.Any())
            {
                _logger.LogError("DDOT.MPS.Permit.Api.Managers.ProjectManager.Update | Supportive team members array is empty. | Project: {request}", JsonSerializer.Serialize(request));
                throw new UDValiationException("NO_SUPPORTIVE_TEAM_MEMBER");
            }

            if (!await _userManager.CheckAllUsersExist(coreTeamMembers))
            {
                _logger.LogError("DDOT.MPS.Permit.Api.Managers.ProjectManager.Update | Invalid core team member(s) detected. | Project: {request}", JsonSerializer.Serialize(request));
                throw new UDArgumentException("INVALID_CORE_TEAM_MEMBER");
            }

            if (!await _userManager.CheckAllUsersExist(supportiveTeamMembers))
            {
                _logger.LogError("DDOT.MPS.Permit.Api.Managers.ProjectManager.Update | Supportive team member(s) detected. | Project: {request}", JsonSerializer.Serialize(request));
                throw new UDArgumentException("INVALID_SUPPORTIVE_TEAM_MEMBER");
            }

            if (await _projectRepository.CheckIfProjectNameUser(request.ProjectName))
            {
                _logger.LogError("DDOT.MPS.Permit.Api.Managers.ProjectManager.Create | Project name already exists. | Project: {request}", JsonSerializer.Serialize(request));
                throw new UDArgumentException("PROJECT_NAME_ALREADY_EXISTS");
            }

            if (request.Locations.Addresses.Count() <= 0 && request.Locations.Blocks.Count() <= 0 && request.Locations.Intersections.Count() <= 0)
            {
                _logger.LogError("DDOT.MPS.Permit.Api.Managers.ProjectManager.Create | Invalid location data. | Project: {request}", JsonSerializer.Serialize(request));
                throw new UDValiationException("INVALID_LOCATION_DATA");
            }

            List <ProjectCoreTeam> projectCoreTeamMembers = new List<ProjectCoreTeam>();
            List<ProjectSupportTeam> supportTeamMembers = new List<ProjectSupportTeam>();

            foreach (int coreTeamMemberId in request.CoreTeamMembers)
            {
                ProjectCoreTeam projectCoreTeamEntity = new ProjectCoreTeam()
                {
                    UserId = coreTeamMemberId,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };

                projectCoreTeamMembers.Add(projectCoreTeamEntity);
            }

            foreach (int supportTeamMemberId in request.SupportiveTeamMembers)
            {

                ProjectSupportTeam projectSuppotTeamEntity = new ProjectSupportTeam()
                {
                    UserId = supportTeamMemberId,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };

                supportTeamMembers.Add(projectSuppotTeamEntity);
            }

            Project project = new Project()
            {
                ProjectName = request.ProjectName,
                ProjectApplicantName = request.ProjectApplicantName,
                ProjectDescription = request.ProjectDescription,
                ProjectStartDate = request.ProjectStartDate,
                ProjectEndDate = request.ProjectEndDate,
                ProjectContactUserId = request.ProjectContactUserId,
                ProjectCoreTeams = projectCoreTeamMembers,
                ProjectSupportTeams = supportTeamMembers,
                BzaZcNumber = request.BzaZcNumber,
                SoSpLtrNumber = request.SoSpLtrNumber,
                DobNumber = request.DobNumber,
                EisfNumber = request.EisfNumber,
                EisfSubmissionDate = request.EisfSubmissionDate,
                IsActive = true,
                IsEisfApproved = request.IsEisfApproved,
                ProjectLocations = ProjectLocationMapper(request.Locations),
                ProjectLocationJson = JsonSerializer.Serialize(request.Locations),
                CreatedDate = DateTime.UtcNow
            };

            await _projectRepository.Create(project);
            ProjectResponseDto projectResponseDto = _mapper.Map<ProjectResponseDto>(project);

            return new BaseResponse<ProjectResponseDto> { Success = true, Data = projectResponseDto, Message = "PROJECT_CREATED_SUCCESSFULLY" };

        }

        public async Task<BaseResponse<DetailedProjectResponseDto>> GetById(int id)
        {
            Project? mpsProject = await _projectRepository.GetById(id);
            IQueryable<Project> projectQ = await _projectRepository.GetByIdQueryable(id);

            if (mpsProject == null)
            {
                throw new UDNotFoundException("PROJECT_NOT_FOUND");
            }

            List<ProjectLocationAddress> addressList = _mapper.Map<List<ProjectLocationAddress>>(projectQ.SelectMany(x => x.ProjectLocations).Where(x => x.LocationTypeId == (int)Core.Enums.LocationType.Address).ToList());
            List<ProjectLocationBlock> blockList = new List<ProjectLocationBlock>();
            List<string> distinctBlockNumbers = projectQ.SelectMany(x => x.ProjectLocations).Where(x => x.LocationTypeId == (int)Core.Enums.LocationType.Block).Select(x => x.BlockNo).Distinct().ToList();
            

            foreach (string blockNo in distinctBlockNumbers)
            {
                List<ProjectLocationBlock> blockDetailsList = projectQ.SelectMany(x => x.ProjectLocations).Where(x => x.BlockNo == blockNo).Select(x => new ProjectLocationBlock
                {
                    Description = x.FullDescription,
                    Details = new List<BlockDetails>
                    {
                        new BlockDetails
                        {
                            Address = x.MultipleAddress,
                            BlockName = x.BlockOfAddress,
                            Anc = x.Anc,
                            Square = x.ASquare,
                            SquareLot = x.ALot,
                            Ward = x.Ward
                        }
                    }
                }).ToList();

                blockList.AddRange(blockDetailsList);
            }

            ProjectTeamMember projectContactUser = mpsProject.ProjectContactUser != null ? new ProjectTeamMember
            {
                UserId = mpsProject.ProjectContactUser.UserId,
                EmailAddress = mpsProject.ProjectContactUser.EmailAddress,
                FirstName = mpsProject.ProjectContactUser.FirstName,
                LastName = mpsProject.ProjectContactUser.LastName,
                IsActive = mpsProject.ProjectContactUser.IsActive,
                MobileNumber = mpsProject.ProjectContactUser.MobileNumber
            } : null;
            DetailedProjectResponseDto detailedProjectListResponseDto = new DetailedProjectResponseDto()
            {
                BzaZcNumber = mpsProject.BzaZcNumber,
                ProjectCreatedBy = mpsProject.CreatedBy,
                ProjectCreatedDate = mpsProject.CreatedDate,
                DobNumber = mpsProject.DobNumber,
                EisfNumber = mpsProject.EisfNumber,
                EisfSubmissionDate = mpsProject.EisfSubmissionDate,
                IsActive = mpsProject.IsActive,
                IsEisfApproved = mpsProject.IsEisfApproved,
                ProjectContactUser = projectContactUser != null ? new ProjectTeamMember()
                {
                    EmailAddress = projectContactUser.EmailAddress,
                    FirstName = projectContactUser.FirstName,
                    LastName = projectContactUser.LastName,
                    IsActive = projectContactUser.IsActive,
                    MobileNumber = projectContactUser.MobileNumber,
                    UserId = projectContactUser.UserId
                } : null,
                ProjectCoreTeams = mpsProject.ProjectCoreTeams.Select(x => new ProjectTeamMember
                {
                    UserId = x.UserId,
                    EmailAddress = x.User.EmailAddress,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    IsActive = x.IsActive,
                    MobileNumber = x.User.MobileNumber
                }).ToList(),
                ProjectSupportTeams = mpsProject.ProjectSupportTeams.Select(x => new ProjectTeamMember
                {
                    UserId = x.UserId,
                    EmailAddress = x.User.EmailAddress,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    IsActive = x.IsActive,
                    MobileNumber = x.User.MobileNumber
                }).ToList(),
                ProjectStartDate = mpsProject.ProjectStartDate,
                ProjectEndDate = mpsProject.ProjectEndDate,
                ProjectDescription = mpsProject.ProjectDescription,
                ProjectApplicantName = mpsProject.ProjectApplicantName,
                ProjectLocationJson = mpsProject.ProjectLocationJson,
                ProjectName = mpsProject.ProjectName,
                SoSpLtrNumber = mpsProject.SoSpLtrNumber,
                ProjectStatusId = mpsProject.ProjectStatusId,
                ProjectSortId = mpsProject.SortId,
                ProjectId = mpsProject.ProjectId,
                ProjectLocationData = new ResponseLocationData
                {
                    Addresses = addressList,
                    Blocks = blockList,
                    Intersections = mpsProject.BzaZcNumber != null ? projectQ.SelectMany(x => x.ProjectLocations).Where(x => x.LocationTypeId == (int)Core.Enums.LocationType.Intersection).Select(x => new ProjectLocationIntersectionReq
                    {
                        StreetOne = x.StName,
                        StreetTwo = x.StName2,
                        Square = x.ASquare,
                        SquareLot = x.ALot,
                        Wards = x.ProjectLocationWardAncs.Select(x => x.WardId.ToString()).ToList(),
                        Ancs = x.ProjectLocationWardAncs.Select(x => x.AncId.ToString()).ToList()
                    }).ToList() : null
                }
            };



            return new BaseResponse<DetailedProjectResponseDto> { Success = true, Data = detailedProjectListResponseDto, Message = "PROJECT_FETCHED_SUCCESSFULLY" };
        }

        public async Task<BaseResponse<Result<ProjectResponseDto>>> GetPaginatedList(ProjectPaginatedRequest request)
        {
            List<Project> projects = await _projectRepository.GetPaginatedList(request);

            foreach (Project? project in projects)
            {
                if (project.ProjectStartDate.HasValue)
                {
                    project.ProjectStartDate = DateTime.SpecifyKind(project.ProjectStartDate.Value, DateTimeKind.Unspecified);
                }
                if (project.ProjectEndDate.HasValue)
                {
                    project.ProjectEndDate = DateTime.SpecifyKind(project.ProjectEndDate.Value, DateTimeKind.Unspecified);
                }
                if (project.EisfSubmissionDate.HasValue)
                {
                    project.EisfSubmissionDate = DateTime.SpecifyKind(project.EisfSubmissionDate.Value, DateTimeKind.Unspecified);
                }
            }

            List<ProjectResponseDto> projectResponseDtos = projects.Select(x => _mapper.Map<ProjectResponseDto>(x)).ToList();

            BaseResponse<Result<ProjectResponseDto>> response = new BaseResponse<Result<ProjectResponseDto>>
            {
                Success = true,
                Data = new Result<ProjectResponseDto>
                {
                    Entities = projectResponseDtos.ToArray(),
                    Pagination = new Pagination
                    {
                        Length = projectResponseDtos.Count
                    }
                }
            };

            return response;
        }


    }
}
