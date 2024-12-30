using AutoMapper;
using DDOT.MPS.Permit.DataAccess.DBContexts;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Repositories.SwoRepository
{
    public class SwoRepository : ISwoRepository
    {

        private readonly MpsDbContext _dbContext;
        private readonly IMapper _mapper;

        public SwoRepository(MpsDbContext userDbContext, IMapper mapper)
        {
            _dbContext = userDbContext;
            _mapper = mapper;
        }

        public async Task<SwoApplication> GetById(int id)
        {
            SwoApplication swoApplication = await _dbContext.SwoApplications
                .FirstOrDefaultAsync(req => req.SwoApplicationId == id);
            return swoApplication;
        }

        public async Task<SwoApplication> GetSwoApplicationBySwoNumber(string swoNumber)
        {
            return await _dbContext.SwoApplications.Where(a => a.SwoNumber == swoNumber).FirstOrDefaultAsync();
        }

        public async Task<NoteType> GetNoteTypeByNoteCode(string noteCode)
        {
            return await _dbContext.NoteTypes.Where(a => a.NoteCode.Equals(noteCode)).FirstOrDefaultAsync();
        }

        public async Task<SwoApplication> CreateSwo(SwoApplication swoApplication)
        {

            _dbContext.Add<SwoApplication>(swoApplication);
            await _dbContext.SaveChangesAsync();
            return swoApplication;
        }

        public async Task<SwoViolation> CreateSwoViolation(SwoViolation swoViolation)
        {
            _dbContext.Add<SwoViolation>(swoViolation);
            await _dbContext.SaveChangesAsync();
            return swoViolation;
        }

        public async Task<SwoNote> CreateSwoNote(SwoNote swoNote)
        {
            _dbContext.Add<SwoNote>(swoNote);
            await _dbContext.SaveChangesAsync();
            return swoNote;
        }

        public IQueryable<ViolationTypeOption> GetViolationTypes()
        {
            IQueryable<ViolationTypeOption> swoViolationTypes = _dbContext.SwoViolationTypes
            .Select(res => new ViolationTypeOption
            {
                ViolationTypeId = res.SwoViolationTypeId,
                ViolationTypeDesc = res.SwoViolTypeDesc,
            }).AsQueryable();
            return swoViolationTypes;
        }

        public async Task<SwoViolation> GetViolationBySwoApplicationId(int swoApplicationId)
        {
            SwoViolation swoViolation = await _dbContext.SwoViolations
                .FirstOrDefaultAsync(req => req.SwoApplicationId == swoApplicationId);
            return swoViolation;
        }

        public async Task<SwoNote> GetSwoNoteBySwoApplicationId(int swoApplicationId)
        {
            SwoNote swoNote = await _dbContext.SwoNotes
                .FirstOrDefaultAsync(req => req.SwoApplicationId == swoApplicationId);
            return swoNote;
        }

        public async Task<SwoApplication> UpdateSwo(SwoApplication swoApplication, SwoViolation swoViolation, SwoNote swoNote)
        {
            _dbContext.Update<SwoApplication>(swoApplication);
            _dbContext.Update<SwoViolation>(swoViolation);
            _dbContext.Update<SwoNote>(swoNote);
            await _dbContext.SaveChangesAsync();
            return swoApplication;
        }

        public IQueryable<SwoStatusOption> GetAllSwoStatuses()
        {
            IQueryable<SwoStatusOption> swoStatuses = _dbContext.SwoStatuses
            .Select(res => new SwoStatusOption
            {
                StatusId = res.SwoStatusId,
                StatusDesc = res.SwoStatusDesc,
            }).AsQueryable();

            return swoStatuses;
        }
    }
}
