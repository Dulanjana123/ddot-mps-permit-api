using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("application_type")]
[Index("ApplicationTypeId", Name = "IX_application_type_id")]
[Index("ApplicationTypeCode", Name = "UK_application_type_code", IsUnique = true)]
public partial class ApplicationType
{
    [Key]
    [Column("application_type_id")]
    public int ApplicationTypeId { get; set; }

    [Column("application_type_code")]
    [StringLength(5)]
    [Unicode(false)]
    public string ApplicationTypeCode { get; set; } = null!;

    [Column("application_type_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ApplicationTypeName { get; set; }

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

    [InverseProperty("AssociatedApplicationType")]
    public virtual ICollection<CpApplicationAssociation> CpApplicationAssociations { get; set; } = new List<CpApplicationAssociation>();

    [InverseProperty("ApplicationType")]
    public virtual ICollection<InspDetail> InspDetails { get; set; } = new List<InspDetail>();

    [InverseProperty("ApplicationType")]
    public virtual ICollection<InspDocument> InspDocuments { get; set; } = new List<InspDocument>();

    [InverseProperty("ApplicationType")]
    public virtual ICollection<InspStatus> InspStatuses { get; set; } = new List<InspStatus>();

    [InverseProperty("PermitApplicationType")]
    public virtual ICollection<SwoPermit> SwoPermits { get; set; } = new List<SwoPermit>();
}
