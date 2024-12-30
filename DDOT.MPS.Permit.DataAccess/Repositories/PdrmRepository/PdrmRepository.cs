using AutoMapper;
using DDOT.MPS.Permit.Core.Constants;
using DDOT.MPS.Permit.DataAccess.DBContexts;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DDOT.MPS.Permit.DataAccess.Repositories
{
    public class PdrmRepository : IPdrmRepository
    {
        private readonly MpsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<PdrmRepository> _logger;
        private readonly string _projectLocationAddressCode;
        private readonly string _projectLocationBlocksCode;
        private readonly string[] _pdrmAllowedLocationCodes;

        public PdrmRepository(MpsDbContext dbContext, IMapper mapper, ILogger<PdrmRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _projectLocationAddressCode = PdrmConstants.ProjectLocationAddressCode;
            _projectLocationBlocksCode = PdrmConstants.ProjectLocationBlockCode;
            _pdrmAllowedLocationCodes = new[] { _projectLocationAddressCode, _projectLocationBlocksCode };
        }

        public async Task<int> CreatePdrm(int pdrMeetingType)
        {
            PdrmApplication pdrmApplication = new PdrmApplication
            {
                MeetingTypeId = pdrMeetingType,
                IsActive = true
            };
            await _dbContext.PdrmApplications.AddAsync(pdrmApplication);
            await _dbContext.SaveChangesAsync();
            return pdrmApplication.PdrmApplicationId;
        }

        public async Task<bool> PdrmExists(int pdrmId)
        {
            return await _dbContext.PdrmApplications.AnyAsync(p => p.PdrmApplicationId == pdrmId && p.IsActive);
        }

        public async Task<PdrmDto> GetPdrmById(int pdrmId)
        {
            PdrmApplication dbPdrmApplication = await _dbContext.PdrmApplications
                .Where(p => p.IsActive && p.PdrmApplicationId == pdrmId)
                .Include(p => p.PdrmLocations)
                .Include(p => p.Project)
                    .ThenInclude(p => p.ProjectLocations)                    
                        .ThenInclude(l => l.LocationType)
                    .Include(p => p.Project.ProjectLocations)
                        .ThenInclude(l => l.LocationCategory)
                    .Include(p => p.Project.ProjectLocations)
                        .ThenInclude(p => p.CreatedUser)
                .AsNoTracking()
                .FirstAsync();         

            return GetPdrmDtoByPdrmApplication(dbPdrmApplication);
        }

        private PdrmDto GetPdrmDtoByPdrmApplication(PdrmApplication dbPdrmApplication)
        {
            Project? dbProject = dbPdrmApplication.Project;
            PdrmProjectDto pdrmProject = null;
            if (dbProject != null) {
                pdrmProject = new PdrmProjectDto
                {
                    Id = dbProject.ProjectId,
                    Name = dbProject.ProjectName,
                    Description = dbProject.ProjectDescription,
                    StartDate = dbProject.ProjectStartDate ?? DateTime.MinValue,
                    EndDate = dbProject.ProjectEndDate ?? DateTime.MinValue,
                    Locations = GetPdrmProjectLocationDtos(dbProject.ProjectLocations.ToList())
                };
            }

            return new PdrmDto 
            {
                Id = dbPdrmApplication.PdrmApplicationId,
                SelectedProjectLocations = dbPdrmApplication.PdrmLocations.Select(l => l.ProjectLocationId).ToList(),
                Project = pdrmProject
            };
        }

        private List<PdrmProjectLocationDto> GetPdrmProjectLocationDtos(IList<ProjectLocation> dbProjectLocations)
        {
            List<PdrmProjectLocationDto> pdrmProjectLocationDtos = new List<PdrmProjectLocationDto>();
            dbProjectLocations = dbProjectLocations.Where(l => !string.IsNullOrWhiteSpace(l.LocationType.LocationTypeCode) && _pdrmAllowedLocationCodes.Contains(l.LocationType.LocationTypeCode)).ToList();
            SetPdrmProjectAddressLocationDtos(dbProjectLocations, pdrmProjectLocationDtos);

            List<ProjectLocation> projectBlockLocations = dbProjectLocations.Where(l => !string.IsNullOrWhiteSpace(l.BlockNo) && l.LocationType.LocationTypeCode.Equals(_projectLocationBlocksCode, StringComparison.InvariantCultureIgnoreCase)).ToList();
            SetPdrmProjectBlockLocationDtos(pdrmProjectLocationDtos, projectBlockLocations);

            return pdrmProjectLocationDtos;
        }

        private void SetPdrmProjectAddressLocationDtos(IList<ProjectLocation> dbProjectLocations, List<PdrmProjectLocationDto> pdrmProjectLocationDtos)
        {
            List<ProjectLocation> dbProjectAddressLocations = dbProjectLocations.Where(l => l.LocationType.LocationTypeCode.Equals(_projectLocationAddressCode, StringComparison.InvariantCultureIgnoreCase)).ToList();
            foreach (ProjectLocation dbProjectAddressLocation in dbProjectAddressLocations)
            {
                pdrmProjectLocationDtos.Add(new PdrmProjectLocationDto
                {
                    ProjectLocationId = dbProjectAddressLocation.ProjectLocationId,
                    Type = _projectLocationAddressCode,
                    Address = new PdrmProjectLocationAddressDto
                    {
                        Category = dbProjectAddressLocation.LocationCategory.LocationCategoryName,
                        Description = dbProjectAddressLocation.FullDescription,
                        CreatedUserName = $"{dbProjectAddressLocation.CreatedUser?.FirstName} {dbProjectAddressLocation.CreatedUser?.LastName}".Trim(),
                        CreatedDate = dbProjectAddressLocation.CreatedDate ?? DateTime.MaxValue,
                        Ward = "",
                        Anc = "",
                        Square = dbProjectAddressLocation.ASquare ?? string.Empty,
                        SquareLot = dbProjectAddressLocation.ALot ?? string.Empty,
                    }
                });
            }
        }

        private void SetPdrmProjectBlockLocationDtos(List<PdrmProjectLocationDto> pdrmProjectLocationDtos, List<ProjectLocation> projectBlockLocations)
        {
            List<string> uniqueBlockNumbers = projectBlockLocations.Select(b => b.BlockNo!).Distinct().ToList();

            foreach (var blockNumber in uniqueBlockNumbers)
            {
                List<ProjectLocation> groupedProjectBlockLocations = projectBlockLocations.Where(l => blockNumber.Equals(l.BlockNo, StringComparison.InvariantCultureIgnoreCase)).ToList();
                ProjectLocation? firstprojectBlockLocation = groupedProjectBlockLocations.FirstOrDefault();
                if (firstprojectBlockLocation == null) continue;

                List<PdrmProjectLocationBlockDataDto> pdrmProjectLocationBlockDataList = new List<PdrmProjectLocationBlockDataDto>();
                foreach (ProjectLocation groupedProjectBlockLocation in groupedProjectBlockLocations)
                {
                    pdrmProjectLocationBlockDataList.Add(new PdrmProjectLocationBlockDataDto
                    {
                        Address = groupedProjectBlockLocation.MultipleAddress ?? string.Empty,
                        Square = groupedProjectBlockLocation.ASquare ?? string.Empty,
                        SquareLot = groupedProjectBlockLocation.ALot ?? string.Empty,
                        Suffix = groupedProjectBlockLocation.BlockOfAddress ?? string.Empty,
                        Ward = ""
                    });
                }

                pdrmProjectLocationDtos.Add(new PdrmProjectLocationDto
                {
                    ProjectLocationId = firstprojectBlockLocation.ProjectLocationId,
                    Type = _projectLocationBlocksCode,
                    Block = new PdrmProjectLocationBlockDto
                    {
                        Category = firstprojectBlockLocation.LocationCategory.LocationCategoryName,
                        Description = firstprojectBlockLocation.FullDescription,
                        CreatedUserName = $"{firstprojectBlockLocation.CreatedUser?.FirstName} {firstprojectBlockLocation.CreatedUser?.LastName}".Trim(),
                        CreatedDate = firstprojectBlockLocation.CreatedDate ?? DateTime.MaxValue,
                        PdrmProjectLocationBlockDataList = pdrmProjectLocationBlockDataList
                    }
                });
            }
        }
    }
}
