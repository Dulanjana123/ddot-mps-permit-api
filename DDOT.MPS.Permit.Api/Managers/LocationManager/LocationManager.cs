using AutoMapper;
using DDOT.MPS.Permit.Core.CoreSettings;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Request;
using DDOT.MPS.Permit.Model.Response;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;
using static DDOT.MPS.Permit.Core.Exceptions.UserDefinedException;

namespace DDOT.MPS.Permit.Api.Managers.LocationManager
{
    public class LocationManager : ILocationManager
    {
        private readonly IMapper _mapper;
        private readonly GlobalAppSettings _globalAppSettings;
        private readonly HttpClient _httpClient; //This will be replaced with a static http client instance later

        public LocationManager(IMapper mapper, IOptions<GlobalAppSettings> globalAppSettings, IHttpClientFactory httpClientFactory)
        {
            _mapper = mapper;
            _globalAppSettings = globalAppSettings.Value;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<BaseResponse<LocationResponseDto[]>> GetLocationAsync(LocationDto req)
        {
            switch (req.Type.ToLower())
            {
                case "address":

                    if (string.IsNullOrEmpty(req.AddressStreetNumber) && string.IsNullOrEmpty(req.AddressStreetName) && string.IsNullOrEmpty(req.AddressStreetNameQuadrant))
                    {
                        throw new UDValiationException("LOCATION_DETAILS_MISSING");
                    }

                    string encodedAddress = WebUtility.UrlEncode($"{req.AddressStreetNumber} {req.AddressStreetName} {req.AddressStreetNameQuadrant.ToUpper()}");
                    encodedAddress = encodedAddress.Replace("+", "%20");
                    string addressUrl = $"{_globalAppSettings.LocationDetailsApiUrl}{encodedAddress}?apikey={_globalAppSettings.MarApiKey}";
                    List<LocationResponseDto> locationDtos = new List<LocationResponseDto>();
                    try
                    {
                        string response = await _httpClient.GetStringAsync(addressUrl);
                        LocationInternalResponseDto locationResponse = JsonSerializer.Deserialize<LocationInternalResponseDto>(response);

                        if (locationResponse != null && locationResponse.Result != null)
                        {
                            try
                            {
                                foreach (AddressInfo addressInfo in locationResponse.Result.Addresses)
                                {
                                    Properties properties = addressInfo.Address.Properties;

                                    LocationResponseDto locationDto = new LocationResponseDto
                                    {
                                        Ward = properties.Ward,
                                        Anc = properties.Anc,
                                        Square = properties.SSL.Split(" ")[0].Trim(),
                                        SquareLot = properties.SSL.Split(" ")[1].Trim(),
                                        Zipcode = properties.ZipCode,
                                        Address = properties.Address,
                                        Score = properties.Score,
                                        MarId = properties.MarId,
                                        XCoordinate = properties.XCoordinate,
                                        YCoordinate = properties.YCoordinate
                                    };

                                    locationDtos.Add(locationDto);
                                }
                            }
                            catch (NullReferenceException)
                            {
                                return new BaseResponse<LocationResponseDto[]>
                                {
                                    Success = false,
                                    Message = "ERROR_IN_REQUEST_STRING",
                                    Data = null
                                };
                            }
                        }

                        return new BaseResponse<LocationResponseDto[]>
                        {
                            Success = true,
                            Message = "LOCATIONS_RETRIEVED_SUCCESSFULLY",
                            Data = locationDtos.ToArray()
                        };
                    }
                    catch (Exception ex)
                    {
                        return new BaseResponse<LocationResponseDto[]>
                        {
                            Success = false,
                            Message = "ERROR_RETRIEVING_LOCATIONS",
                            Data = null
                        };

                    }
                case "block":
                    if (string.IsNullOrEmpty(req.BlockStreetName) && string.IsNullOrEmpty(req.BlockStreetNameQuadrant) && string.IsNullOrEmpty(req.BlockStreet1) && string.IsNullOrEmpty(req.BlockStreet1Quadrant) && string.IsNullOrEmpty(req.BlockStreet2) && string.IsNullOrEmpty(req.BlockStreet2Quadrant))
                    {
                        throw new UDValiationException("LOCATION_DETAILS_MISSING");
                    }
                    string encodedBlock = WebUtility.UrlEncode($"{req.BlockStreetName} {req.BlockStreetNameQuadrant.ToUpper()} from {req.BlockStreet1} {req.BlockStreet1Quadrant.ToUpper()} to {req.BlockStreet2} {req.BlockStreet2Quadrant.ToUpper()}");
                    encodedBlock = encodedBlock.Replace("+", "%20");
                    string blockUrl = $"{_globalAppSettings.LocationDetailsApiUrl}{encodedBlock}?apikey={_globalAppSettings.MarApiKey}";
                    List<LocationResponseDto> BlockLocationDtos = new List<LocationResponseDto>();
                    try
                    {
                        string response = await _httpClient.GetStringAsync(blockUrl);

                        BlockInternalResponseDto result = JsonSerializer.Deserialize<BlockInternalResponseDto>(response);

                        if (result != null && result.Result != null)
                        {
                            try
                            {
                                foreach (Block block in result.Result.Blocks)
                                {

                                    LocationResponseDto blockLocationDto = new LocationResponseDto
                                    {
                                        Ward = block.BlockDetail?.Properties?.Ward,
                                        BlockName = block.BlockDetail?.Properties?.BlockName,
                                        FullBlock = block.BlockDetail?.Properties?.FullBlock,
                                        Score = block.BlockDetail?.Properties?.Score,
                                        MarId = block.BlockDetail?.Properties?.MarId,
                                        XCoordinate = block.BlockDetail?.Properties?.XCoordinate,
                                        YCoordinate = block.BlockDetail?.Properties?.YCoordinate
                                    };

                                    BlockLocationDtos.Add(blockLocationDto);
                                }
                            }
                            catch (NullReferenceException)
                            {
                                return new BaseResponse<LocationResponseDto[]>
                                {
                                    Success = false,
                                    Message = "ERROR_IN_REQUEST_STRING",
                                    Data = null
                                };
                            }

                        }

                        return new BaseResponse<LocationResponseDto[]>
                        {
                            Success = true,
                            Message = "LOCATIONS_RETRIEVED_SUCCESSFULLY",
                            Data = BlockLocationDtos.ToArray()
                        };
                    }
                    catch (Exception ex)
                    {
                        return new BaseResponse<LocationResponseDto[]>
                        {
                            Success = false,
                            Message = "ERROR_RETRIEVING_LOCATIONS",
                            Data = null
                        };

                    }

                case "intersection":
                    if (string.IsNullOrEmpty(req.IntersectionStreet1) && string.IsNullOrEmpty(req.IntersectionStreet1Quadrant) && string.IsNullOrEmpty(req.IntersectionStreet2) && string.IsNullOrEmpty(req.IntersectionStreet2Quadrant))
                    {
                        throw new UDValiationException("LOCATION_DETAILS_MISSING");
                    }
                    string encodedIntersection = WebUtility.UrlEncode($"{req.IntersectionStreet1} {req.IntersectionStreet1Quadrant.ToUpper()} and {req.IntersectionStreet2} {req.IntersectionStreet2Quadrant.ToUpper()}");
                    encodedIntersection = encodedIntersection.Replace("+", "%20");
                    string IntersectionUrl = $"{_globalAppSettings.LocationDetailsApiUrl}{encodedIntersection}?apikey={_globalAppSettings.MarApiKey}";
                    List<LocationResponseDto> intersectionLocationDtos = new List<LocationResponseDto>();
                    try
                    {
                        string response = await _httpClient.GetStringAsync(IntersectionUrl);

                        IntersectionInternalResponse result = JsonSerializer.Deserialize<IntersectionInternalResponse>(response);

                        if (result != null && result.Result != null)
                        {
                            try
                            {
                                foreach (IntersectionWrapper intersectionWrapper in result.Result.Intersections)
                                {
                                    IntersectionProperties properties = intersectionWrapper.Intersection.Properties;
                                    LocationResponseDto blockLocationDto = new LocationResponseDto
                                    {
                                        Street1 = properties.St1Name,
                                        Street2 = properties.St2Name,
                                        MarId = properties.MarId,
                                        Score = properties.Score,
                                        XCoordinate = properties.XCoordinate,
                                        YCoordinate = properties.YCoordinate
                                    };

                                    intersectionLocationDtos.Add(blockLocationDto);
                                }
                            }
                            catch (NullReferenceException)
                            {
                                return new BaseResponse<LocationResponseDto[]>
                                {
                                    Success = false,
                                    Message = "ERROR_IN_REQUEST_STRING",
                                    Data = null
                                };
                            }
                        }

                        return new BaseResponse<LocationResponseDto[]>
                        {
                            Success = true,
                            Message = "LOCATIONS_RETRIEVED_SUCCESSFULLY",
                            Data = intersectionLocationDtos.ToArray()
                        };
                    }
                    catch (Exception ex)
                    {
                        return new BaseResponse<LocationResponseDto[]>
                        {
                            Success = false,
                            Message = "ERROR_RETRIEVING_LOCATIONS",
                            Data = null
                        };

                    }
                default:
                    return new BaseResponse<LocationResponseDto[]>
                    {
                        Success = false,
                        Message = "INVALID_LOCATION_TYPE",
                        Data = null
                    };
            }

        }

        public async Task<BaseResponse<BlockAddressResponseDto[]>> GetBlockAddressesAsync(string fullBlock)
        {
            string blockUrl = $"{_globalAppSettings.LocationDetailsApiUrl}{fullBlock}?apikey={_globalAppSettings.MarApiKey}";
            List<BlockAddressResponseDto> BlockLocationDtos = new List<BlockAddressResponseDto>();
            try
            {
                string response = await _httpClient.GetStringAsync(blockUrl);

                BlockInternalResponseDto result = JsonSerializer.Deserialize<BlockInternalResponseDto>(response);

                if (result != null && result.Result != null)
                {
                    try
                    {
                        foreach (BlockAddress block in result.Result.BlockAddresses)
                        {

                            BlockAddressResponseDto blockLocationDto = new BlockAddressResponseDto
                            {
                                Address = block.Address?.Properties?.FullAddress,
                                Ward = block.Address?.Properties?.Ward,
                                Anc = block.Address?.Properties?.Anc,
                                Square = block.Address?.Properties?.Ssl?.Split(" ")[0].Trim(),
                                SquareLot = block.Address?.Properties?.Ssl?.Split(" ")[1].Trim()
                            };

                            BlockLocationDtos.Add(blockLocationDto);
                        }
                    }
                    catch (NullReferenceException)
                    {
                        return new BaseResponse<BlockAddressResponseDto[]>
                        {
                            Success = false,
                            Message = "ERROR_IN_REQUEST_STRING",
                            Data = null
                        };
                    }

                }

                return new BaseResponse<BlockAddressResponseDto[]>
                {
                    Success = true,
                    Message = "LOCATIONS_RETRIEVED_SUCCESSFULLY",
                    Data = BlockLocationDtos.ToArray()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<BlockAddressResponseDto[]>
                {
                    Success = false,
                    Message = "ERROR_RETRIEVING_LOCATIONS",
                    Data = null
                };

            }
        }

        public async Task<BaseResponse<ReverseGeoCodeSslResponseDto>> GetReverseGeocoding(LocationRequest reverseGeocoding)
        {
            string reverseGeocodeUrl = $"{_globalAppSettings.LocationDetailsApiUrl}{reverseGeocoding.Longitude},{reverseGeocoding.Latitude}?apikey={_globalAppSettings.MarApiKey}";

            try
            {
                ReverseGeoCodeResponseDto result = await _httpClient.GetFromJsonAsync<ReverseGeoCodeResponseDto>(reverseGeocodeUrl);

                if (result != null && result.Result != null) {
                    string marId = result.Result[0].Address.Properties.MarId;

                    if (marId != null)
                    {
                        string sslQueryUrl = $"{_globalAppSettings.SSLQueryApiUrl}?marid={marId}&apikey={_globalAppSettings.MarApiKey}";
                        SslQueryResponseDto sslData = await _httpClient.GetFromJsonAsync<SslQueryResponseDto>(sslQueryUrl);

                        ReverseGeoCodeSslResponseDto response = new ReverseGeoCodeSslResponseDto()
                        {
                            Address = result.Result[0].Address,
                            Distance = result.Result[0].Distance,
                            Units = result.Result[0].Units,
                            Zones = result.Result[0].Zones,
                            sslData = sslData.Result.Ssls[0]
                        };

                        return new BaseResponse<ReverseGeoCodeSslResponseDto>
                        {
                            Success = true,
                            Message = "LOCATION_RETRIEVED_SUCCESSFULLY",
                            Data = response
                        };
                    }

                    ReverseGeoCodeSslResponseDto sslEmptyresponse = new ReverseGeoCodeSslResponseDto()
                    {
                        Address = result.Result[0].Address,
                        Distance = result.Result[0].Distance,
                        Units = result.Result[0].Units,
                        Zones = result.Result[0].Zones,
                        sslData = null
                    };

                    return new BaseResponse<ReverseGeoCodeSslResponseDto>
                    {
                        Success = true,
                        Message = "LOCATION_RETRIEVED_SUCCESSFULLY",
                        Data = sslEmptyresponse
                    };
                }

                return new BaseResponse<ReverseGeoCodeSslResponseDto>
                {
                    Success = false,
                    Data = null,
                    Message = "LOCATION_NOT_FOUND"
                };
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
