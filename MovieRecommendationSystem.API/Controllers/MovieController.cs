using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendationSystem.Application.Commands.MovieCommands;
using MovieRecommendationSystem.Application.Commands.UserCommands;
using MovieRecommendationSystem.Application.Shared;

namespace MovieRecommendationSystem.API.Controllers
{
    [Route("api/MovieRecommendationSystem/[controller]")]
    [ApiController]
    public class MovieController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("SaveMovie")]
        public async Task<IActionResult> SaveMovie(SaveMovieCommand command)
        {
            var response = await _mediator.Send(command);

            return CreateActionResultInstance(response);
        }
    }
}
