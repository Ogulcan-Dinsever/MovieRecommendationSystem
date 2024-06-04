using AutoMapper;
using MediatR;
using MovieRecommendationSystem.Application.Commands.MovieCommands;
using MovieRecommendationSystem.Application.Interfaces;
using MovieRecommendationSystem.Application.Queries.MovieQueries;
using MovieRecommendationSystem.Application.Responses;
using MovieRecommendationSystem.Application.Shared;
using MovieRecommendationSystem.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Handlers.MovieHandlers.QueryHandlers
{
    public class GetAllMovieHandler : IRequestHandler<GetAllMovieQuery, Response<List<MovieResponse>>>
    {
        private readonly IRepository<Movie> _repository;
        private readonly IMapper _mapper;

        public GetAllMovieHandler(IRepository<Movie> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<List<MovieResponse>>> Handle(GetAllMovieQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNum <= 0 || request.Take <= 0)
                return Response<List<MovieResponse>>.Fail("requests cannot be 0 or less than 0", 409);

            var getMovies = await _repository.GetAllTake((x => x.Status), request.Take, request.PageNum);

            var response = _mapper.Map<List<MovieResponse>>(getMovies);

            return Response<List<MovieResponse>>.Success(response, 200);
        }
    }
}
