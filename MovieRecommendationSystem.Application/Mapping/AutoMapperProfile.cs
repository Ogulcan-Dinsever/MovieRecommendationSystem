using AutoMapper;
using MovieRecommendationSystem.Application.Responses;
using MovieRecommendationSystem.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
           CreateMap<User, UserResponse>().ReverseMap();
           CreateMap<Movie, MovieResponse>().ReverseMap();
        }
    }
}
