using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Infrastructure.APIs
{
    public static class TheMovieAPIs
    {

        public static async void GetAllMovies()
        {
            HttpClient httpClient = new HttpClient();

            var response = await httpClient.GetAsync($"https://developers.themoviedb.org/3/getting-started/introduction");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var movies = JObject.Parse(content)["results"];


            int deneme = 0;
        }
    }
}
