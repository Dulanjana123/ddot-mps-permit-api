using AutoMapper;
using DDOT.MPS.Permit.Api.Managers;
using DDOT.MPS.Permit.DataAccess.Repositories.EwrRepository;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;
using Moq;
using NUnit.Framework;
using DDOT.MPS.Permit.Model.Request.Generic;
using System.Linq.Expressions;
using Test.Helpers;
using DDOT.MPS.Permit.DataAccess.Repositories.UserRepository;
using DDOT.MPS.Permit.DataAccess.Repositories.InspectionRepository;
using DDOT.MPS.Permit.DataAccess.Entities;
using static DDOT.MPS.Permit.Core.Exceptions.UserDefinedException;
using DDOT.MPS.Permit.DataAccess.Repositories.SwoRepository;

namespace DDOT.MPS.Permit.Api.Tests.Managers.EwrManagement
{
    [TestFixture]
    public class EwrManagerTest
    {
        private EwrManager _ewrManager;
        private Mock<IMpsEwrRepository> _ewrRepositoryMock;
        private Mock<ISwoRepository> _swoRepositoryMock;
        private Mock<IMpsUserRepository> _userRepositoryMock;
        private Mock<IInspectionRepository> _inspectionRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            // Create mock instances
            _ewrRepositoryMock = new Mock<IMpsEwrRepository>();
            _swoRepositoryMock = new Mock<ISwoRepository>();
            _userRepositoryMock = new Mock<IMpsUserRepository>();
            _inspectionRepositoryMock = new Mock<IInspectionRepository>();
            _mapperMock = new Mock<IMapper>();

            // Initialize EwrManager with mocked dependencies
            _ewrManager = new EwrManager(_ewrRepositoryMock.Object, _swoRepositoryMock.Object, _userRepositoryMock.Object, _inspectionRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task GetPaginatedList_ShouldReturnCorrectResponse_WhenDataExists()
        {
            // Arrange
            EwrPaginatedRequest request = new EwrPaginatedRequest
            {
                Filters = new EwrPaginatedFilters
                {
                    RequestNumber = "TestRequest",
                },
                PagingAndSortingInfo = new PagingAndSortingInfo
                {
                    Paging = new PagingInfo
                    {
                        PageNo = 1,
                        PageSize = 10
                    }
                }
            };

            IQueryable<EwrResponseDto> ewrResponseDtos = new List<EwrResponseDto>
            {
                new EwrResponseDto { RequestId = 1, RequestNumber = "TestRequest1" },
                new EwrResponseDto { RequestId = 2, RequestNumber = "TestRequest2" }
            }.AsQueryable();

            // Mock the IQueryable to behave asynchronously
            TestAsyncEnumerable<EwrResponseDto> mockEwrResponseDtos = new TestAsyncEnumerable<EwrResponseDto>(ewrResponseDtos);

            _ewrRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<EwrPaginatedRequest>()))
                .Returns(mockEwrResponseDtos);

            _ewrRepositoryMock.Setup(repo => repo.GetRowCount(It.IsAny<EwrPaginatedRequest>()))
                .Returns(2);

            // Act
            BaseResponse<Result<EwrResponseDto>> result = await _ewrManager.GetPaginatedList(request);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, result.Data.Entities.Length);
            Assert.AreEqual(2, result.Data.Pagination.Length);
        }

        [Test]
        public async Task GetPaginatedList_ShouldReturnEmptyResponse_WhenNoDataExists()
        {
            // Arrange
            EwrPaginatedRequest request = new EwrPaginatedRequest
            {
                Filters = new EwrPaginatedFilters(),
                PagingAndSortingInfo = new PagingAndSortingInfo
                {
                    Paging = new PagingInfo
                    {
                        PageNo = 1,
                        PageSize = 10
                    }
                }
            };

            _ewrRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<EwrPaginatedRequest>()))
                .Returns(new TestAsyncEnumerable<EwrResponseDto>(Enumerable.Empty<EwrResponseDto>().AsQueryable()));

            _ewrRepositoryMock.Setup(repo => repo.GetRowCount(It.IsAny<EwrPaginatedRequest>()))
                .Returns(0);

            // Act
            BaseResponse<Result<EwrResponseDto>> result = await _ewrManager.GetPaginatedList(request);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.Data.Entities.Length);
            Assert.AreEqual(0, result.Data.Pagination.Length);
        }

        [Test]
        public async Task GetEwrDashboardData_ShouldReturnCorrectDashboardData()
        {
            // Arrange
            DateRangeRequest request = new DateRangeRequest
            {
                StartDate = DateTime.UtcNow.AddDays(-30),
                EndDate = DateTime.UtcNow
            };

            _ewrRepositoryMock.Setup(repo => repo.GetTotalEwrCount(It.IsAny<DateRangeRequest>()))
                .Returns(10); // Mock total EWR count

            // Mock data for each chart
            IQueryable<AgencyEwrCountDto> agencyEwrCounts = new List<AgencyEwrCountDto>
            {
                new AgencyEwrCountDto { UtilityCompany = "sample_company_1", RequestCount = 5 },
                new AgencyEwrCountDto { UtilityCompany = "sample_company_2", RequestCount = 5 }
            }.AsQueryable();

            _ewrRepositoryMock.Setup(repo => repo.GetEwrCountByUtilityCompany(It.IsAny<DateRangeRequest>()))
                .Returns(agencyEwrCounts);

            IQueryable<WardEwrCountDto> wardEwrCounts = new List<WardEwrCountDto>
            {
                new WardEwrCountDto { Ward = "4", RequestCount = 6 },
                new WardEwrCountDto { Ward = "2", RequestCount = 4 }
            }.AsQueryable();

            _ewrRepositoryMock.Setup(repo => repo.GetEwrCountByWard(It.IsAny<DateRangeRequest>()))
                .Returns(wardEwrCounts);

            IQueryable<EmergencyTypeEwrCountDto> emergencyTypeEwrCounts = new List<EmergencyTypeEwrCountDto>
            {
                new EmergencyTypeEwrCountDto { EmergencyType = "Electric Overhead", RequestCount = 7 },
                new EmergencyTypeEwrCountDto { EmergencyType = "Electric Overhead", RequestCount = 3 }
            }.AsQueryable();

            _ewrRepositoryMock.Setup(repo => repo.GetEwrCountByEmergencyType(It.IsAny<DateRangeRequest>()))
                .Returns(emergencyTypeEwrCounts);

            IQueryable<StatusEwrCountDto> statusEwrCounts = new List<StatusEwrCountDto>
            {
                new StatusEwrCountDto { Status = "Cancelled", RequestCount = 8 },
                new StatusEwrCountDto { Status = "Cancelled", RequestCount = 2 }
            }.AsQueryable();

            _ewrRepositoryMock.Setup(repo => repo.GetEwrCountByStatus(It.IsAny<DateRangeRequest>()))
                .Returns(statusEwrCounts);

            IQueryable<InspectorCountDto> inspectorCounts = new List<InspectorCountDto>
            {
                new InspectorCountDto { InspectorName = "User1", TotalInspectCount = 8 },
                new InspectorCountDto { InspectorName = "User2", TotalInspectCount = 2 }
            }.AsQueryable();

            _ewrRepositoryMock.Setup(repo => repo.GetInspectedCountByInspector(It.IsAny<DateRangeRequest>()))
                .Returns(inspectorCounts);

            // Act
            BaseResponse<EwrDashboardResponseDto> result = await _ewrManager.GetEwrDashboardData(request);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(10, result.Data.TotalEwrCount);
            Assert.AreEqual(2, result.Data.EwrVsAgencyChartData.Count);
            Assert.AreEqual(2, result.Data.WardVsEwrChartData.Count);
            Assert.AreEqual(2, result.Data.EmergencyTypeVsEwrChartData.Count);
            Assert.AreEqual(2, result.Data.StatusVsEwrChartData.Count);
            Assert.AreEqual(2, result.Data.InspectorCountChartData.Count);
        }

        [Test]
        public async Task GetEwrDashboardData_ShouldReturnEmptyData_WhenNoDataExists()
        {
            // Arrange
            DateRangeRequest request = new DateRangeRequest
            {
                StartDate = DateTime.UtcNow.AddDays(-30),
                EndDate = DateTime.UtcNow
            };

            _ewrRepositoryMock.Setup(repo => repo.GetTotalEwrCount(It.IsAny<DateRangeRequest>()))
                .Returns(0);

            _ewrRepositoryMock.Setup(repo => repo.GetEwrCountByUtilityCompany(It.IsAny<DateRangeRequest>()))
                .Returns(Enumerable.Empty<AgencyEwrCountDto>().AsQueryable());

            _ewrRepositoryMock.Setup(repo => repo.GetEwrCountByWard(It.IsAny<DateRangeRequest>()))
                .Returns(Enumerable.Empty<WardEwrCountDto>().AsQueryable());

            _ewrRepositoryMock.Setup(repo => repo.GetEwrCountByEmergencyType(It.IsAny<DateRangeRequest>()))
                .Returns(Enumerable.Empty<EmergencyTypeEwrCountDto>().AsQueryable());

            _ewrRepositoryMock.Setup(repo => repo.GetEwrCountByStatus(It.IsAny<DateRangeRequest>()))
                .Returns(Enumerable.Empty<StatusEwrCountDto>().AsQueryable());

            _ewrRepositoryMock.Setup(repo => repo.GetAllByDateRange(It.IsAny<DateRangeRequest>()))
               .Returns(Enumerable.Empty<EwrResponseDto>().AsQueryable());

            // Act
            BaseResponse<EwrDashboardResponseDto> result = await _ewrManager.GetEwrDashboardData(request);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(0, result.Data.TotalEwrCount);
            Assert.AreEqual(0, result.Data.EwrVsAgencyChartData.Count);
            Assert.AreEqual(0, result.Data.WardVsEwrChartData.Count);
            Assert.AreEqual(0, result.Data.EmergencyTypeVsEwrChartData.Count);
            Assert.AreEqual(0, result.Data.StatusVsEwrChartData.Count);
            Assert.AreEqual(0, result.Data.EwrData.Count);
        }

        [Test]
        public async Task GetAssigningInfo_ShouldReturnCorrectAssigningInfoData()
        {
            // Arrange
            IQueryable<UserOption> inspectors = new List<UserOption>
            {
                new UserOption { UserId = 1, FullName = "Inspector A" },
                new UserOption { UserId = 2, FullName = "Inspector B" }
            }.AsQueryable();

            var inspStatuses = new List<InspStatusOption>
            {
                new InspStatusOption { InspStatusId = 1, InspStatusDesc = "In Progress" },
                new InspStatusOption { InspStatusId = 2, InspStatusDesc = "Completed" }
            }.AsQueryable();

            var ewrStatuses = new List<EwrStatusOption>
            {
                new EwrStatusOption { StatusId = 1, StatusDesc = "Open" },
                new EwrStatusOption { StatusId = 2, StatusDesc = "Closed" }
            }.AsQueryable();

            _ewrRepositoryMock.Setup(repo => repo.GetAllEwrStatuses())
                .Returns(ewrStatuses);

            _userRepositoryMock.Setup(repo => repo.GetAllInspectors())
                .Returns(inspectors);

            _inspectionRepositoryMock.Setup(repo => repo.GetAllInspectionStatuses())
                .Returns(inspStatuses);

            // Act
            BaseResponse<AssigningInfoResponseDto> result = await _ewrManager.GetAssigningInfo();

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, result.Data.Inspectors.Count);
            Assert.AreEqual("Inspector A", result.Data.Inspectors[0].FullName);
            Assert.AreEqual(2, result.Data.InspStatuses.Count);
            Assert.AreEqual("In Progress", result.Data.InspStatuses[0].InspStatusDesc);
            Assert.AreEqual(2, result.Data.EwrStatuses.Count);
            Assert.AreEqual("Open", result.Data.EwrStatuses[0].StatusDesc);
            Assert.AreEqual("ASSIGNING_INFO_RETRIEVED", result.Message);
        }

        [Test]
        public async Task UpdateEwrAssigning_ShouldReturnSuccess_WhenEwrExists()
        {
            // Arrange
            int ewrId = 1;
            EwrAssigningDto ewrAssigningDto = new EwrAssigningDto
            {
                AssigneeId = 2,
                InspStatusId = 3,
                Comments = "Test comment"
            };

            EwrApplication existingEwrApplication = new EwrApplication
            {
                EwrApplicationId = ewrId,
                AssignedInspector = 1,
                StatusId = 1,
            };

            _ewrRepositoryMock.Setup(repo => repo.GetById(ewrId))
                              .ReturnsAsync(existingEwrApplication);

            // Act
            BaseResponse<EwrAssigningDto> result = await _ewrManager.UpdateEwrAssigning(ewrId, ewrAssigningDto);

            // Assert
            Assert.True(result.Success);
            Assert.AreEqual("EWR_ASSIGNED_SUCCESSFULLY", result.Message);
            Assert.AreEqual(ewrAssigningDto, result.Data);

            _ewrRepositoryMock.Verify(repo => repo.UpdateEwrApplication(It.Is<EwrApplication>(
                app => app.AssignedInspector == ewrAssigningDto.AssigneeId
            )), Times.Once);
        }


        [Test]
        public async Task UpdateEwrAssigning_ShouldThrowNotFoundException_WhenEwrDoesNotExist()
        {
            // Arrange
            int ewrId = 1;
            EwrAssigningDto ewrAssigningDto = new EwrAssigningDto
            {
                AssigneeId = 2,
                InspStatusId = 3,
                Comments = "Test comment"
            };

            _ewrRepositoryMock.Setup(repo => repo.GetById(ewrId))
                              .ReturnsAsync((EwrApplication)null);

            UDNotFoundException exception = Assert.ThrowsAsync<UDNotFoundException>(() => _ewrManager.UpdateEwrAssigning(ewrId, ewrAssigningDto));

            Assert.AreEqual("EWR_NOT_FOUND", exception.Message);
            _ewrRepositoryMock.Verify(repo => repo.UpdateEwrApplication(It.IsAny<EwrApplication>()), Times.Never);
        }

        [Test]
        public async Task CreateNewEwr_ShouldReturnSuccessResponse_WhenRequestIsValid()
        {
            // Arrange
            EwrCreateRequest request = new EwrCreateRequest
            {
                EmergencyCategoryId = 1,
                EmergencyCauseId = 1,
                EmergencyTypeId = 1
            };

            var ewrApplication = new EwrApplication(); 
            var ewrResponse = new EwrCreateResponse(); 

            _ewrRepositoryMock
                .Setup(repo => repo.CreateEwr(It.IsAny<EwrCreateRequest>()))
                .ReturnsAsync(ewrApplication);

            _mapperMock
                .Setup(mapper => mapper.Map<EwrCreateResponse>(ewrApplication))
                .Returns(ewrResponse);

            // Act
            var response = await _ewrManager.CreateEwr(request);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.AreEqual("EWR_CREATED_SUCCESSFULLY", response.Message);
            Assert.AreEqual(ewrResponse, response.Data);
        }

        [Test]
        public async Task CreateNewEwr_ShouldThrowException_WhenRequestIsNull()
        {
            // Act & Assert
            UDValiationException exception = Assert.ThrowsAsync<UDValiationException>(() => _ewrManager.CreateEwr(null));
            Assert.AreEqual("INVALID_REQUEST", exception.Message);
        }

        [Test]
        public async Task CreateNewEwr_ShouldThrowException_WhenEmergencyCategoryIdIsZero()
        {
            // Arrange
            EwrCreateRequest request = new EwrCreateRequest { EmergencyCategoryId = 0, EmergencyCauseId = 1, EmergencyTypeId = 1 };

            // Act & Assert
            UDValiationException exception = Assert.ThrowsAsync<UDValiationException>(() => _ewrManager.CreateEwr(request));
            Assert.AreEqual("INVALID_EMERGENCY_CATEGORY", exception.Message);
        }

        [Test]
        public async Task CreateNewEwr_ShouldThrowException_WhenEmergencyCauseIdIsZero()
        {
            // Arrange
            EwrCreateRequest requestCause = new EwrCreateRequest { EmergencyCategoryId = 1, EmergencyCauseId = 0, EmergencyTypeId = 1 };

            // Act & Assert
            UDValiationException exception = Assert.ThrowsAsync<UDValiationException>(() => _ewrManager.CreateEwr(requestCause));
            Assert.AreEqual("INVALID_EMERGENCY_CAUSE", exception.Message);
        }

        [Test]
        public async Task CreateNewEwr_ShouldThrowException_WhenEmergencyTypeIdIsZero()
        {
            // Arrange
            EwrCreateRequest requestCause = new EwrCreateRequest { EmergencyCategoryId = 1, EmergencyCauseId = 0, EmergencyTypeId = 0 };

            // Act & Assert
            UDValiationException exception = Assert.ThrowsAsync<UDValiationException>(() => _ewrManager.CreateEwr(requestCause));
            Assert.AreEqual("INVALID_EMERGENCY_CAUSE", exception.Message);
        }

        [Test]
        public async Task GetIndexFiltersInfo_ShouldReturnCorrectIndexFiltersInfoData()
        {
            // Arrange
            var ewrStatuses = new List<EwrStatusOption>
        {
            new EwrStatusOption { StatusId = 1, StatusDesc = "Open" },
            new EwrStatusOption { StatusId = 2, StatusDesc = "Closed" }
        }.AsQueryable();

            var ewrEmergencyTypes = new List<EwrEmergencyTypeOption>
        {
            new EwrEmergencyTypeOption { EmergencyTypeId = 1, EmergencyTypeDesc = "Type A" },
            new EwrEmergencyTypeOption { EmergencyTypeId = 2, EmergencyTypeDesc = "Type B" }
        }.AsQueryable();

            var ewrEmergencyCauses = new List<EwrEmergencyCauseOption>
        {
            new EwrEmergencyCauseOption { EmergencyCauseId = 1, EmergencyCauseDesc = "Cause X" },
            new EwrEmergencyCauseOption { EmergencyCauseId = 2, EmergencyCauseDesc = "Cause Y" }
        }.AsQueryable();

            var ewrEmergencyCategories = new List<EwrEmergencyCategoryOption>
        {
            new EwrEmergencyCategoryOption { EmergencyCategoryId = 1, EmergencyCategoryDesc = "Category X" },
            new EwrEmergencyCategoryOption { EmergencyCategoryId = 2, EmergencyCategoryDesc = "Category Y" }
        }.AsQueryable();

            var users = new List<UserOption>
        {
            new UserOption { UserId = 1, FullName = "User A" },
            new UserOption { UserId = 2, FullName = "User B" }
        }.AsQueryable();

            var swoStatuses = new List<SwoStatusOption>
        {
            new SwoStatusOption { StatusId = 1, StatusDesc = "Pending" },
            new SwoStatusOption { StatusId = 2, StatusDesc = "Approved" }
        }.AsQueryable();

            _ewrRepositoryMock.Setup(repo => repo.GetAllEwrStatuses())
                .Returns(ewrStatuses);

            _ewrRepositoryMock.Setup(repo => repo.GetAllEwrEmergencyTypes())
                .Returns(ewrEmergencyTypes);

            _ewrRepositoryMock.Setup(repo => repo.GetAllEwrEmergencyCauses())
                .Returns(ewrEmergencyCauses);

            _ewrRepositoryMock.Setup(repo => repo.GetAllEwrEmergencyCategories())
               .Returns(ewrEmergencyCategories);

            _userRepositoryMock.Setup(repo => repo.GetAllUsersNames())
                .Returns(users);

            _swoRepositoryMock.Setup(repo => repo.GetAllSwoStatuses())
                .Returns(swoStatuses);

            // Act
            BaseResponse<EwrIndexFiltersInfoResponseDto> result = await _ewrManager.GetIndexFiltersInfo();

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(2, result.Data.EwrStatuses.Count);
            Assert.AreEqual("Open", result.Data.EwrStatuses[0].StatusDesc);

            Assert.AreEqual(2, result.Data.EwrEmergencyTypes.Count);
            Assert.AreEqual("Type A", result.Data.EwrEmergencyTypes[0].EmergencyTypeDesc);

            Assert.AreEqual(2, result.Data.EwrEmergencyCauses.Count);
            Assert.AreEqual("Cause X", result.Data.EwrEmergencyCauses[0].EmergencyCauseDesc);

            Assert.AreEqual(2, result.Data.EwrEmergencyCategories.Count);
            Assert.AreEqual("Category X", result.Data.EwrEmergencyCategories[0].EmergencyCategoryDesc);

            Assert.AreEqual(2, result.Data.Users.Count);
            Assert.AreEqual("User A", result.Data.Users[0].FullName);

            Assert.AreEqual(2, result.Data.SwoStatuses.Count);
            Assert.AreEqual("Pending", result.Data.SwoStatuses[0].StatusDesc);

            Assert.AreEqual("EWR_INDEX_FILTERS_INFO_RETRIEVED", result.Message);
        }

        [Test]
        public async Task CloseEwr_ShouldReturnSuccess_WhenEwrExists()
        {
            // Arrange
            int ewrId = 1;
            EwrCloseDto ewrCloseDto = new EwrCloseDto
            {
                ReasonForClose = "Duplicate",
                CancelledDate = DateTime.UtcNow,
                CancelledBy = 2
            };

            EwrApplication existingEwrApplication = new EwrApplication
            {
                EwrApplicationId = ewrId,
                StatusId = 2, // Some other status
                ReasonForClose = null,
                CancelledDate = null,
                CancelledBy = null
            };

            _ewrRepositoryMock.Setup(repo => repo.GetById(ewrId))
                              .ReturnsAsync(existingEwrApplication);

            // Act
            BaseResponse<EwrCloseDto> result = await _ewrManager.CloseEwr(ewrId, ewrCloseDto);

            // Assert
            Assert.True(result.Success);
            Assert.AreEqual("EWR_CLOSED_SUCCESSFULLY", result.Message);
            Assert.AreEqual(ewrCloseDto, result.Data);

            _ewrRepositoryMock.Verify(repo => repo.UpdateEwrApplication(It.Is<EwrApplication>(
                app => app.ReasonForClose == ewrCloseDto.ReasonForClose &&
                       app.CancelledDate == ewrCloseDto.CancelledDate &&
                       app.CancelledBy == ewrCloseDto.CancelledBy &&
                       app.StatusId == 1 // Cancelled Status Id
            )), Times.Once);
        }
    }


    // Custom AsyncEnumerable class to mock IQueryable for async
    public class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }
        public TestAsyncEnumerable(Expression expression) : base(expression) { }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }
    }
}
