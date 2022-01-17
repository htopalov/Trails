using AutoMapper;
using Trails.Web.Areas.Identity.Pages.Account.Manage;
using Trails.Web.Data.DomainModels;

namespace Trails.Web.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<IndexModel.InputModel, User>()
                .ReverseMap();
        }
    }
}
