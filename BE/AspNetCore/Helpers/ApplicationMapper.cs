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
        }
    }
}
