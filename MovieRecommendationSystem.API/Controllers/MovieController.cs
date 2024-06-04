using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieRecommendationSystem.Application.Commands.MovieCommands;
using MovieRecommendationSystem.Application.Commands.UserCommands;
using MovieRecommendationSystem.Application.Queries.MovieQueries;
using MovieRecommendationSystem.Application.Shared;
using MovieRecommendationSystem.Infrastructure.Responses;

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

        [HttpGet("GetAllMovie/{pageId}&{take}")]
        public async Task<IActionResult> GetAllMovie(int pageId, int take)
        {
            var response = await _mediator.Send(new GetAllMovieQuery { PageNum = pageId, Take = take });

            return CreateActionResultInstance(response);
        }

        [HttpGet("GetMovie/{theMovieId}&{userId}")]
        public async Task<IActionResult> GetMovie(int theMovieId, string userId)
        {
            var response = await _mediator.Send(new GetMovieQuery { TheMovieId = theMovieId, UserId = userId });

            return CreateActionResultInstance(response);
        }

        [HttpPost("RateTheMovie")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> RateTheMovie(RateTheMovieCommand command)
        {
            var response = await _mediator.Send(command);

            return CreateActionResultInstance(response);
        }


        [HttpPost("SaveMovie")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SaveMovie(SaveMovieCommand command)
        {
            var response = await _mediator.Send(command);

            return CreateActionResultInstance(response);
        }

        [HttpPost("RecommendTheMovie")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> RecommendTheMovie(RecommendTheMovieCommand command)
        {
            var response = await _mediator.Send(command);

            return CreateActionResultInstance(response);
        }
    }
}
