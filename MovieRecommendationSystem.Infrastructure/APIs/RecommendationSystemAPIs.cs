using Flurl.Http;
using MovieRecommendationSystem.Infrastructure.Requests;
using MovieRecommendationSystem.Infrastructure.Responses;
using MovieRecommendationSystem.Infrastructure.SharedInfra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Infrastructure.APIs
{
    public static class RecommendationSystemAPIs
    {
        static string ipAdress = "localhost";

        public static async Task<bool> SaveMovie(SaveMovieRequest movies)
        {
            var response = await $"https://{ipAdress}:7228/api/MovieRecommendationSystem/Movie/SaveMovie".PostJsonAsync(movies).ReceiveJson<Response<bool>>();

            return response.Data;
        }
    }
}
