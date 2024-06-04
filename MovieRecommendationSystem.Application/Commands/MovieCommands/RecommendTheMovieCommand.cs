using MediatR;
using MovieRecommendationSystem.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Commands.MovieCommands
{
    public class RecommendTheMovieCommand : IRequest<Response<bool>>
    {
        public int TheMovieId { get; set; }
        public string EmailAddress { get; set; }
    }
}
