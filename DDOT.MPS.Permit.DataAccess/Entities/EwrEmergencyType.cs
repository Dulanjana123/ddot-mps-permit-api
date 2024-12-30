using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("ewr_emergency_type")]
[Index("EmergencyTypeId", Name = "IX_emergency_type_id")]
[Index("EmergencyTypeCode", Name = "UK_emergency_type_code", IsUnique = true)]
public partial class EwrEmergencyType
{
    [Key]
    [Column("emergency_type_id")]
    public int EmergencyTypeId { get; set; }

    [Column("emergency_type_code")]
    [StringLength(50)]
    [Unicode(false)]
    public string EmergencyTypeCode { get; set; } = null!;

    [Column("emergency_type_desc")]
    [StringLength(100)]
    [Unicode(false)]
    public string? EmergencyTypeDesc { get; set; }

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

    [Column("is_migrated_from_tops")]
    public bool IsMigratedFromTops { get; set; }

    [InverseProperty("EmergencyType")]
    public virtual ICollection<EwrApplication> EwrApplications { get; set; } = new List<EwrApplication>();

    [InverseProperty("EmergencyType")]
    public virtual ICollection<EwrEmergencyTypeAgency> EwrEmergencyTypeAgencies { get; set; } = new List<EwrEmergencyTypeAgency>();

    [InverseProperty("EmergencyType")]
    public virtual ICollection<EwrEmergencyTypeCause> EwrEmergencyTypeCauses { get; set; } = new List<EwrEmergencyTypeCause>();
}
