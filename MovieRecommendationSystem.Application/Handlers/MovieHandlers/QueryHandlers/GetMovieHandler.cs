using AutoMapper;
using MediatR;
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
    public class GetMovieHandler : IRequestHandler<GetMovieQuery, Response<MovieResponse>>
    {
        private readonly IRepository<Movie> _repository;
        private readonly IRepository<MoviePopularity> _popularityRepository;
        private readonly IMapper _mapper;

        public GetMovieHandler(IRepository<Movie> repository, IRepository<MoviePopularity> popularityRepository, IMapper mapper)
        {
            _repository = repository;
            _popularityRepository = popularityRepository;
            _mapper = mapper;
        }
        public async Task<Response<MovieResponse>> Handle(GetMovieQuery request, CancellationToken cancellationToken)
        {
            if (request.TheMovieId <= 0)
                return Response<MovieResponse>.Fail("request cannot be 0 or less than 0", 409);

            var getMovie = await _repository.Find(x => x.Status && request.TheMovieId == x.TheMovieId);

            if (getMovie == null)
                return Response<MovieResponse>.Fail("movie not found. please enter the `TheMovieId`.", 409);

            var response = _mapper.Map<MovieResponse>(getMovie);

            var notes = new List<string>();
            
            var moviePopularities = await _popularityRepository.GetAll(x=> x.Status && x.TheMovieId == request.TheMovieId);

            var rating = moviePopularities.Select(s=> s.Rate).ToList().Average();

            foreach (var item in moviePopularities)
            {
                if (item.Notes != null)
                    notes.AddRange(item.Notes);
            }

            response.Notes = notes;
            response.Rating = rating;

            if (request.UserId != null)
            {
                var userRate = moviePopularities?.FirstOrDefault(x => x.UserId == request.UserId);

                if (userRate != null) 
                { 
                    response.UserRate = userRate.Rate;
                    response.Rating = userRate.Rate;
                }
            }

            return Response<MovieResponse>.Success(response, 200);
        }
    }
}
