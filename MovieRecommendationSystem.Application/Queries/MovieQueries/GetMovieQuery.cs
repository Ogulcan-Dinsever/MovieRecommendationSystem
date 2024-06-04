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
    public class GetMovieQuery : IRequest<Response<MovieResponse>>
    {
        public int TheMovieId { get; set; }
        public string? UserId { get; set; }
    }
}
