namespace DDOT.MPS.Permit.Model.Dtos
{
    public class EwrLocationDto
    {
        public int? EwrLocationId { get; set; }
        public int LocationTypeId { get; set; }
        public int? LocationCategoryId { get; set; }
        public string? StreetClassification { get; set; }
        public string? Zone { get; set; }
        public bool? IsVerified { get; set; }
        public int? ReferenceId { get; set; }
        public int? AddressId { get; set; }
        public int? StreetSegId { get; set; }
        public int? Intersection1Id { get; set; }
        public int? Intersection2Id { get; set; }
        public string? FullDescription { get; set; }
        public string? MultipleAddress { get; set; }
        public string? AddrNum { get; set; }
        public string? BlockNo { get; set; }
        public string? StName { get; set; }
        public string? StName2 { get; set; }
        public string? StreetType { get; set; }
        public string? Quadrant { get; set; }
        public string? Quadrant2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? XCoord { get; set; }
        public string? YCoord { get; set; }
        public string? MarXCoord { get; set; }
        public string? MarYCoord { get; set; }
        public string? Ssl { get; set; }
        public string? ALot { get; set; }


        public string? ASquare { get; set; }


        public string? Anc { get; set; }


        public string? Ward { get; set; }


        public string? Smd { get; set; }


        public bool? IsLocked { get; set; }


        public string? RoadwaySegId { get; set; }


        public string? Directionality { get; set; }


        public string? RoadType { get; set; }


        public string? SegmentLength { get; set; }


        public bool? IsPsToAddConsWork { get; set; }

   
        public bool? IsCbd { get; set; }


        public bool? HasFireHydrant { get; set; }

        public bool? HasBusStop { get; set; }


        public bool? HasMetroEntrace { get; set; }


        public bool? IsBaseballStadiumArea { get; set; }


        public bool? HasLandMark { get; set; }


        public bool? IsSuspendedStreet { get; set; }


        public string? FunctionClass { get; set; }


        public bool? IsHistorical { get; set; }


        public bool? IsFineArt { get; set; }


        public bool? IsRoadClosure { get; set; }


        public bool? IsCoveredWalkway { get; set; }


        public string? CoveredWalkwayType { get; set; }


        public string? OwnerName { get; set; }

        public string? OwnerCareOf { get; set; }


        public string? OwnerAddress { get; set; }


        public string? OwnerAddress2 { get; set; }


        public int? MigratedId { get; set; }

        public string? RowDirectionality { get; set; }


        public string? RowOwnership { get; set; }


        public string? RowLifecycleStatus { get; set; }

        public string? RowStName { get; set; }


        public string? RowTotal { get; set; }


        public string? RowRoadway { get; set; }


        public string? RowSidewalk1 { get; set; }


        public string? RowSidewalk1Ft { get; set; }

        public string? RowSidewalk2 { get; set; }


        public string? RowSidewalk2Ft { get; set; }


        public string? RowParking1 { get; set; }


        public string? RowParking1Ft { get; set; }


        public string? RowParking2 { get; set; }

        public string? RowParking2Ft { get; set; }


        public string? BidName { get; set; }


        public double? Latitude { get; set; }


        public double? Longitude { get; set; }


        public double? OnSegx { get; set; }


        public double? OnSegy { get; set; }


        public string? BlockKey { get; set; }


        public string? SubBlockKey { get; set; }


        public bool IsActive { get; set; }

        public int? ModifiedBy { get; set; }


        public DateTime? ModifiedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LegacyIdForMigration { get; set; }

        public bool IsMigratedFromTops { get; set; }

        
    }
}
