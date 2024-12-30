using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;

namespace DDOT.MPS.Permit.Api.Managers.LocationManager
{
    public interface ILocationManager
    {
        Task<BaseResponse<LocationResponseDto[]>> GetLocationAsync(LocationDto req);
        Task<BaseResponse<BlockAddressResponseDto[]>> GetBlockAddressesAsync(string fullBlock);
        Task<BaseResponse<ReverseGeoCodeSslResponseDto>> GetReverseGeocoding(LocationRequest reverseGeocoding);
    }
}
