using MediatR;
using MovieRecommendationSystem.Application.Commands.MovieCommands;
using MovieRecommendationSystem.Application.Helpers;
using MovieRecommendationSystem.Application.Interfaces;
using MovieRecommendationSystem.Application.Responses;
using MovieRecommendationSystem.Application.Services;
using MovieRecommendationSystem.Application.Shared;
using MovieRecommendationSystem.Domain.Aggregate;
using MovieRecommendationSystem.Infrastructure.ServicesInfra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Handlers.MovieHandlers.CommandHandlers
{
    public class RecommendTheMovieHandler : IRequestHandler<RecommendTheMovieCommand, Response<bool>>
    {
        private readonly IRepository<Movie> _repository;
        private readonly IRepository<User> _userRepository;
        private readonly RabbitMQHelper _rabbitMQHelper;
        private readonly SendMailRequestHandler _handling;
        private readonly UserHelper _userHelper;

        public RecommendTheMovieHandler(IRepository<Movie> repository, IRepository<User> userRepository, RabbitMQHelper rabbitMQHelper, SendMailRequestHandler handling, UserHelper userHelper)
        {
            _repository = repository;
            _userRepository = userRepository;
            _rabbitMQHelper = rabbitMQHelper;
            _handling = handling;
            _userHelper = userHelper;
        }

        public async Task<Response<bool>> Handle(RecommendTheMovieCommand request, CancellationToken cancellationToken)
        {
            if (!GlobalFunctions.EmailControll(request.EmailAddress))
                return Response<bool>.Fail("Please enter a correct e-mail", 409);

            var userId = _userHelper.User.UserId;

            var getUser = await _userRepository.Find(x => x.Id == userId && x.Status);

            if (getUser == null)
                return Response<bool>.Fail("This user is not found", 409);

            var getMovie = await _repository.Find(x => x.TheMovieId == request.TheMovieId && x.Status);

            if (getMovie == null)
                return Response<bool>.Fail("This movie is not found", 409);

            string mailBody = GlobalFunctions.CreateMailBody(getMovie.title, getMovie.poster_path, getUser.Name);


            try
            {
                _rabbitMQHelper.SendEmailRequest(request.EmailAddress, mailBody);
            }
            catch (Exception)
            {
                return Response<bool>.Fail("An error occurred while sending the message to the queue", 409);
            }
            

            _handling.StartHandling();

            return Response<bool>.Success(true, 201);
        }
    }
}
