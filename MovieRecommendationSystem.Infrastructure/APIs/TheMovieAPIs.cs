using Microsoft.Extensions.Configuration;
using RestSharp;
using Newtonsoft.Json;
using MovieRecommendationSystem.Infrastructure.Responses;

namespace MovieRecommendationSystem.Infrastructure.APIs
{
    public static class TheMovieAPIs
    {
        private static IConfiguration _config;
        private static string _authorizationKey;

        public static void Initialize(IConfiguration config)
        {
            _config = config;
            _authorizationKey = _config.GetConnectionString("TheMovieAuthorization");
        }
        public static async Task<MovieResponse> GetAllMovies()
        {
            var options = new RestClientOptions("https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=popularity.desc");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", _authorizationKey);
            var response = await client.GetAsync(request);

            var result = JsonConvert.DeserializeObject<MovieResponse>(response.Content);



            return result;
        }
    }
}
