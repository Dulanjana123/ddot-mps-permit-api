using AutoMapper;
using DDOT.MPS.Permit.Api.Managers.SwoManagement;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.DataAccess.Repositories.SwoRepository;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Response;
using Moq;
using NUnit.Framework;
using static DDOT.MPS.Permit.Core.Exceptions.UserDefinedException;

namespace DDOT.MPS.Permit.Test.Managers
{
    [TestFixture]
    public class SwoManagerTest
    {
        private Mock<ISwoRepository> _swoRepository;
        private Mock<IMapper> _mapper;
        private SwoManager _swoManager;

        [SetUp]
        public void SetUp()
        {
            _swoRepository = new Mock<ISwoRepository>();
            _mapper = new Mock<IMapper>();
            _swoManager = new SwoManager(_swoRepository.Object, _mapper.Object);
        }

        [Test]
        public async Task CreateSwoViolation_SwoNumberAlreadyExists_ReturnsFailureResponse()
        {
            // Arrange
            SwoViolationDto swoViolationDto = new SwoViolationDto { SwoNumber = "SWO123", NoteCode = "NT01" };
            SwoApplication existingSwoApplication = new SwoApplication();

            _swoRepository.Setup(x => x.GetSwoApplicationBySwoNumber(swoViolationDto.SwoNumber))
                          .ReturnsAsync(existingSwoApplication);

            // Act
            BaseResponse<SwoResponseDto> result = await _swoManager.CreateSwoViolation(swoViolationDto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("SWO_NUMBER_ALREADY_EXCIST", result.Message);
        }

        [Test]
        public async Task CreateSwoViolation_NoteTypeUnavailable_ReturnsFailureResponse()
        {
            // Arrange
            SwoViolationDto swoViolationDto = new SwoViolationDto { SwoNumber = "SWO123", NoteCode = "NT01" };
            _swoRepository.Setup(x => x.GetSwoApplicationBySwoNumber(swoViolationDto.SwoNumber))
                          .ReturnsAsync((SwoApplication)null);
            _swoRepository.Setup(x => x.GetNoteTypeByNoteCode(swoViolationDto.NoteCode))
                          .ReturnsAsync((NoteType)null);

            // Act
            BaseResponse<SwoResponseDto> result = await _swoManager.CreateSwoViolation(swoViolationDto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("NOTE_TYPE_IS_UNAVAILABLE", result.Message);
        }

        [Test]
        public async Task CreateSwoViolation_Successful_ReturnsViolationAddedResponse()
        {
            // Arrange
            SwoViolationDto swoViolationDto = new SwoViolationDto { SwoNumber = "SWO123", NoteCode = "NT01" };
            SwoApplication createdSwoApplication = new SwoApplication { SwoApplicationId = 1 };
            NoteType noteType = new NoteType { NoteTypeId = 1 };
            SwoViolation swoViolation = new SwoViolation();
            SwoResponseDto swoResponseDto = new SwoResponseDto();

            
            _swoRepository.Setup(x => x.GetNoteTypeByNoteCode(swoViolationDto.NoteCode))
                          .ReturnsAsync(noteType);
            _swoRepository.Setup(x => x.CreateSwo(It.IsAny<SwoApplication>()))
                          .ReturnsAsync(createdSwoApplication);
            _mapper.Setup(m => m.Map<SwoResponseDto>(It.IsAny<SwoViolation>()))
                   .Returns(swoResponseDto);

            // Act
            BaseResponse<SwoResponseDto> result = await _swoManager.CreateSwoViolation(swoViolationDto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("SWO_SAVED_SUCCESSFULLY", result.Message);
        }

        [Test]
        public async Task GetViolationTypes_ReturnsViolationTypesResponse()
        {
            // Arrange
            List<ViolationTypeOption> violationTypesList = new List<ViolationTypeOption> { new ViolationTypeOption() };
            IQueryable<ViolationTypeOption> violationTypes = violationTypesList.AsQueryable();
            SwoViolationTypesResponseDto violationTypesResponseDto = new SwoViolationTypesResponseDto { ViolationTypes = violationTypesList };

            _swoRepository.Setup(x => x.GetViolationTypes()).Returns(violationTypes);

            // Act
            BaseResponse<SwoViolationTypesResponseDto> result = await _swoManager.GetViolationTypes();

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("VIOLATION_TYPES_RETRIEVED", result.Message);
            Assert.AreEqual(violationTypesResponseDto.ViolationTypes, result.Data.ViolationTypes);
        }

        [Test]
        public void UpdateSwoViolation_SwoApplicationNotFound_ThrowsNotFoundException()
        {
            // Arrange
            int id = 1;
            SwoViolationDto swoViolationDto = new SwoViolationDto();

            _swoRepository.Setup(x => x.GetById(id)).ReturnsAsync((SwoApplication)null);

            // Act & Assert
            UDNotFoundException exception = Assert.ThrowsAsync<UDNotFoundException>(() => _swoManager.UpdateSwoViolation(id, swoViolationDto));
            Assert.AreEqual("SWO_NOT_FOUND", exception.Message);
        }

        [Test]
        public async Task UpdateSwoViolation_Successful_ReturnsUpdatedResponse()
        {
            // Arrange
            int id = 1;
            SwoViolationDto swoViolationDto = new SwoViolationDto { SwoTypeId = 2, SwoStatusId = 3, InternalNotes = "Updated Notes" };
            SwoApplication existingSwoApplication = new SwoApplication { SwoApplicationId = id };
            SwoViolation existingViolation = new SwoViolation { SwoApplicationId = id };
            SwoNote existingNote = new SwoNote { SwoApplicationId = id };
            SwoResponseDto swoResponseDto = new SwoResponseDto();

            _swoRepository.Setup(x => x.GetById(id)).ReturnsAsync(existingSwoApplication);
            _swoRepository.Setup(x => x.GetViolationBySwoApplicationId(id)).ReturnsAsync(existingViolation);
            _swoRepository.Setup(x => x.GetSwoNoteBySwoApplicationId(id)).ReturnsAsync(existingNote);
            _swoRepository.Setup(x => x.UpdateSwo(existingSwoApplication, existingViolation, existingNote))
                          .ReturnsAsync(existingSwoApplication);
            _mapper.Setup(m => m.Map<SwoResponseDto>(existingSwoApplication)).Returns(swoResponseDto);

            // Act
            BaseResponse<SwoResponseDto> result = await _swoManager.UpdateSwoViolation(id, swoViolationDto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(swoResponseDto, result.Data);
            Assert.AreEqual("SWO_UPDATED_SUCCESSFULLY", result.Message);
        }

        [Test]
        public async Task UpdateSwoViolation_UpdatesFieldsCorrectly()
        {
            // Arrange
            int id = 1;
            SwoViolationDto swoViolationDto = new SwoViolationDto
            {
                SwoTypeId = 2,
                SwoStatusId = 3,
                ViolationComments = "New Violation Comments",
                InternalNotes = "Updated Internal Notes"
            };
            SwoApplication existingSwoApplication = new SwoApplication { SwoApplicationId = id };
            SwoViolation existingViolation = new SwoViolation { SwoApplicationId = id };
            SwoNote existingNote = new SwoNote { SwoApplicationId = id };

            _swoRepository.Setup(x => x.GetById(id)).ReturnsAsync(existingSwoApplication);
            _swoRepository.Setup(x => x.GetViolationBySwoApplicationId(id)).ReturnsAsync(existingViolation);
            _swoRepository.Setup(x => x.GetSwoNoteBySwoApplicationId(id)).ReturnsAsync(existingNote);
            _swoRepository.Setup(x => x.UpdateSwo(existingSwoApplication, existingViolation, existingNote))
                          .ReturnsAsync(existingSwoApplication);

            // Act
            await _swoManager.UpdateSwoViolation(id, swoViolationDto);

            // Assert
            Assert.AreEqual(swoViolationDto.SwoTypeId, existingSwoApplication.SwoTypeId);
            Assert.AreEqual(swoViolationDto.SwoStatusId, existingSwoApplication.SwoStatusId);
            Assert.AreEqual(swoViolationDto.ViolationComments, existingSwoApplication.ViolationComments);
            Assert.AreEqual(swoViolationDto.InternalNotes, existingNote.Notes);
        }

    }
}
