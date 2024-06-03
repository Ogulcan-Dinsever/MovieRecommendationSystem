using MediatR;
using MovieRecommendationSystem.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Commands.MovieCommands
{
    public class SaveMovieCommand : IRequest<Response<bool>>
    {
        public List<MoviesRequest>? MoviesRequest { get; set; }

    }
    public class MoviesRequest
    {
        public int id { get; set; }
        public string? title { get; set; }
        public bool adult { get; set; }
        public string? poster_path { get; set; }
        public string? overview { get; set; }
        public string? release_date { get; set; }
        public List<int>? genre_ids { get; set; }
        public string? original_title { get; set; }
        public string? original_language { get; set; }
        public string? backdrop_path { get; set; }
        public decimal popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public decimal vote_average { get; set; }
    }
}
