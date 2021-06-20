using AutoMapper;
using HooliCash.Core.Models;
using HooliCash.DTOs.Category;

namespace HooliCash.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
