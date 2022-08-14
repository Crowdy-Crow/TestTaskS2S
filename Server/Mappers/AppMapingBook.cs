using API.Data.Models;
using AutoMapper;
using Contracts;

namespace API.Mappers
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Book,BookDTO>().ReverseMap();
        }
    }
}