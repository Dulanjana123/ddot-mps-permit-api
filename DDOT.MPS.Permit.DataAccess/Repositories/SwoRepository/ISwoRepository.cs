using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Response;

namespace DDOT.MPS.Permit.DataAccess.Repositories.SwoRepository
{
    public interface ISwoRepository
    {
        Task<SwoApplication> CreateSwo(SwoApplication swoApplication);

        Task<SwoViolation> CreateSwoViolation(SwoViolation swoViolation);

        Task<SwoNote> CreateSwoNote(SwoNote swoNote);

        Task<SwoApplication> GetById(int id);

        Task<SwoViolation> GetViolationBySwoApplicationId(int swoApplicationId);

        Task<SwoNote> GetSwoNoteBySwoApplicationId(int swoApplicationId);

        Task<NoteType> GetNoteTypeByNoteCode(string noteCode);

        Task<SwoApplication> GetSwoApplicationBySwoNumber(string swoNumber);

        IQueryable<ViolationTypeOption> GetViolationTypes();

        IQueryable<SwoStatusOption> GetAllSwoStatuses();

        Task<SwoApplication> UpdateSwo(SwoApplication swoApplication, SwoViolation swoViolation, SwoNote swoNote);
    }
}
