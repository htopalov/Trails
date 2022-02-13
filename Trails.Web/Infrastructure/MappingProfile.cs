using System.Text.Json;
using AutoMapper;
using Trails.Web.Areas.Administration.Models.Beacon;
using Trails.Web.Areas.Identity.Pages.Account;
using Trails.Web.Areas.Identity.Pages.Account.Manage;
using Trails.Web.Data.DomainModels;
using Trails.Web.Models.Event;
using Trails.Web.Models.Route;
using Trails.Web.Models.RoutePoint;
using Route = Trails.Web.Data.DomainModels.Route;

namespace Trails.Web.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<RegisterModel.InputModel, User>();

            this.CreateMap<IndexModel.InputModel, User>()
                .ReverseMap();

            this.CreateMap<BeaconFormModel, Beacon>()
                .ForMember(dest => dest.KeyHash, opt => opt.MapFrom(s => s.Key))
                .ReverseMap();

            this.CreateMap<BaseBeaconModel, Beacon>()
                .ReverseMap();

            this.CreateMap<EventFormModel, Event>()
                .ForMember(dest=>dest.Image, opt=>opt.Ignore())
                .ReverseMap();

            this.CreateMap<RouteCreateModel, Route>()
                .ForMember(dest => dest.RoutePoints, opt => opt.Ignore())
                .ReverseMap();

            this.CreateMap<Event, EventDetailsModel>()
                .ForMember(dest=>dest.Image, opt=>opt.Ignore())
                .ReverseMap();

            this.CreateMap<Event, EventEditFormModel>()
                .ReverseMap();

            this.CreateMap<Route, RouteDetailsModel>()
                .ReverseMap();

            this.CreateMap<Route, RouteEditFormModel>()
                .ReverseMap();

            this.CreateMap<RoutePoint, RoutePointExportModel>()
                .ReverseMap();
        }
    }
}
