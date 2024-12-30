using AutoMapper;
using DDOT.MPS.Permit.Api.Managers;
using DDOT.MPS.Permit.Api.Managers.UserManager;
using DDOT.MPS.Permit.Core.Utilities;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.DataAccess.Repositories;
using DDOT.MPS.Permit.DataAccess.Repositories.UserRepository;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using static DDOT.MPS.Permit.Core.Exceptions.UserDefinedException;

namespace DDOT.MPS.Permit.Test.Managers
{
    [TestFixture]
    public class ProjectManagerTest
    {
        private Mock<IMpsProjectRepository> _projectRepository;
        private Mock<IMapper> _mapper;
        private ProjectManager _projectManager;
        private UserManager _userManager;
        private Mock<IMpsUserRepository> _userRepository;
        private Mock<ILogger<ProjectManager>> _logger;
        private Mock<IStringUtils> _stringUtils;

        [SetUp]
        public void Setup()
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
        public async Task GetById_ShouldReturnSuccessResponse_WhenProjectIsFound()
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

            var result = await _projectManager.GetById(projectId);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("PROJECT_FETCHED_SUCCESSFULLY", result.Message);
            Assert.IsNotNull(result.Data);
            
        }

        [Test]
        public void GetById_ShouldThrowUDNotFoundException_WhenProjectIsNotFound()
        {
            int projectId = 1;
            _projectRepository.Setup(x => x.GetById(projectId)).ReturnsAsync((Project)null);

            UDNotFoundException exception = Assert.ThrowsAsync<UDNotFoundException>(() => _projectManager.GetById(projectId));
            Assert.AreEqual("PROJECT_NOT_FOUND", exception.Message);

            _projectRepository.Verify(x => x.GetById(projectId), Times.Once);
            _mapper.Verify(x => x.Map<ProjectResponseDto>(It.IsAny<Project>()), Times.Never);
        }
    }
}
