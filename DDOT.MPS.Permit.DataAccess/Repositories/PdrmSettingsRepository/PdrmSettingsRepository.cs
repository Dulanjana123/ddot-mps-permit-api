using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DDOT.MPS.Permit.DataAccess.DBContexts;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DDOT.MPS.Permit.DataAccess.Repositories.PdrmSettingsRepository
{
    public class PdrmSettingsRepository : IPdrmSettingsRepository
    {
        private readonly MpsDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<PdrmSettingsRepository> _logger;

        public PdrmSettingsRepository(MpsDbContext dbContext, IMapper mapper, ILogger<PdrmSettingsRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<PdrmMeetingConstraintsDto> GetMeetingsSettingsAsync(int meetingTypeId)
        {
            List<PdrmCalendarDay> calendarDays = await _dbContext.PdrmCalendarDays
                .Where(x => x.MeetingTypeId == meetingTypeId)
                .ToListAsync();
            List<MeetingType> meetingTypes = await _dbContext.MeetingTypes
                .ToListAsync();
            List<PdrmCalendarTimeSlot> calendarTimeSlots = await _dbContext.PdrmCalendarTimeSlots
                .Where(x => x.MeetingTypeId == meetingTypeId && x.IsActive && !x.IsDeleted)
                .ToListAsync();

            PdrmMeetingConstraintsDto pdrmMeetingConstraints = new PdrmMeetingConstraintsDto
            {
                CalendarDatesSelectionDto = new CalendarDatesSelectionDto
                {
                    MeetingTypes = meetingTypes.Select(x => new KeyValuePair<int, string>(x.MeetingTypeId, x.MeetingTypeName)).ToList(),
                    ScheduledDays = calendarDays.Select(x => new KeyValuePair<string, bool>(x.CalendarDay, x.IsActive)).ToList(),
                },
                TimeBlocksSelectionDto = new TimeBlocksSelectionDto
                {
                    TimeBlocks = calendarTimeSlots.Select(x => new TimeBlockDto
                    {
                        StartTime = x.FromTime,
                        EndTime = x.ToTime,
                    }).ToList()
                }
            };

            return pdrmMeetingConstraints;
        }

        public async Task<List<int>> GetMeetingTypeIds()
        {
            return await _dbContext.MeetingTypes.Select(x => x.MeetingTypeId).ToListAsync();
        }

        public async Task<CalendarDatesSelectionReqDto> CreateOrUpdateCalendarDatesSelectionData(CalendarDatesSelectionReqDto calendarDatesSelectionReq)
        {
            for (int i = 0; i < calendarDatesSelectionReq.ScheduledDays.Count; i++)
            {
                var day = calendarDatesSelectionReq.ScheduledDays[i];

                // Correct capitalization
                string correctedDay = char.ToUpper(day.Key[0]) + day.Key.Substring(1).ToLower();

                // Validate the day
                if (!Enum.TryParse(correctedDay, out DayOfWeek _))
                {
                    _logger.LogError("DDOT.MPS.Permit.Api.Managers.CreateOrUpdateCalendarDatesSelectionData | INVALID_DAY_OF_WEEK");
                    throw new ArgumentException($"Invalid day of the week: {day.Key}");
                }

                // Update the key if capitalization was corrected
                if (correctedDay != day.Key)
                {
                    calendarDatesSelectionReq.ScheduledDays[i] = new KeyValuePair<string, bool>(correctedDay, day.Value);
                }
            }

            foreach (KeyValuePair<string, bool> day in calendarDatesSelectionReq.ScheduledDays)
            {
                PdrmCalendarDay existingDay = await _dbContext.PdrmCalendarDays
                    .FirstOrDefaultAsync(x => x.CalendarDay == day.Key && x.MeetingTypeId == calendarDatesSelectionReq.MeetingTypeId);

                if (existingDay != null)
                {
                    existingDay.IsActive = day.Value;
                    existingDay.ModifiedDate = DateTime.Now;
                }
            }

            if (calendarDatesSelectionReq.ScheduledDays.Count > 0)
            {
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            return calendarDatesSelectionReq;
        }

        public async Task<TimeBlocksSelectionDtoReqDto> CreateOrUpdateAvailableTimeBlocks(
            TimeBlocksSelectionDtoReqDto calendarDatesSelectionDtoReq)
        {
            var existingTimeSlots = await _dbContext.PdrmCalendarTimeSlots
                .Where(x => x.MeetingTypeId == calendarDatesSelectionDtoReq.MeetingTypeId && x.IsActive && !x.IsDeleted)
                .ToListAsync();

            var passedTimeSlots = calendarDatesSelectionDtoReq.TimeBlocks
                .Select(x => new { x.StartTime, x.EndTime })
                .ToList();

            foreach (var timeBlock in calendarDatesSelectionDtoReq.TimeBlocks)
            {
                var existingTimeSlot = existingTimeSlots
                    .FirstOrDefault(x => x.FromTime == timeBlock.StartTime && x.ToTime == timeBlock.EndTime);

                if (existingTimeSlot != null)
                {
                    existingTimeSlot.ModifiedDate = DateTime.Now;
                    existingTimeSlot.IsDeleted = false;
                    existingTimeSlot.IsActive = true;
                    existingTimeSlot.TimeDurationMinutes = (int)(timeBlock.EndTime - timeBlock.StartTime).TotalMinutes;
                }
                else
                {
                    var newTimeSlot = new PdrmCalendarTimeSlot
                    {
                        MeetingTypeId = calendarDatesSelectionDtoReq.MeetingTypeId,
                        FromTime = timeBlock.StartTime,
                        ToTime = timeBlock.EndTime,
                        ModifiedDate = DateTime.Now,
                        IsDeleted = false,
                        IsActive = true,
                        TimeDurationMinutes = (int)(timeBlock.EndTime - timeBlock.StartTime).TotalMinutes
                    };
                    _dbContext.PdrmCalendarTimeSlots.Add(newTimeSlot);
                }
            }

            // Mark time slots as inactive and deleted if they are not present in the passed array
            foreach (var existingTimeSlot in existingTimeSlots)
            {
                if (!passedTimeSlots.Any(x => x.StartTime == existingTimeSlot.FromTime && x.EndTime == existingTimeSlot.ToTime))
                {
                    existingTimeSlot.IsActive = false;
                    existingTimeSlot.IsDeleted = true;
                    existingTimeSlot.ModifiedDate = DateTime.Now;
                }
            }

            await _dbContext.SaveChangesAsync();

            return calendarDatesSelectionDtoReq;
        }


    }
}
