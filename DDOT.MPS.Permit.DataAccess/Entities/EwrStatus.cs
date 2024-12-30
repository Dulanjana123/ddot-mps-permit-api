using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("ewr_status")]
[Index("StatusId", Name = "IX_status_id")]
[Index("StatusCode", Name = "UK_status_code", IsUnique = true)]
public partial class EwrStatus
{
    [Key]
    [Column("status_id")]
    public int StatusId { get; set; }

    [Column("status_code")]
    [StringLength(50)]
    [Unicode(false)]
    public string StatusCode { get; set; } = null!;

    [Column("status_desc")]
    [StringLength(100)]
    [Unicode(false)]
    public string? StatusDesc { get; set; }

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

    [InverseProperty("Status")]
    public virtual ICollection<EwrApplication> EwrApplications { get; set; } = new List<EwrApplication>();
}
