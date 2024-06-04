using MovieRecommendationSystem.Infrastructure.ServicesInfra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Services
{
    public class SendMailRequestHandler
    {
        private RabbitMQHelper _rabbitMQHelper;

        public SendMailRequestHandler(RabbitMQHelper rabbitMQHelper)
        {
            _rabbitMQHelper = rabbitMQHelper;
        }
        public void StartHandling()
        {
            _rabbitMQHelper.ConsumeSendEmailRequest((email, mailBody) =>
            {
                MailHelper.SendRecommandationMail(email, mailBody);
            });
        }
    }
}
