using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("swo_application")]
[Index("SwoApplicationId", Name = "IX_swo_application_id")]
[Index("SwoNumber", Name = "UK_swo_number", IsUnique = true)]
public partial class SwoApplication
{
    [Key]
    [Column("swo_application_id")]
    public int SwoApplicationId { get; set; }

    [Column("swo_number")]
    [StringLength(20)]
    [Unicode(false)]
    public string SwoNumber { get; set; } = null!;

    [Column("swo_type_id")]
    public int SwoTypeId { get; set; }

    [Column("swo_status_id")]
    public int? SwoStatusId { get; set; }

    [Column("violated_contr_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ViolatedContrName { get; set; }

    [Column("violated_contr_reg_no")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ViolatedContrRegNo { get; set; }

    [Column("violated_contr_reg_addr")]
    [StringLength(500)]
    [Unicode(false)]
    public string? ViolatedContrRegAddr { get; set; }

    [Column("violated_owner_fname")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ViolatedOwnerFname { get; set; }

    [Column("violated_owner_lname")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ViolatedOwnerLname { get; set; }

    [Column("violation_comments")]
    [Unicode(false)]
    public string? ViolationComments { get; set; }

    [Column("issued_by")]
    public int? IssuedBy { get; set; }

    [Column("issued_date", TypeName = "datetime")]
    public DateTime? IssuedDate { get; set; }

    [Column("issued_time")]
    [StringLength(20)]
    [Unicode(false)]
    public string? IssuedTime { get; set; }

    [Column("work_site_foreman")]
    [StringLength(50)]
    [Unicode(false)]
    public string? WorkSiteForeman { get; set; }

    [Column("work_site_foreman_phone")]
    [StringLength(15)]
    [Unicode(false)]
    public string? WorkSiteForemanPhone { get; set; }

    [Column("work_site_conditions")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? WorkSiteConditions { get; set; }

    [Column("weather_conditions")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? WeatherConditions { get; set; }

    [Column("lifting_justification")]
    [StringLength(1000)]
    [Unicode(false)]
    public string? LiftingJustification { get; set; }

    [Column("lifted_by")]
    public int? LiftedBy { get; set; }

    [Column("lifted_date", TypeName = "datetime")]
    public DateTime? LiftedDate { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("sort_id")]
    public int? SortId { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("created_date", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("modified_by")]
    public int? ModifiedBy { get; set; }

    [Column("modified_date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("legacy_id_for_migration")]
    public int? LegacyIdForMigration { get; set; }

    [Column("is_migrated_from_tops")]
    public bool IsMigratedFromTops { get; set; }

    [InverseProperty("SwoApplication")]
    public virtual ICollection<EwrApplication> EwrApplications { get; set; } = new List<EwrApplication>();

    [ForeignKey("IssuedBy")]
    [InverseProperty("SwoApplicationIssuedByNavigations")]
    public virtual User? IssuedByNavigation { get; set; }

    [ForeignKey("LiftedBy")]
    [InverseProperty("SwoApplicationLiftedByNavigations")]
    public virtual User? LiftedByNavigation { get; set; }

    [InverseProperty("SwoApplication")]
    public virtual ICollection<SwoDocument> SwoDocuments { get; set; } = new List<SwoDocument>();

    [InverseProperty("SwoApplication")]
    public virtual ICollection<SwoLocation> SwoLocations { get; set; } = new List<SwoLocation>();

    [InverseProperty("SwoApplication")]
    public virtual ICollection<SwoNote> SwoNotes { get; set; } = new List<SwoNote>();

    [InverseProperty("SwoApplication")]
    public virtual ICollection<SwoPermit> SwoPermits { get; set; } = new List<SwoPermit>();

    [ForeignKey("SwoStatusId")]
    [InverseProperty("SwoApplications")]
    public virtual SwoStatus? SwoStatus { get; set; }

    [ForeignKey("SwoTypeId")]
    [InverseProperty("SwoApplications")]
    public virtual SwoType SwoType { get; set; } = null!;

    [InverseProperty("SwoApplication")]
    public virtual ICollection<SwoViolation> SwoViolations { get; set; } = new List<SwoViolation>();
}
