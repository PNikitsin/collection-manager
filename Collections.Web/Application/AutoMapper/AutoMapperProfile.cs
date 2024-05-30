using AutoMapper;
using Collections.Domain.Entities;
using Collections.Web.ViewModels.Collection;
using Collections.Web.ViewModels.Item;

namespace Collections.Web.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateCollectionViewModel, Collection>();
            CreateMap<Collection, DetailsCollectionViewModel>();
            CreateMap<Item, ItemViewModel>();
        }
    }
}