using AutoMapper;
using PixelPalette.Entities;
using PixelPalette.Models;

namespace PixelPalette.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<SignUpModel, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<Post, PostModel>().ReverseMap();
            CreateMap<Collection, CollectionModel>().ReverseMap();
        }
    }
}
