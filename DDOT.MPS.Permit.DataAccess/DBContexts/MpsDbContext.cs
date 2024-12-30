using DDOT.MPS.Permit.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.DBContexts;

public partial class MpsDbContext : DbContext
{
    public MpsDbContext(DbContextOptions<MpsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AddressType> AddressTypes { get; set; }
    public virtual DbSet<Agency> Agencies { get; set; }

    public virtual DbSet<AgencyCategory> AgencyCategories { get; set; }

    public virtual DbSet<ApplicationType> ApplicationTypes { get; set; }

    public virtual DbSet<CpApplication> CpApplications { get; set; }

    public virtual DbSet<CpApplicationAssociation> CpApplicationAssociations { get; set; }

    public virtual DbSet<CpAssociationType> CpAssociationTypes { get; set; }

    public virtual DbSet<CpStatus> CpStatuses { get; set; }

    public virtual DbSet<EwrEmergencyCause> EwrEmergencyCauses { get; set; }

    public virtual DbSet<EwrEmergencyType> EwrEmergencyTypes { get; set; }

    public virtual DbSet<EwrEmergencyTypeAgency> EwrEmergencyTypeAgencies { get; set; }

    public virtual DbSet<EwrEmergencyCategory> EwrEmergencyCategories { get; set; }
    public virtual DbSet<EwrEmergencyTypeCause> EwrEmergencyTypeCauses { get; set; }

    public virtual DbSet<EwrLocation> EwrLocations { get; set; }

    public virtual DbSet<EwrApplication> EwrApplications { get; set; }

    public virtual DbSet<EwrApplicationLocation> EwrApplicationLocations { get; set; }

    public virtual DbSet<EwrStatus> EwrStatuses { get; set; }

    public virtual DbSet<EwrTcp> EwrTcps { get; set; }

    public virtual DbSet<InspDetail> InspDetails { get; set; }

    public virtual DbSet<InspDocument> InspDocuments { get; set; }

    public virtual DbSet<InspStatus> InspStatuses { get; set; }

    public virtual DbSet<MeetingType> MeetingTypes { get; set; }

    public virtual DbSet<NoteType> NoteTypes { get; set; }

    public virtual DbSet<PdrmApplication> PdrmApplications { get; set; }

    public virtual DbSet<PdrmApplicationAttendee> PdrmApplicationAttendees { get; set; }

    public virtual DbSet<PdrmApplicationCaseType> PdrmApplicationCaseTypes { get; set; }

    public virtual DbSet<PdrmApplicationConversation> PdrmApplicationConversations { get; set; }

    public virtual DbSet<PdrmApplicationMinute> PdrmApplicationMinutes { get; set; }

    public virtual DbSet<PdrmApplicationReviewer> PdrmApplicationReviewers { get; set; }

    public virtual DbSet<PdrmApplicationWorkType> PdrmApplicationWorkTypes { get; set; }

    public virtual DbSet<PdrmCalendarDay> PdrmCalendarDays { get; set; }

    public virtual DbSet<PdrmCalendarExclusion> PdrmCalendarExclusions { get; set; }

    public virtual DbSet<PdrmCalendarTimeSlot> PdrmCalendarTimeSlots { get; set; }

    public virtual DbSet<PdrmCaseType> PdrmCaseTypes { get; set; }

    public virtual DbSet<PdrmDocument> PdrmDocuments { get; set; }

    public virtual DbSet<PdrmLocation> PdrmLocations { get; set; }

    public virtual DbSet<PdrmNote> PdrmNotes { get; set; }

    public virtual DbSet<PdrmReviewerTeam> PdrmReviewerTeams { get; set; }

    public virtual DbSet<PdrmReviewerTeamMember> PdrmReviewerTeamMembers { get; set; }

    public virtual DbSet<PdrmSchedulingLog> PdrmSchedulingLogs { get; set; }

    public virtual DbSet<PdrmStatus> PdrmStatuses { get; set; }

    public virtual DbSet<PdrmWorkTypeDetail> PdrmWorkTypeDetails { get; set; }

    public virtual DbSet<PdrmWorkTypeMaster> PdrmWorkTypeMasters { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectCoreTeam> ProjectCoreTeams { get; set; }

    public virtual DbSet<ProjectSupportTeam> ProjectSupportTeams { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserMergeEmailLog> UserMergeEmailLogs { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<SwoApplication> SwoApplications { get; set; }

    public virtual DbSet<SwoDocument> SwoDocuments { get; set; }

    public virtual DbSet<SwoLocation> SwoLocations { get; set; }

    public virtual DbSet<SwoNote> SwoNotes { get; set; }

    public virtual DbSet<SwoPermit> SwoPermits { get; set; }

    public virtual DbSet<SwoStatus> SwoStatuses { get; set; }

    public virtual DbSet<SwoType> SwoTypes { get; set; }

    public virtual DbSet<SwoViolation> SwoViolations { get; set; }

    public virtual DbSet<SwoViolationType> SwoViolationTypes { get; set; }

    public virtual DbSet<ProjectLocation> ProjectLocations { get; set; }

    public virtual DbSet<ProjectApplicantTeam> ProjectApplicantTeams { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    public virtual DbSet<Anc> Ancs { get; set; }

    public virtual DbSet<ProjectLocationWardAnc> ProjectLocationWardAncs { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}
