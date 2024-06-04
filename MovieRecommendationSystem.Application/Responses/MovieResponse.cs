using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Responses
{
    public class MovieResponse
    {
        public string Id { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public int TheMovieId { get; set; }
        public string title { get; set; }
        public bool adult { get; set; }
        public string poster_path { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public List<int> genre_ids { get; set; }
        public string original_title { get; set; }
        public string original_language { get; set; }
        public string backdrop_path { get; set; }
        public decimal popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public decimal vote_average { get; set; }
        public double Rating { get; set; }
        public List<string>? Notes { get; set; }
        public int UserRate { get; set; }
        public List<string>? UserNotes { get; set; }
    }
}
