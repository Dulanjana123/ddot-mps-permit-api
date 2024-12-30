using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("location_category")]
public partial class LocationCategory
{
    [Key]
    [Column("location_category_id")]
    public int LocationCategoryId { get; set; }

    [Column("location_category_code")]
    [StringLength(25)]
    [Unicode(false)]
    public string LocationCategoryCode { get; set; } = null!;

    [Column("location_category_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? LocationCategoryName { get; set; }

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

    [InverseProperty("LocationCategory")]
    public virtual ICollection<EwrLocation> EwrLocations { get; set; } = new List<EwrLocation>();

    [InverseProperty("LocationCategory")]
    public virtual ICollection<ProjectLocation> ProjectLocations { get; set; } = new List<ProjectLocation>();

    [InverseProperty("LocationCategory")]
    public virtual ICollection<SwoLocation> SwoLocations { get; set; } = new List<SwoLocation>();
}
