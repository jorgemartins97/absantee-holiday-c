using System.Text;
using Application.DTO;
using Application.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WebApi.Controllers
{
    public class RabbitMQAssociationConsumerController : IRabbitMQAssociationConsumerController
    {
        private List<string> _errorMessages = new List<string>();
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private string _queueName;

        public RabbitMQAssociationConsumerController(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _factory = new ConnectionFactory { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

<<<<<<< HEAD
            _channel.ExchangeDeclare(exchange: "associationPendent", type: ExchangeType.Fanout);
=======
            _channel.ExchangeDeclare(exchange: "association_logs", type: ExchangeType.Fanout);
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33

            Console.WriteLine(" [*] Waiting for messages from Associations.");
        }
        public void ConfigQueue(string queueName)
        {
            _queueName = "association" + queueName;

            _channel.QueueDeclare(queue: _queueName,
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

            _channel.QueueBind(queue: _queueName,
<<<<<<< HEAD
                  exchange: "associationPendent",
=======
                  exchange: "association_logs",
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
                  routingKey: string.Empty);
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                AssociationAmqpDTO associationDTO = AssociationAmqpDTO.Deserialize(message);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var associationService = scope.ServiceProvider.GetRequiredService<AssociationService>();
                    await associationService.Validations(associationDTO);
                }

                Console.WriteLine($" [x] Received {message}");
            };
            _channel.BasicConsume(queue: _queueName,
                                autoAck: true,
                                consumer: consumer);
        }
    }
}