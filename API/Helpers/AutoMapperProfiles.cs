using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // we use formember to populate a prop .we specify which prop to populate ( PhotoUrl property ) , then for options where we are getting the data from
            CreateMap<AppUser, MemberDto>().ForMember(destProp => destProp.PhotoUrl, options => options.MapFrom(
                src => src.Photos.FirstOrDefault(p => p.IsMain).Url)).ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            // to stop the object cycle exception in our entities as they both refrence each other and (the values for them )
            CreateMap<Photo, PhotoDto>();

        }
    }
}