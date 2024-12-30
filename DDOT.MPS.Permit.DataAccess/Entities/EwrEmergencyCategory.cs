using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("ewr_emergency_category")]
[Index("EmergencyCategoryId", Name = "IX_emergency_category_id")]
public partial class EwrEmergencyCategory
{
    [Key]
    [Column("emergency_category_id")]
    public int EmergencyCategoryId { get; set; }

    [Column("emergency_category")]
    [StringLength(50)]
    [Unicode(false)]
    public string EmergencyCategory { get; set; } = null!;

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

    [InverseProperty("EmergencyCategory")]
    public virtual ICollection<EwrApplication> EwrApplications { get; set; } = new List<EwrApplication>();
}