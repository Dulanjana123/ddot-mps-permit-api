using AutoMapper;
using DDOT.MPS.Permit.Api.Managers.InspectionManagement;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.DataAccess.Repositories.ApplicationTypeRepository;
using DDOT.MPS.Permit.DataAccess.Repositories.InspectionRepository;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Request.Generic;
using DDOT.MPS.Permit.Model.Response;
using Moq;
using NUnit.Framework;
using Test.Helpers;
using static DDOT.MPS.Permit.Core.Exceptions.UserDefinedException;


namespace DDOT.MPS.Permit.Test.Managers
{
    [TestFixture]
    public class InspectionManagerTest
    {
        private Mock<IInspectionRepository> _inspectionRepository;
        private Mock<IMpsApplicationTypeRepository> _applicationTypeRepository;
        private Mock<IMapper> _mapper;
        private IInspectionManager _inspectionManager;


        [SetUp]
        public void SetUp()
        {
            _inspectionRepository = new Mock<IInspectionRepository>();
            _applicationTypeRepository = new Mock<IMpsApplicationTypeRepository>();
            _mapper = new Mock<IMapper>();
            _inspectionManager = new InspectionManager(_inspectionRepository.Object, _applicationTypeRepository.Object, _mapper.Object);
        }

        [Test]
        public async Task CreateInspection_Successful_ReturnsInspectionResponse()
        {
            InspectionDto inspectionDto = new InspectionDto { ApplicationTypeCode = "EWR", ApplicationId = 7, InspectedBy = 1, InternalNotes = "test_note" };
            ApplicationType applicationType = new ApplicationType { ApplicationTypeId = 7 };
            InspDetail inspectionDetail = new InspDetail();
            InspectionResponseDto inspectionResponseDto = new InspectionResponseDto();

            _applicationTypeRepository.Setup(x => x.GetApplicationTypeByTypeCode(inspectionDto.ApplicationTypeCode))
                                      .ReturnsAsync(applicationType);
            _mapper.Setup(m => m.Map<InspectionResponseDto>(It.IsAny<InspDetail>())).Returns(inspectionResponseDto);

            BaseResponse<InspectionResponseDto> result = await _inspectionManager.CreateInspection(inspectionDto);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(inspectionResponseDto, result.Data);
            Assert.AreEqual("INSPECTION_CREATED_SUCCESSFULLY", result.Message);
        }

        [Test]
        public async Task CreateInspection_ApplicationCodeNotFound_ReturnsFailureResponse()
        {
            InspectionDto inspectionDto = new InspectionDto { ApplicationTypeCode = "EWR" };

            _applicationTypeRepository.Setup(x => x.GetApplicationTypeByTypeCode(inspectionDto.ApplicationTypeCode))
                                      .ReturnsAsync((ApplicationType)null);

            BaseResponse<InspectionResponseDto> result = await _inspectionManager.CreateInspection(inspectionDto);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("APPLICATION_TYPE_CODE_NOT_FOUND", result.Message);
        }

        [Test]
        public void CreateInnspection_ExceptionThrown_RethrowsException()
        {
            InspectionDto inspectionDto = new InspectionDto { ApplicationTypeCode = "EWR" };

            _applicationTypeRepository.Setup(x => x.GetApplicationTypeByTypeCode(inspectionDto.ApplicationTypeCode))
                                      .Throws(new Exception("Database error"));

            Assert.ThrowsAsync<Exception>(() => _inspectionManager.CreateInspection(inspectionDto));
        }

        [Test]
        public async Task GetInspectionById_InspectionExists_ReturnsInspectionResponse()
        {
            int inspectionId = 9;
            //InspDetail inspDetail = new InspDetail();
            InspectionResponseDto inspectionResponseDto = new InspectionResponseDto();

            _inspectionRepository.Setup(x => x.GetInspectionById(inspectionId))
                             .ReturnsAsync(inspectionResponseDto);
            _mapper.Setup(m => m.Map<InspectionResponseDto>(inspectionResponseDto))
                   .Returns(inspectionResponseDto);

            BaseResponse<InspectionResponseDto> result = await _inspectionManager.GetInspectionById(inspectionId);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(inspectionResponseDto, result.Data);
            Assert.AreEqual("INSPECTION_DETAILS_RETRIEVED_SUCCESSFULLY", result.Message);
        }

        [Test]
        public void GetInspectionById_InspectionNotFound_ThrowsNotFoundException()
        {
            int inspectionId = 1;

            _inspectionRepository.Setup(x => x.GetInspectionById(inspectionId))
                             .ReturnsAsync((InspectionResponseDto)null);

            Assert.ThrowsAsync<UDNotFoundException>(() => _inspectionManager.GetInspectionById(inspectionId));
        }


        [Test]
        public async Task GetPaginatedList_ValidRequest_ReturnsPaginatedResponse()
        {
            InspectionPaginatedRequest request = new InspectionPaginatedRequest
            {
                PagingAndSortingInfo = new PagingAndSortingInfo
                {
                    Paging = new PagingInfo
                    {
                        PageSize = 10,
                        PageNo = 1
                    }
                }
            };

            IQueryable<InspDetail> inspections = new List<InspDetail> { new InspDetail(), new InspDetail() }.AsQueryable();
            List<InspectionResponseDto> inspectionResponseDtos = new List<InspectionResponseDto> { new InspectionResponseDto(), new InspectionResponseDto() };

            TestAsyncEnumerable<InspectionResponseDto> mockInspectionQueryable = new TestAsyncEnumerable<InspectionResponseDto>(inspectionResponseDtos);

            _inspectionRepository.Setup(x => x.GetAll(It.IsAny<InspectionPaginatedRequest>()))
                             .Returns(mockInspectionQueryable);

            _inspectionRepository.Setup(x => x.GetRowCount(It.IsAny<InspectionPaginatedRequest>()))
                             .Returns(inspections.Count());

            _mapper.Setup(m => m.Map<InspectionResponseDto>(It.IsAny<InspDetail>()))
                   .Returns(new InspectionResponseDto());

            BaseResponse<Result<InspectionResponseDto>> result = await _inspectionManager.GetPaginatedList(request);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(inspectionResponseDtos.Count, result.Data.Entities.Length);
            Assert.AreEqual(inspections.Count(), result.Data.Pagination.Length);
            Assert.AreEqual(request.PagingAndSortingInfo.Paging.PageSize, result.Data.Pagination.PageSize);
        }

        [Test]
        public async Task UpdateInspection_InspectionExists_ReturnsUpdatedInspectionResponse()
        {
            // Arrange
            int inspectionId = 9;
            InspectionDto inspectionDto = new InspectionDto
            {
                InspectedBy = 2,
                InspectionDate = DateTime.UtcNow,
                MinutesSpent = 30,
                InternalNotes = "Updated internal notes",
                ExternalNotes = "Updated external notes"
            };

            InspDetail existingInspection = new InspDetail();
            InspDetail updatedInspection = new InspDetail
            {
                InspectedBy = inspectionDto.InspectedBy,
                InspectionDate = inspectionDto.InspectionDate,
                MinutesSpent = inspectionDto.MinutesSpent,
                InternalNotes = inspectionDto.InternalNotes,
                ExternalNotes = inspectionDto.ExternalNotes,
                ModifiedDate = DateTime.UtcNow
            };
            InspectionResponseDto inspectionResponseDto = new InspectionResponseDto();

            _inspectionRepository.Setup(x => x.GetById(inspectionId)).ReturnsAsync(existingInspection);
            _inspectionRepository.Setup(x => x.UpdateInspection(existingInspection)).ReturnsAsync(updatedInspection);
            _mapper.Setup(m => m.Map<InspectionResponseDto>(updatedInspection)).Returns(inspectionResponseDto);

            // Act
            BaseResponse<InspectionResponseDto> result = await _inspectionManager.UpdateInspection(inspectionId, inspectionDto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(inspectionResponseDto, result.Data);
            Assert.AreEqual("INSPECTION_UPDATED_SUCCESSFULLY", result.Message);
        }

        [Test]
        public void UpdateInspection_InspectionNotFound_ThrowsNotFoundException()
        {
            // Arrange
            int inspectionId = 1;
            InspectionDto inspectionDto = new InspectionDto();

            _inspectionRepository.Setup(x => x.GetById(inspectionId)).ReturnsAsync((InspDetail)null);

            // Act & Assert
            Assert.ThrowsAsync<UDNotFoundException>(() => _inspectionManager.UpdateInspection(inspectionId, inspectionDto));
        }

    }
}
