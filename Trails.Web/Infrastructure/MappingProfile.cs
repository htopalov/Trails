using AutoMapper;
using Trails.Web.Areas.Identity.Pages.Account;
using Trails.Web.Areas.Identity.Pages.Account.Manage;
using Trails.Web.Data.DomainModels;
using Trails.Web.Models.Beacon;

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
                .ForMember(dest=>dest.KeyHash, opt=>opt.MapFrom(s=>s.Key));
        }
    }
}
