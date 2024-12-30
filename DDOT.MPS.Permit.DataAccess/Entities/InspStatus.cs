using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("insp_status")]
[Index("InspStatusId", Name = "IX_insp_status_id")]
[Index("InspStatusCode", Name = "UK_insp_status_code", IsUnique = true)]
public partial class InspStatus
{
    [Key]
    [Column("insp_status_id")]
    public int InspStatusId { get; set; }

    [Column("application_type_id")]
    public int ApplicationTypeId { get; set; }

    [Column("insp_status_code")]
    [StringLength(50)]
    [Unicode(false)]
    public string InspStatusCode { get; set; } = null!;

    [Column("insp_status_desc")]
    [StringLength(100)]
    [Unicode(false)]
    public string? InspStatusDesc { get; set; }

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

    [ForeignKey("ApplicationTypeId")]
    [InverseProperty("InspStatuses")]
    public virtual ApplicationType ApplicationType { get; set; } = null!;

    [InverseProperty("InspStatus")]
    public virtual ICollection<InspDetail> InspDetails { get; set; } = new List<InspDetail>();
}
