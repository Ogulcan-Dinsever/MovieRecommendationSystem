using MovieRecommendationSystem.Application.Commands.MovieCommands;
using MovieRecommendationSystem.Infrastructure.APIs;
using MovieRecommendationSystem.Infrastructure.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Services
{
    public class HangfireService
    {
        public async Task SaveDailyMovie()
        {
            var getMoviesFromTheMovieOrg = await TheMovieAPIs.GetAllMovies();

            var movies = getMoviesFromTheMovieOrg.results;

            var moviesRequest = movies.Select(s=> new MoviesRequestInfra
            {
                adult = s.adult,
                backdrop_path = s.backdrop_path,
                genre_ids = s.genre_ids,
                id = s.id,
                original_language = s.original_language,
                original_title = s.original_title,
                overview = s.overview,
                popularity = s.popularity,
                poster_path = s.poster_path,
                release_date = s.release_date,
                title = s.title,
                video = s.video,
                vote_average = s.vote_average,
                vote_count = s.vote_count,
            }).ToList();

            await RecommendationSystemAPIs.SaveMovie(new SaveMovieRequest { MoviesRequest = moviesRequest });
        }
    }
}
