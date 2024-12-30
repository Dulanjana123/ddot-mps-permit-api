using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("pdrm_application_conversations")]
[Index("PdrmAppConversationId", Name = "IX_pdrm_app_conversation_id")]
public partial class PdrmApplicationConversation
{
    [Key]
    [Column("pdrm_app_conversation_id")]
    public int PdrmAppConversationId { get; set; }

    [Column("pdrm_application_id")]
    public int PdrmApplicationId { get; set; }

    [Column("message_text")]
    public string? MessageText { get; set; }

    [Column("messaged_by")]
    public int? MessagedBy { get; set; }

    [Column("messaged_date", TypeName = "datetime")]
    public DateTime? MessagedDate { get; set; }

    [Column("is_public_user")]
    public bool? IsPublicUser { get; set; }

    [Column("is_dept_user")]
    public bool? IsDeptUser { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("deleted_date", TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [Column("deleted_by")]
    public int? DeletedBy { get; set; }

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

    [Column("legacy_id_for_migration")]
    public int? LegacyIdForMigration { get; set; }

    [Column("is_migrated_from_tops")]
    public bool IsMigratedFromTops { get; set; }

    [ForeignKey("MessagedBy")]
    [InverseProperty("PdrmApplicationConversations")]
    public virtual User? MessagedByNavigation { get; set; }

    [ForeignKey("PdrmApplicationId")]
    [InverseProperty("PdrmApplicationConversations")]
    public virtual PdrmApplication PdrmApplication { get; set; } = null!;
}
