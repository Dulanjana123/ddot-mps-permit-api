using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("location_type")]
[Index("LocationTypeId", Name = "IX_location_type_id")]
[Index("LocationTypeCode", Name = "UK_location_type_code", IsUnique = true)]
public partial class LocationType
{
    [Key]
    [Column("location_type_id")]
    public int LocationTypeId { get; set; }

    [Column("location_type_code")]
    [StringLength(25)]
    [Unicode(false)]
    public string LocationTypeCode { get; set; } = null!;

    [Column("location_type_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? LocationTypeName { get; set; }

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

    [InverseProperty("LocationType")]
    public virtual ICollection<EwrLocation> EwrLocations { get; set; } = new List<EwrLocation>();

    [InverseProperty("LocationType")]
    public virtual ICollection<ProjectLocation> ProjectLocations { get; set; } = new List<ProjectLocation>();

    [InverseProperty("LocationType")]
    public virtual ICollection<SwoLocation> SwoLocations { get; set; } = new List<SwoLocation>();
}
