using AutoMapper;
using DDOT.MPS.Permit.DataAccess.Entities;
using DDOT.MPS.Permit.Model.Dtos;
using DDOT.MPS.Permit.Model.Response;

namespace DDOT.MPS.Permit.Api.CustomConfigurations
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            MapperConfiguration? mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProjectResponseDto, Project>();
                config.CreateMap<Project, ProjectResponseDto>();

                config.CreateMap<Project, ProjectAddDto>();
                config.CreateMap<ProjectAddDto, Project>();

                config.CreateMap<ProjectCoreTeamDto, ProjectCoreTeam>();
                config.CreateMap<ProjectCoreTeam, ProjectCoreTeamDto>();

                config.CreateMap<EwrApplication, EwrRequestDto>();
                config.CreateMap<EwrRequestDto, EwrApplication>();
                config.CreateMap<EwrApplication, EwrResponseDto>();
                config.CreateMap<EwrResponseDto, EwrApplication>();
                config.CreateMap<EwrApplication, EwrCreateResponse>().ReverseMap();

                config.CreateMap<User, ProjectTeamMember>();
                config.CreateMap<ProjectTeamMember, User>();

                config.CreateMap<User, ProjectTeamMember>();
                config.CreateMap<ProjectTeamMember, User>();

                config.CreateMap<ProjectSupportTeam, ProjectTeamMember>();
                config.CreateMap<ProjectTeamMember, ProjectSupportTeam>();

                config.CreateMap<ProjectCoreTeam, ProjectTeamMember>();
                config.CreateMap<ProjectTeamMember, ProjectCoreTeam>();
                config.CreateMap<InspDetail, InspectionDto>();
                config.CreateMap<InspectionDto, InspDetail>();
                config.CreateMap<InspDetail, InspectionResponseDto>();
                config.CreateMap<InspectionResponseDto, InspDetail>();

                config.CreateMap<SwoApplication, SwoResponseDto>();
                config.CreateMap<SwoResponseDto, SwoApplication>();

                config.CreateMap<ProjectLocation, ProjectLocationAddress>()
                .ForMember(dest => dest.Square, opt => opt.MapFrom(src => src.ASquare))
                .ForMember(dest => dest.SquareLot, opt => opt.MapFrom(src => src.ALot))
                .ReverseMap();

                config.CreateMap<EwrLocationDto, EwrLocation>().ReverseMap();             
            });

            return mappingConfig;
        }
    }
}
