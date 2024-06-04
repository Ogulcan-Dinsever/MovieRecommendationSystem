using MovieRecommendationSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Domain.Aggregate
{
    public class MoviePopularity : BaseEntity
    {
        public int TheMovieId { get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public List<string>? Notes { get; set; }
    }
}
