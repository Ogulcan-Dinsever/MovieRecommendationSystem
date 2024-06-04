using MediatR;
using MovieRecommendationSystem.Application.Commands.MovieCommands;
using MovieRecommendationSystem.Application.Interfaces;
using MovieRecommendationSystem.Application.Queries.MovieQueries;
using MovieRecommendationSystem.Application.Shared;
using MovieRecommendationSystem.Domain.Aggregate;
using MovieRecommendationSystem.Infrastructure.ServicesInfra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MovieRecommendationSystem.Application.Handlers.MovieHandlers.CommandHandlers
{
    public class RateTheMovieHandler : IRequestHandler<RateTheMovieCommand, Response<bool>>
    {
        private readonly IRepository<Movie> _repository;
        private readonly IRepository<MoviePopularity> _popularityRepository;

        public RateTheMovieHandler(IRepository<Movie> repository, IRepository<MoviePopularity> popularityRepository)
        {
            _repository = repository;
            _popularityRepository = popularityRepository;
        }
        public async Task<Response<bool>> Handle(RateTheMovieCommand request, CancellationToken cancellationToken)
        {
            if (request.Rate >= 1 && request.Rate <= 10)
                return Response<bool>.Fail("Please enter a number between 1 and 10", 409);

            var getMovie = await _repository.Any(x => x.Status && x.TheMovieId == request.TheMovieId);

            if (getMovie)
                return Response<bool>.Fail("Please enter a number between 1 and 10", 409);

            var getUserPopularity = await _popularityRepository.Find(x => x.Status && x.TheMovieId == request.TheMovieId && x.UserId == request.UserId);


            if (getUserPopularity == null)
            {
                MoviePopularity rating = new MoviePopularity
                {
                    UserId = request.UserId,
                    TheMovieId = request.TheMovieId,
                    CreatedDate = DateTime.Now,
                    Rate = request.Rate,
                    Status = true,
                    Notes = new List<string>() { request.Note }
                };

                await _popularityRepository.Create(rating);
            }
            else
            {


                getUserPopularity.UserId = request.UserId;
                getUserPopularity.TheMovieId = request.TheMovieId;
                getUserPopularity.CreatedDate = DateTime.Now;
                getUserPopularity.Rate = request.Rate;
                getUserPopularity.Status = true;
                getUserPopularity.Notes?.Add(request.Note);
            }

            return Response<bool>.Success(true, 201);
        }
    }
}
