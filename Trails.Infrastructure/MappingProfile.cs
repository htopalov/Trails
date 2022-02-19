using AutoMapper;
using Trails.Data.DomainModels;
using Trails.Models.Beacon;
using Trails.Models.Event;
using Trails.Models.Route;
using Trails.Models.RoutePoint;
using Route = Trails.Data.DomainModels.Route;

namespace Trails.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<BeaconFormModel, Beacon>()
                .ForMember(dest => dest.KeyHash, opt => opt.MapFrom(s => s.Key))
                .ReverseMap();

            this.CreateMap<BaseBeaconModel, Beacon>()
                .ReverseMap();

            this.CreateMap<EventFormModel, Event>()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<RouteCreateModel, Route>()
                .ForMember(dest => dest.RoutePoints, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<Event, EventDetailsModel>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(s => ImageProcessor.ProcessImageFromDb(s)))
                .ReverseMap();

            this.CreateMap<Event, EventEditFormModel>()
                .ReverseMap();

            this.CreateMap<Route, RouteDetailsModel>()
                .ReverseMap();

            this.CreateMap<Route, RouteEditFormModel>()
                .ReverseMap();

            this.CreateMap<BaseRouteModel, Route>()
                .ReverseMap();

            this.CreateMap<RoutePoint, RoutePointExportModel>()
                .ReverseMap();

            this.CreateMap<Event, FirstToStartEventCardModel>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(s=> ImageProcessor.ProcessImageFromDb(s)))
                .ReverseMap();

            this.CreateMap<BaseEventModel, Event>()
                .ReverseMap();
        }
    }
}
