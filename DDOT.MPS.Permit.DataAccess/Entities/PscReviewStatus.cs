using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("psc_review_status")]
[Index("PscReviewStatusId", Name = "IX_psc_review_status_id")]
[Index("PscReviewStatusCode", Name = "UK_psc_review_status_code", IsUnique = true)]
public partial class PscReviewStatus
{
    [Key]
    [Column("psc_review_status_id")]
    public int PscReviewStatusId { get; set; }

    [Column("psc_review_status_code")]
    [StringLength(50)]
    [Unicode(false)]
    public string PscReviewStatusCode { get; set; } = null!;

    [Column("psc_review_status")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PscReviewStatus1 { get; set; }

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

    [InverseProperty("PscReviewStatus")]
    public virtual ICollection<CpApplication> CpApplications { get; set; } = new List<CpApplication>();
}
