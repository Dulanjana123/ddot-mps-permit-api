using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("ewr_application_location")]
[Index("EwrApplicationLocationId", Name = "IX_ewr_application_location_id")]
public partial class EwrApplicationLocation
{
    [Key]
    [Column("ewr_application_location_id")]
    public int EwrApplicationLocationId { get; set; }

    [Column("ewr_application_id")]
    public int EwrApplicationId { get; set; }

    [Column("ewr_location_id")]
    public int EwrLocationId { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

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

    [ForeignKey("EwrApplicationId")]
    [InverseProperty("EwrApplicationLocations")]
    public virtual EwrApplication EwrApplication { get; set; } = null!;

    [ForeignKey("EwrLocationId")]
    [InverseProperty("EwrApplicationLocations")]
    public virtual EwrLocation EwrLocation { get; set; } = null!;
}
