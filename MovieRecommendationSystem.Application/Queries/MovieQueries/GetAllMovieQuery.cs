using MediatR;
using MovieRecommendationSystem.Application.Responses;
using MovieRecommendationSystem.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Queries.MovieQueries
{
    public class GetAllMovieQuery : IRequest<Response<List<MovieResponse>>>
    {
        public int Take { get; set; }
        public int PageNum { get; set; }
    }
}
