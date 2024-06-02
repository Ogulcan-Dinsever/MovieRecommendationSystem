using MediatR;
using MovieRecommendationSystem.Application.Responses;
using MovieRecommendationSystem.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Commands.UserCommands
{
    public class LoginCommand : IRequest<Response<UserResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
