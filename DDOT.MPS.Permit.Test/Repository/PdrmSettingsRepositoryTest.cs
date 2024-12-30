using AutoMapper;
using DDOT.MPS.Permit.DataAccess.DBContexts;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.DataAccess.Repositories.PdrmSettingsRepository;
using DDOT.MPS.Permit.Model.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDOT.MPS.Permit.Test.Repositories
{
    [TestFixture]
    public class PdrmSettingsRepositoryTest
    {
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<PdrmSettingsRepository>> _loggerMock;
        private DbContextOptions<MpsDbContext> _dbContextOptions;
        private MpsDbContext _dbContext;
        private PdrmSettingsRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<PdrmSettingsRepository>>();
            _dbContextOptions = new DbContextOptionsBuilder<MpsDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new MpsDbContext(_dbContextOptions);
            _repository = new PdrmSettingsRepository(_dbContext, _mapperMock.Object, _loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
        
        [Test]
        public async Task CreateOrUpdateCalendarDatesSelectionData_ShouldHandleEmptyScheduledDays()
        {
            CalendarDatesSelectionReqDto calendarDatesSelectionReq = new CalendarDatesSelectionReqDto
            {
                MeetingTypeId = 1,
                ScheduledDays = new List<KeyValuePair<string, bool>>()
            };

            CalendarDatesSelectionReqDto result = await _repository.CreateOrUpdateCalendarDatesSelectionData(calendarDatesSelectionReq);

            Assert.IsNull(result);
        }

        [Test]
        public void CreateOrUpdateCalendarDatesSelectionData_ShouldThrowExceptionForInvalidDay()
        {
            CalendarDatesSelectionReqDto calendarDatesSelectionReq = new CalendarDatesSelectionReqDto
            {
                MeetingTypeId = 1,
                ScheduledDays = new List<KeyValuePair<string, bool>>
                {
                    new KeyValuePair<string, bool>("Funday", true)
                }
            };

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
                await _repository.CreateOrUpdateCalendarDatesSelectionData(calendarDatesSelectionReq));
            Assert.That(ex.Message, Is.EqualTo("Invalid day of the week: Funday"));
        }

        [Test]
        public async Task CreateOrUpdateCalendarDatesSelectionData_ShouldUpdateExistingDays()
        {
            _dbContext.PdrmCalendarDays.Add(new PdrmCalendarDay { CalendarDay = "Monday", IsActive = false, MeetingTypeId = 1 });
            _dbContext.PdrmCalendarDays.Add(new PdrmCalendarDay { CalendarDay = "Tuesday", IsActive = true, MeetingTypeId = 1 });
            await _dbContext.SaveChangesAsync();

            CalendarDatesSelectionReqDto calendarDatesSelectionReq = new CalendarDatesSelectionReqDto
            {
                MeetingTypeId = 1,
                ScheduledDays = new List<KeyValuePair<string, bool>>
                {
                    new KeyValuePair<string, bool>("Monday", true),
                    new KeyValuePair<string, bool>("Tuesday", false)
                }
            };

            CalendarDatesSelectionReqDto result = await _repository.CreateOrUpdateCalendarDatesSelectionData(calendarDatesSelectionReq);

            List<PdrmCalendarDay> calendarDays = _dbContext.PdrmCalendarDays.ToList();
            Assert.That(calendarDays.Count, Is.EqualTo(2));
            Assert.That(calendarDays[0].CalendarDay, Is.EqualTo("Monday"));
            Assert.That(calendarDays[0].IsActive, Is.True);
            Assert.That(calendarDays[1].CalendarDay, Is.EqualTo("Tuesday"));
            Assert.That(calendarDays[1].IsActive, Is.False);
            Assert.That(result, Is.EqualTo(calendarDatesSelectionReq));
        }

    }
}
