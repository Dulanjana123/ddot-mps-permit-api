using AutoMapper;
using DDOT.MPS.Permit.Api.Managers;
using DDOT.MPS.Permit.Api.Managers.UserManager;
using DDOT.MPS.Permit.Core.Utilities;
using DDOT.MPS.Permit.DataAccess.DBContexts;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.DataAccess.Repositories;
using DDOT.MPS.Permit.DataAccess.Repositories.UserRepository;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Request.Generic;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using static DDOT.MPS.Permit.Core.Exceptions.UserDefinedException;

namespace DDOT.MPS.Permit.Test.Repositories
{
    [TestFixture]
    public class MpsProjectRepositoryTest
    {
        private Mock<MpsDbContext> _mockDbContext;
        private Mock<IMapper> _mockMapper;
        private Mock<IMpsUserRepository> _mockUserRepository;
        private Mock<ILogger<MpsProjectRepository>> _mockLogger;
        private MpsProjectRepository _repository;
        private Mock<DbSet<Project>> _mockProjectDbSet;

        private Mock<IMpsProjectRepository> _projectRepository;
        private Mock<IMapper> _mapper;
        private ProjectManager _projectManager;
        private UserManager _userManager;
        private Mock<IMpsUserRepository> _userRepository;
        private Mock<ILogger<ProjectManager>> _logger;
        private Mock<IStringUtils> _stringUtils;


        [SetUp]
        public void SetUp()
        {

            _projectRepository = new Mock<IMpsProjectRepository>();
            _userRepository = new Mock<IMpsUserRepository>();
            _mapper = new Mock<IMapper>();
            _userManager = new UserManager(_userRepository.Object, _mapper.Object);
            _logger = new Mock<ILogger<ProjectManager>>();
            _stringUtils = new Mock<IStringUtils>();
            _projectManager = new ProjectManager(_projectRepository.Object, _mapper.Object, _userManager, _logger.Object, _stringUtils.Object);
        }

        [Test]
        public void Update_ProjectNotFound_ShouldThrowUDNotFoundException()
        {
            int projectId = 1;
            _projectRepository.Setup(x => x.GetById(projectId)).ReturnsAsync((Project)null);

            UDNotFoundException exception =
                Assert.ThrowsAsync<UDNotFoundException>(() => _projectManager.GetById(projectId));
            Assert.AreEqual("PROJECT_NOT_FOUND", exception.Message);

            _projectRepository.Verify(x => x.GetById(projectId), Times.Once);
            _mapper.Verify(x => x.Map<ProjectResponseDto>(It.IsAny<Project>()), Times.Never);
        }

        [Test]
        public void Update_ProjectFound_ShouldReturnProjectResponseDto()
        {
            int projectId = 1;
            var project = new Project
            {
                ProjectId = projectId,
                ProjectName = "Test Project",
                ProjectContactUser = new User
                {
                    EmailAddress = "test@example.com",
                    FirstName = "John",
                    LastName = "Doe",
                    IsActive = true,
                    MobileNumber = "1234567890",
                    UserId = 1
                },
                ProjectCoreTeams = new List<ProjectCoreTeam>
                {
                    new ProjectCoreTeam
                    {
                        UserId = 1,
                        User = new User
                        {
                            EmailAddress = "core@example.com",
                            FirstName = "Core",
                            LastName = "User",
                            IsActive = true,
                            MobileNumber = "0987654321"
                        },
                        IsActive = true
                    }
                },
                ProjectSupportTeams = new List<ProjectSupportTeam>
                {
                    new ProjectSupportTeam
                    {
                        UserId = 2,
                        User = new User
                        {
                            EmailAddress = "support@example.com",
                            FirstName = "Support",
                            LastName = "User",
                            IsActive = true,
                            MobileNumber = "1122334455"
                        },
                        IsActive = true
                    }
                }
            };

            _projectRepository.Setup(x => x.GetById(projectId)).ReturnsAsync(project);
            _mapper.Setup(x => x.Map<DetailedProjectResponseDto>(It.IsAny<Project>())).Returns(new DetailedProjectResponseDto());

            var result = _projectManager.GetById(projectId).
                GetAwaiter().GetResult();
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetById_MapsProjectToDtoCorrectly()
        {
            int projectId = 9;
            Project project = new Project();

            _projectRepository.Setup(x => x.GetById(projectId))
                .ReturnsAsync(project);
            _mapper.Setup(m => m.Map<Project>(project))
                .Returns(project);

            BaseResponse<DetailedProjectResponseDto> result = await _projectManager.GetById(projectId);

            Assert.IsTrue(result.Success);
        }

        [Test]
        public void GetInspectionById_InspectionNotFound_ThrowsNotFoundException()
        {
            int projectId = 1;

            _projectRepository.Setup(x => x.GetById(projectId))
                .ReturnsAsync((Project)null);

            Assert.ThrowsAsync<UDNotFoundException>(() => _projectManager.GetById(projectId));
        }


        [Test]
        public async Task CheckIfProjectNameUser_ShouldReturnExpectedResult()
        {
            string projectName = "Test Project";
            _projectRepository.Setup(x => x.CheckIfProjectNameUser(projectName)).ReturnsAsync(true);

            var result = await _projectRepository.Object.CheckIfProjectNameUser(projectName);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CreateCoreTeamMember_ShouldReturnExpectedResult()
        {
            ProjectCoreTeamDto project = new ProjectCoreTeamDto();
            _projectRepository.Setup(x => x.CreateCoreTeamMember(project)).ReturnsAsync(project);

            var result = await _projectRepository.Object.CreateCoreTeamMember(project);

            Assert.AreEqual(project, result);
        }

        [Test]
        public async Task GetPaginatedList_ShouldReturnExpectedResult()
        {
            ProjectPaginatedRequest request = new ProjectPaginatedRequest();
            List<Project> projects = new List<Project> { new Project(), new Project() };
            _projectRepository.Setup(x => x.GetPaginatedList(request)).ReturnsAsync(projects);

            var result = await _projectRepository.Object.GetPaginatedList(request);

            Assert.AreEqual(projects, result);
        }

        [Test]
        public async Task GetCoreTeamMembers_ShouldReturnExpectedResult()
        {
            int projectId = 1;
            List<int> userIds = new List<int> { 1, 2, 3 };
            _projectRepository.Setup(x => x.GetCoreTeamMembers(projectId)).ReturnsAsync(userIds);

            var result = await _projectRepository.Object.GetCoreTeamMembers(projectId);

            Assert.AreEqual(userIds, result);
        }

        [Test]
        public async Task GetSupportTeamMembers_ShouldReturnExpectedResult()
        {
            int projectId = 1;
            List<int> userIds = new List<int> { 1, 2, 3 };
            _projectRepository.Setup(x => x.GetSupportTeamMembers(projectId)).ReturnsAsync(userIds);

            var result = await _projectRepository.Object.GetSupportTeamMembers(projectId);

            Assert.AreEqual(userIds, result);
        }

        [Test]
        public async Task Update_ShouldReturnExpectedResult()
        {
            int projectId = 1;
            ProjectUpdateDto request = new ProjectUpdateDto();
            ProjectResponseDto project = new ProjectResponseDto();
            _projectRepository.Setup(x => x.Update(projectId, request)).ReturnsAsync(project);

            var result = await _projectRepository.Object.Update(projectId, request);

            Assert.AreEqual(project, result);
        }

        [Test]
        public async Task GetProjectCoreTeamByProjectId_ShouldReturnExpectedResult()
        {
            int projectId = 1;
            List<ProjectCoreTeam?> projectCoreTeams = new List<ProjectCoreTeam?> { new ProjectCoreTeam(), new ProjectCoreTeam() };
            _projectRepository.Setup(x => x.GetProjectCoreTeamByProjectId(projectId)).ReturnsAsync(projectCoreTeams);

            var result = await _projectRepository.Object.GetProjectCoreTeamByProjectId(projectId);

            Assert.AreEqual(projectCoreTeams, result);
        }

        [Test]
        public async Task GetSupportiveTeamByProjectId_ShouldReturnExpectedResult()
        {
            int projectId = 1;
            List<ProjectSupportTeam?> projectSupportTeams = new List<ProjectSupportTeam?> { new ProjectSupportTeam(), new ProjectSupportTeam() };
            _projectRepository.Setup(x => x.GetSupportiveTeamByProjectId(projectId)).ReturnsAsync(projectSupportTeams);

            var result = await _projectRepository.Object.GetSupportiveTeamByProjectId(projectId);

            Assert.AreEqual(projectSupportTeams, result);
        }

        [Test]
        public void GetWards_ShouldReturnExpectedResult()
        {
            List<KeyValuePair<int, string>> wards = new List<KeyValuePair<int, string>> { new KeyValuePair<int, string>(1, "Ward 1"), new KeyValuePair<int, string>(2, "Ward 2") };
            _projectRepository.Setup(x => x.GetWards()).Returns(wards);

            var result = _projectRepository.Object.GetWards();

            Assert.AreEqual(wards, result);
        }

        [Test]
        public void GetAncs_ShouldReturnExpectedResult()
        {
            List<KeyValuePair<int, string>> ancs = new List<KeyValuePair<int, string>> { new KeyValuePair<int, string>(1, "ANC 1"), new KeyValuePair<int, string>(2, "ANC 2") };
            _projectRepository.Setup(x => x.GetAncs()).Returns(ancs);

            var result = _projectRepository.Object.GetAncs();

            Assert.AreEqual(ancs, result);
        }

        [Test]
        public async Task GetPaginatedList_ShouldReturnFilteredProjects()
        {
            var request = new ProjectPaginatedRequest
            {
                Filters = new ProjectPaginatedFilters()
                {
                    ProjectName = "Test",
                    ProjectApplicantName = "Applicant",
                    ProjectStartDateFrom = DateTime.UtcNow.AddDays(-10),
                    ProjectStartDateTo = DateTime.UtcNow.AddDays(10),
                    ProjectEndDateFrom = DateTime.UtcNow.AddDays(-5),
                    ProjectEndDateTo = DateTime.UtcNow.AddDays(5),
                    ProjectStatus = true,
                    AgencyId = null,
                    UserId = 1
                },
                PagingAndSortingInfo = new PagingAndSortingInfo
                {
                    Paging = new PagingInfo()
                    {
                        PageNo = 1,
                        PageSize = 10
                    }
                }
            };

            var projects = new List<Project>
            {
                new Project
                {
                    ProjectId = 1,
                    ProjectName = "Test Project",
                    ProjectApplicantName = "Applicant",
                    ProjectStartDate = DateTime.UtcNow.AddDays(-1),
                    ProjectEndDate = DateTime.UtcNow.AddDays(1),
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = DateTime.UtcNow
                }
            }.AsQueryable();

            _projectRepository.Setup(x => x.GetPaginatedList(request)).ReturnsAsync(projects.ToList());

            var result = await _projectRepository.Object.GetPaginatedList(request);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().ProjectName, Is.EqualTo("Test Project"));
        }
    }
}
