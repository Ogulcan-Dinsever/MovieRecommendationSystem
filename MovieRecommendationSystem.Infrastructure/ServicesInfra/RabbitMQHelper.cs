using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Infrastructure.ServicesInfra
{
    public class RabbitMQHelper
    {
        private readonly ConnectionFactory _factory;
        private readonly IModel _channel;

        public RabbitMQHelper()
        {
            _factory = new ConnectionFactory();
            _factory.Uri = new Uri("amqps://uaryitsv:hM_0MuanNKcF3q6Wef94ObB8y3EaTQ6H@fish.rmq.cloudamqp.com/uaryitsv");
            var connection = _factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue: "RecommendationMail", durable: false, exclusive: false);
        }

        public void SendEmailRequest(string email, string mailBody)
        {
            string message = $"{email}|{mailBody}";
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: "RecommendationMail", basicProperties: null, body: body);
        }

        public void ConsumeSendEmailRequest(Action<string, string> onRecieved)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                string[] parts = message.Split('|');
                string email = parts[0];
                string mailBody = parts[1];

                onRecieved(email, mailBody);
            };

            _channel.BasicConsume(queue: "RecommendationMail", autoAck: true, consumer: consumer);
        }
    }
}
