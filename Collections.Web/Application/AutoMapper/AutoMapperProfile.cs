using AutoMapper;
using Collections.Domain.Entities;
using Collections.Web.ViewModels.Admin;
using Collections.Web.ViewModels.Collection;
using Collections.Web.ViewModels.Comment;
using Collections.Web.ViewModels.Item;

namespace Collections.Web.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateCollectionViewModel, Collection>();
            CreateMap<Collection, CollectionViewModel>()
                .ForMember(dest => dest.Category, act => act.MapFrom(src => src.Category.Name));
            CreateMap<Item, ItemViewModel>()
                .ForMember(dest => dest.Collection, act => act.MapFrom(src => src.Collection.Name))
                .ForMember(dest => dest.Author, act => act.MapFrom(src => src.Collection.Author));
            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<Comment, CommentViewModel>();
        }
    }
}