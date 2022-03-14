using AutoMapper;
using Trails.Api.Models;
using Trails.Data.DomainModels;
using Trails.GPXProcessor.Models.Export;
using Trails.Models.Beacon;
using Trails.Models.Event;
using Trails.Models.Participant;
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
                .ForMember(dest=>dest.RoutePoints, opt=>opt.MapFrom(s=>s.RoutePoints.OrderBy(p=>p.OrderNumber)))
                .ForMember(dest=>dest.HasEventStarted, opt=>opt.MapFrom(s=>s.Event.StartDate < DateTime.UtcNow))
                .ReverseMap();

            this.CreateMap<Route, RouteEditFormModel>()
                .ReverseMap();

            this.CreateMap<BaseRouteModel, Route>()
                .ReverseMap();

            this.CreateMap<RoutePoint, RoutePointExportModel>()
                .ReverseMap();

            this.CreateMap<RoutePoint, ExportPointModel>()
                .ReverseMap();

            this.CreateMap<Event, FirstToStartEventCardModel>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(s=> ImageProcessor.ProcessImageFromDb(s)))
                .ReverseMap();

            this.CreateMap<BaseEventModel, Event>()
                .ReverseMap();

            this.CreateMap<Event, UnapprovedEventDetailsModel>()
                .ForMember(dest=>dest.Creator, opt=>opt.MapFrom(s=> $"{s.Creator.FirstName} {s.Creator.LastName}"))
                .ReverseMap();

            this.CreateMap<Event, UnapprovedEventModel>()
                .ForMember(dest=>dest.DetailsModel,opt=>opt.MapFrom(s=>s))
                .ReverseMap();

            this.CreateMap<Event, EventPreparationModel>()
                .ReverseMap();

            this.CreateMap<Participant, ParticipantPreparationModel>()
                .ForMember(dest=>dest.FullName, opt=>opt.MapFrom(s=> $"{s.User.FirstName} {s.User.LastName}"))
                .ReverseMap();

            this.CreateMap<Beacon, BeaconPreparationModel>()
                .ReverseMap();

            this.CreateMap<BeaconData, ExportPointModel>()
                .ReverseMap();

            this.CreateMap<BeaconDataDtoPost, BeaconData>()
                .ReverseMap();

            this.CreateMap<Event, LiveEventCardModel>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(s => ImageProcessor.ProcessImageFromDb(s)))
                .ReverseMap();

            this.CreateMap<BeaconData, BeaconDataBroadcastModel>()
                .ReverseMap();

            this.CreateMap<Participant, LiveParticipantDetailsModel>()
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(s => $"{s.User.FirstName} {s.User.LastName}"))
                .ForMember(dest=>dest.CountryName, opt=>opt.MapFrom(s=>s.User.CountryName))
                .ForMember(dest=>dest.Gender, opt=>opt.MapFrom(s=>s.User.Gender))
                .ForMember(dest=>dest.BeaconData, opt=>opt.MapFrom(s=>s.BeaconData.OrderBy(bd=>bd.Timestamp)))
                .ReverseMap();

            this.CreateMap<Event, LiveEventDetailsModel>()
                .ForMember(dest=>dest.RoutePoints, opt=>opt.MapFrom(s=>s.Route.RoutePoints.OrderBy(r=>r.OrderNumber)))
                .ForMember(dest=>dest.Participants, opt=>opt.MapFrom(s=>s.Participants))
                .ReverseMap();
        }
    }
}
