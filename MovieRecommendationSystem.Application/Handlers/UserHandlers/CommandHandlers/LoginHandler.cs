using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MovieRecommendationSystem.Application.Commands.UserCommands;
using MovieRecommendationSystem.Application.Interfaces;
using MovieRecommendationSystem.Application.Responses;
using MovieRecommendationSystem.Application.Shared;
using MovieRecommendationSystem.Domain.Aggregate;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Handlers.UserHandlers.CommandHandlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, Response<UserResponse>>
    {
        private readonly IRepository<User> _repository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public LoginHandler(IRepository<User> repository, IConfiguration configuration, IMapper mapper)
        {
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<Response<UserResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (!GlobalFunctions.EmailControll(request.Email))
                return Response<UserResponse>.Fail("Please enter a correct e mail", 409);

            var user = await _repository.Find(x => x.Status && x.Email == request.Email);

            if (user == null)
                return Response<UserResponse>.Fail("Email address or username not found..", 409);

            if (request.Password != GlobalFunctions.DecryptString(user.Password))
                return Response<UserResponse>.Fail("Email address or username not found..", 409);

            var response = _mapper.Map<UserResponse>(user);

            response.Token = await BuildToken(user.Id, user.Role);

            return Response<UserResponse>.Success(response, 200);
        }

        private async Task<string> BuildToken(string userId, string userRole)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["keyjwt"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Role, userRole)
                }),

                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var response = tokenHandler.WriteToken(token);

            return response;
        }
    }
}
