using Microsoft.AspNetCore.Http;
using MovieRecommendationSystem.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Helpers
{
    public class UserHelper
    {
        public readonly CurrentUser User;

        public UserHelper(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor != null)
            {
                var userContext = httpContextAccessor.HttpContext.User;
                var userId = userContext.Claims.FirstOrDefault(w => w.Type == ClaimTypes.NameIdentifier)?.Value;

                User = new CurrentUser
                {
                    UserId = userId
                };
            }
        }
    }
}
