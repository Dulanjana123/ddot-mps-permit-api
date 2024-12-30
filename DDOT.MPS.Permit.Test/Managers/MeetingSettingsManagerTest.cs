using AutoMapper;
using DDOT.MPS.Permit.Api.Managers.PdrmSettingsManager;
using DDOT.MPS.Permit.Core.Enums;
using DDOT.MPS.Permit.DataAccess.Repositories.PdrmSettingsRepository;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DDOT.MPS.Permit.Test.Managers
{
    [TestFixture]
    public class MeetingSettingsManagerTest
    {
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<MeetingSettingsManager>> _loggerMock;
        private Mock<IPdrmSettingsRepository> _pdrmSettingsRepositoryMock;
        private MeetingSettingsManager _meetingSettingsManager;

        [SetUp]
        public void SetUp()
        {
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<MeetingSettingsManager>>();
            _pdrmSettingsRepositoryMock = new Mock<IPdrmSettingsRepository>();
            _meetingSettingsManager = new MeetingSettingsManager(_mapperMock.Object, _loggerMock.Object, _pdrmSettingsRepositoryMock.Object);
        }

        [Test]
        public async Task GetAll_ShouldReturnMeetingsSettingsDto()
        {
            PdrmMeetingConstraintsDto pdrmMeetingConstraintsDto = new PdrmMeetingConstraintsDto
            {
                CalendarDatesSelectionDto = new CalendarDatesSelectionDto(),
                TimeBlocksSelectionDto = new TimeBlocksSelectionDto()
            };
            _pdrmSettingsRepositoryMock.Setup(repo => repo.GetMeetingsSettingsAsync(1))
                .ReturnsAsync(pdrmMeetingConstraintsDto);

            _pdrmSettingsRepositoryMock.Setup(repo => repo.GetMeetingTypeIds())
                .ReturnsAsync(new List<int> { 1 });

            BaseResponse<PdrmMeetingConstraintsDto> result = await _meetingSettingsManager.GetAll(1);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.That(result.Message, Is.EqualTo("PDRM_SETTINGS_RETRIEVED_SUCCESSFULLY"));
            Assert.That(result.Data, Is.EqualTo(pdrmMeetingConstraintsDto));
        }

        [Test]
        public async Task CreateOrUpdateCalendarDatesSelectionData_ShouldReturnUpdatedCalendarDatesSelectionDto()
        {
            CalendarDatesSelectionReqDto calendarDatesSelectionReqDto = new CalendarDatesSelectionReqDto
            {
                MeetingTypeId = 1,
                ScheduledDays = new List<KeyValuePair<string, bool>>
                {
                    new("Monday", true),
                    new("Tuesday", false)
                }
            };
            CalendarDatesSelectionReqDto updatedCalendarDatesSelectionReqDto = new CalendarDatesSelectionReqDto
            {
                MeetingTypeId = 1,
                ScheduledDays = new List<KeyValuePair<string, bool>>
                {
                    new("Monday", true),
                    new("Tuesday", true)
                }
            };
            _pdrmSettingsRepositoryMock.Setup(repo => repo.CreateOrUpdateCalendarDatesSelectionData(calendarDatesSelectionReqDto))
                .ReturnsAsync(updatedCalendarDatesSelectionReqDto);

            BaseResponse<CalendarDatesSelectionReqDto> result = await _meetingSettingsManager.CreateOrUpdateCalendarDatesSelectionData(calendarDatesSelectionReqDto);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.That(result.Message, Is.EqualTo("PDRM_SETTINGS_UPDATED_SUCCESSFULLY"));
            Assert.That(result.Data, Is.EqualTo(updatedCalendarDatesSelectionReqDto));
        }
        
        [Test]
        public async Task CreateOrUpdateAvailableTimeBlocks_ShouldReturnStartTimeMustBeLessThanEndTime_WhenStartTimeIsGreaterOrEqualToEndTime()
        {
            TimeBlocksSelectionDtoReqDto timeBlocksSelectionDtoReqDto = new TimeBlocksSelectionDtoReqDto
            {
                MeetingTypeId = 1,
                Action = CommonActions.Add,
                TimeBlocks = new List<TimeBlockDto>
                {
                    new() { StartTime = new TimeOnly(9, 0), EndTime = new TimeOnly(8, 0) }
                }
            };

            BaseResponse<TimeBlocksSelectionDtoReqDto> result = await _meetingSettingsManager.CreateOrUpdateAvailableTimeBlocks(timeBlocksSelectionDtoReqDto);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.That(result.Message, Is.EqualTo("START_TIME_MUST_BE_LESS_THAN_END_TIME"));
            Assert.IsNull(result.Data);
        }
    }
}
