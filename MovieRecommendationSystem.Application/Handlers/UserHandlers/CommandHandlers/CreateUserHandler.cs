using AutoMapper;
using MediatR;
using MovieRecommendationSystem.Application.Commands.UserCommands;
using MovieRecommendationSystem.Application.Interfaces;
using MovieRecommendationSystem.Application.Responses;
using MovieRecommendationSystem.Application.Shared;
using MovieRecommendationSystem.Domain.Aggregate;
using MovieRecommendationSystem.Infrastructure.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Handlers.UserHandlers.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Response<UserResponse>>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<UserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            TheMovieAPIs.GetAllMovies();


            if (!GlobalFunctions.EmailControll(request.Email))
                return Response<UserResponse>.Fail("Please enter a correct e-mail", 409);

            if (String.IsNullOrWhiteSpace(request.Name) || String.IsNullOrWhiteSpace(request.SurName) || String.IsNullOrWhiteSpace(request.Password))
                return Response<UserResponse>.Fail("Name, Surname or Password cannot be empty", 409);

            var isThere = await _repository.Any(x => x.Status && x.Email == request.Email);

            if (isThere)
                return Response<UserResponse>.Fail("This email is already exist", 409);

            User user = new User
            {
                Email = request.Email,
                CreatedDate = DateTime.UtcNow,
                Password = GlobalFunctions.EncryptString(request.Password),
                Role = request.IsAdmin == true ? "Admin" : "User",
                Name = request.Name,
                SurName = request.SurName,
                CreatedBy = request.CreatedBy,
            };

            await _repository.Create(user);

            var response = _mapper.Map<UserResponse>(user);

            return Response<UserResponse>.Success(response, 201);
        }
    }
}
