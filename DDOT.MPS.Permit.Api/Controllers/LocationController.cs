using AutoMapper;
using DDOT.MPS.Permit.Api.Managers.LocationManager;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace DDOT.MPS.Permit.Api.Controllers
{
    [ApiController]
    public class LocationController : CoreController
    {
        private readonly ILocationManager _locationManager;

        public LocationController(ILocationManager locationManager, IMapper mapper)
        {
            _locationManager = locationManager;
        }

        [HttpPost("mar-v2-get-details")]
        public async Task<BaseResponse<LocationResponseDto[]>> Get([FromBody] LocationDto request)
        {
            BaseResponse<LocationResponseDto[]> createdLocation = await _locationManager.GetLocationAsync(request);
            return createdLocation;
        }

        [HttpGet("mar-v2-get-details/{fullBlock}")]
        public async Task<BaseResponse<BlockAddressResponseDto[]>> GetBlockDetails(string fullBlock)
        {
            BaseResponse<BlockAddressResponseDto[]> createdLocation = await _locationManager.GetBlockAddressesAsync(fullBlock);
            return createdLocation;
        }

        [HttpPost("mar-v2-reverse-geocoding")]
        public async Task<BaseResponse<ReverseGeoCodeSslResponseDto>> GetReverseGeocodingLocationDetail([FromBody] LocationRequest locationRequest)
        {
            return await _locationManager.GetReverseGeocoding(locationRequest);
        }
    }
}
