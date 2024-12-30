using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DDOT.MPS.Permit.DataAccess.Entities;

[Table("project_locations")]
[Index("ProjectLocationId", Name = "IX_project_location_id")]
public partial class ProjectLocation
{
    [Key]
    [Column("project_location_id")]
    public int ProjectLocationId { get; set; }

    [Column("project_id")]
    public int ProjectId { get; set; }

    [Column("location_type_id")]
    public int LocationTypeId { get; set; }

    [Column("location_category_id")]
    public int? LocationCategoryId { get; set; }

    [Column("is_added_via_pdrm")]
    public bool IsAddedViaPdrm { get; set; }

    [Column("street_classification")]
    [StringLength(50)]
    [Unicode(false)]
    public string? StreetClassification { get; set; }

    [Column("zone")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Zone { get; set; }

    [Column("is_verified")]
    public bool? IsVerified { get; set; }

    [Column("reference_id")]
    public int? ReferenceId { get; set; }

    [Column("address_id")]
    public int? AddressId { get; set; }

    [Column("street_seg_id")]
    public int? StreetSegId { get; set; }

    [Column("intersection1_id")]
    public int? Intersection1Id { get; set; }

    [Column("intersection2_id")]
    public int? Intersection2Id { get; set; }

    [Column("full_description")]
    [StringLength(100)]
    [Unicode(false)]
    public string? FullDescription { get; set; }

    [Column("multiple_address")]
    [StringLength(100)]
    [Unicode(false)]
    public string? MultipleAddress { get; set; }

    [Column("addr_num")]
    [StringLength(10)]
    [Unicode(false)]
    public string? AddrNum { get; set; }

    [Column("block_no")]
    [StringLength(100)]
    [Unicode(false)]
    public string? BlockNo { get; set; }

    [Column("st_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? StName { get; set; }

    [Column("st_name2")]
    [StringLength(100)]
    [Unicode(false)]
    public string? StName2 { get; set; }

    [Column("street_type")]
    [StringLength(20)]
    [Unicode(false)]
    public string? StreetType { get; set; }

    [Column("quadrant")]
    [StringLength(2)]
    [Unicode(false)]
    public string? Quadrant { get; set; }

    [Column("quadrant2")]
    [StringLength(2)]
    [Unicode(false)]
    public string? Quadrant2 { get; set; }

    [Column("city")]
    [StringLength(50)]
    [Unicode(false)]
    public string? City { get; set; }

    [Column("state")]
    [StringLength(50)]
    [Unicode(false)]
    public string? State { get; set; }

    [Column("zip_code")]
    [StringLength(10)]
    [Unicode(false)]
    public string? ZipCode { get; set; }

    [Column("x_coord")]
    [StringLength(50)]
    [Unicode(false)]
    public string? XCoord { get; set; }

    [Column("y_coord")]
    [StringLength(50)]
    [Unicode(false)]
    public string? YCoord { get; set; }

    [Column("mar_x_coord")]
    [StringLength(50)]
    [Unicode(false)]
    public string? MarXCoord { get; set; }

    [Column("mar_y_coord")]
    [StringLength(50)]
    [Unicode(false)]
    public string? MarYCoord { get; set; }

    [Column("ssl")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Ssl { get; set; }

    [Column("a_lot")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ALot { get; set; }

    [Column("a_square")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ASquare { get; set; }

    [Column("anc")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Anc { get; set; }

    [Column("ward")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Ward { get; set; }

    [Column("smd")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Smd { get; set; }

    [Column("is_locked")]
    public bool? IsLocked { get; set; }

    [Column("roadway_seg_id")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RoadwaySegId { get; set; }

    [Column("directionality")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Directionality { get; set; }

    [Column("road_type")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RoadType { get; set; }

    [Column("segment_length")]
    [StringLength(20)]
    [Unicode(false)]
    public string? SegmentLength { get; set; }

    [Column("is_cbd")]
    public bool? IsCbd { get; set; }

    [Column("has_fire_hydrant")]
    public bool? HasFireHydrant { get; set; }

    [Column("has_bus_stop")]
    public bool? HasBusStop { get; set; }

    [Column("has_metro_entrace")]
    public bool? HasMetroEntrace { get; set; }

    [Column("is_baseball_stadium_area")]
    public bool? IsBaseballStadiumArea { get; set; }

    [Column("has_land_mark")]
    public bool? HasLandMark { get; set; }

    [Column("is_suspended_street")]
    public bool? IsSuspendedStreet { get; set; }

    [Column("function_class")]
    [StringLength(50)]
    [Unicode(false)]
    public string? FunctionClass { get; set; }

    [Column("is_historical")]
    public bool? IsHistorical { get; set; }

    [Column("is_fine_art")]
    public bool? IsFineArt { get; set; }

    [Column("is_road_closure")]
    public bool? IsRoadClosure { get; set; }

    [Column("is_covered_walkway")]
    public bool? IsCoveredWalkway { get; set; }

    [Column("covered_walkway_type")]
    [StringLength(50)]
    [Unicode(false)]
    public string? CoveredWalkwayType { get; set; }

    [Column("owner_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? OwnerName { get; set; }

    [Column("owner_care_of")]
    [StringLength(100)]
    [Unicode(false)]
    public string? OwnerCareOf { get; set; }

    [Column("owner_address")]
    [StringLength(100)]
    [Unicode(false)]
    public string? OwnerAddress { get; set; }

    [Column("owner_address2")]
    [StringLength(100)]
    [Unicode(false)]
    public string? OwnerAddress2 { get; set; }

    [Column("migrated_id")]
    public int? MigratedId { get; set; }

    [Column("row_directionality")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RowDirectionality { get; set; }

    [Column("row_ownership")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RowOwnership { get; set; }

    [Column("row_lifecycle_status")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RowLifecycleStatus { get; set; }

    [Column("row_st_name")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RowStName { get; set; }

    [Column("row_total")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RowTotal { get; set; }

    [Column("row_roadway")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RowRoadway { get; set; }

    [Column("row_sidewalk1")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RowSidewalk1 { get; set; }

    [Column("row_sidewalk1_ft")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RowSidewalk1Ft { get; set; }

    [Column("row_sidewalk2")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RowSidewalk2 { get; set; }

    [Column("row_sidewalk2_ft")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RowSidewalk2Ft { get; set; }

    [Column("row_parking1")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RowParking1 { get; set; }

    [Column("row_parking1_ft")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RowParking1Ft { get; set; }

    [Column("row_parking2")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RowParking2 { get; set; }

    [Column("row_parking2_ft")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RowParking2Ft { get; set; }

    [Column("bid_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string? BidName { get; set; }

    [Column("latitude")]
    public double? Latitude { get; set; }

    [Column("longitude")]
    public double? Longitude { get; set; }

    [Column("on_segx")]
    public double? OnSegx { get; set; }

    [Column("on_segy")]
    public double? OnSegy { get; set; }

    [Column("is_street_car")]
    public bool? IsStreetCar { get; set; }

    [Column("block_of_address")]
    [StringLength(255)]
    [Unicode(false)]
    public string? BlockOfAddress { get; set; }

    [Column("block_key")]
    [StringLength(255)]
    [Unicode(false)]
    public string? BlockKey { get; set; }

    [Column("sub_block_key")]
    [StringLength(255)]
    [Unicode(false)]
    public string? SubBlockKey { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("deleted_date", TypeName = "datetime")]
    public DateTime? DeletedDate { get; set; }

    [Column("deleted_by")]
    public int? DeletedBy { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; }

    [Column("modified_by")]
    public int? ModifiedBy { get; set; }

    [Column("modified_date", TypeName = "datetime")]
    public DateTime? ModifiedDate { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("created_date", TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    [Column("legacy_id_for_migration")]
    public int? LegacyIdForMigration { get; set; }

    [Column("is_migrated_from_tops")]
    public bool IsMigratedFromTops { get; set; }

    [ForeignKey("LocationCategoryId")]
    [InverseProperty("ProjectLocations")]
    public virtual LocationCategory? LocationCategory { get; set; } = null!;

    [ForeignKey("LocationTypeId")]
    [InverseProperty("ProjectLocations")]
    public virtual LocationType LocationType { get; set; } = null!;

    [InverseProperty("ProjectLocation")]
    public virtual ICollection<PdrmLocation> PdrmLocations { get; set; } = new List<PdrmLocation>();

    [ForeignKey("ProjectId")]
    [InverseProperty("ProjectLocations")]
    public virtual Project Project { get; set; } = null!;

    //Manually added
    [ForeignKey("CreatedBy")]
    public virtual User? CreatedUser { get; set; }

    [InverseProperty("ProjectLocation")]
    public virtual ICollection<ProjectLocationWardAnc> ProjectLocationWardAncs { get; set; } = new List<ProjectLocationWardAnc>();

}
