using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MovieRecommendationSystem.Application.Commands.MovieCommands;
using MovieRecommendationSystem.Application.Commands.UserCommands;
using MovieRecommendationSystem.Application.Interfaces;
using MovieRecommendationSystem.Application.Responses;
using MovieRecommendationSystem.Application.Shared;
using MovieRecommendationSystem.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Handlers.MovieHandlers.CommandHandlers
{
    public class SaveMovieHandler : IRequestHandler<SaveMovieCommand, Response<bool>>
    {
        private readonly IRepository<Movie> _repository;

        public SaveMovieHandler(IRepository<Movie> repository)
        {
            _repository = repository;
        }
        public async Task<Response<bool>> Handle(SaveMovieCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                return Response<bool>.Fail("bad request", 409);

            string exceptionMovieTitles = "";

            List<int> idsOfMoviesToBeSaved = request.MoviesRequest?.Select(s => s.id)?.ToList();

            var getMoviesInDatabase = await _repository.GetAll(x => x.Status && idsOfMoviesToBeSaved.Contains(x.TheMovieId));

            foreach (var item in request.MoviesRequest)
            {
                var movieInDatabase = getMoviesInDatabase?.FirstOrDefault(x => item.id == x.TheMovieId);

                if (movieInDatabase == null)
                {
                    Movie movie = new Movie
                    {
                        TheMovieId = item.id,
                        adult = item.adult,
                        backdrop_path = item.backdrop_path,
                        genre_ids = item.genre_ids,
                        original_language = item.original_language,
                        original_title = item.original_title,
                        overview = item.overview,
                        popularity = item.popularity,
                        poster_path = item.poster_path,
                        release_date = item.release_date,
                        title = item.title,
                        vote_count = item.vote_count,
                        vote_average = item.vote_average,
                        video = item.video,
                        CreatedBy = "System",
                        CreatedDate = DateTime.Now,
                        Status = true
                    };
                    try
                    {
                        await _repository.Create(movie);
                    }
                    catch (Exception)
                    {
                        exceptionMovieTitles = exceptionMovieTitles + $"{item.title}" + ", ";
                    }


                    continue;
                }

                movieInDatabase.TheMovieId = item.id;
                movieInDatabase.adult = item.adult;
                movieInDatabase.backdrop_path = item.backdrop_path;
                movieInDatabase.genre_ids = item.genre_ids;
                movieInDatabase.original_language = item.original_language;
                movieInDatabase.original_title = item.original_title;
                movieInDatabase.overview = item.overview;
                movieInDatabase.popularity = item.popularity;
                movieInDatabase.poster_path = item.poster_path;
                movieInDatabase.release_date = item.release_date;
                movieInDatabase.title = item.title;
                movieInDatabase.vote_count = item.vote_count;
                movieInDatabase.vote_average = item.vote_average;
                movieInDatabase.video = item.video;
                movieInDatabase.CreatedBy = "System";
                movieInDatabase.CreatedDate = DateTime.Now;
                movieInDatabase.Status = true;


                try
                {
                    await _repository.Update(movieInDatabase);
                }
                catch (Exception)
                {
                    exceptionMovieTitles = exceptionMovieTitles + $"{item.title}" + ", ";
                }
            }

            if (exceptionMovieTitles != "")
                return Response<bool>.Fail($"Movies Named {exceptionMovieTitles} could not be added", 409);

            return Response<bool>.Success(true, 201);
        }
    }
}
