using AutoMapper;
using Collections.Domain.Entities;
using Collections.Web.ViewModels.Admin;
using Collections.Web.ViewModels.Collection;
using Collections.Web.ViewModels.Item;

namespace Collections.Web.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateCollectionViewModel, Collection>();
            CreateMap<Collection, DetailsCollectionViewModel>()
                .ForMember(dest => dest.Category, act => act.MapFrom(src => src.Category.Name));
            CreateMap<Item, ItemViewModel>();
            CreateMap<ApplicationUser, UserViewModel>();
        }
    }
}