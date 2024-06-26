﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Infrastructure.Responses
{
    public class MovieResponse
    {
        public int page { get; set; }
        public List<GetAllMovie> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
