using System.Text;
using RabbitMQ.Client;

namespace Gateway
{
    public class AssociationVerificationAmqpGateway
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public AssociationVerificationAmqpGateway()
        {
            _factory = new ConnectionFactory { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
<<<<<<< HEAD
            _channel.ExchangeDeclare(exchange: "associationPendentResponse", type: ExchangeType.Fanout);
=======
            _channel.ExchangeDeclare(exchange: "association_logs", type: ExchangeType.Fanout);
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
        }

        public void Publish(string holiday)
        {
            var body = Encoding.UTF8.GetBytes(holiday);
<<<<<<< HEAD
            _channel.BasicPublish(exchange: "associationPendentResponse",
=======
            _channel.BasicPublish(exchange: "association_logs",
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
                                  routingKey: string.Empty,
                                  basicProperties: null,
                                  body: body);
        }
    }
}