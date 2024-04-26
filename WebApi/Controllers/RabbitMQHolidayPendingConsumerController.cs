<<<<<<< HEAD
using RabbitMQ.Client;

using Application.DTO;
using RabbitMQ.Client.Events;
using System.Text;
using Application.Services;

=======
using Application.DTO;
using Application.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
namespace WebApi.Controllers
{
    public class RabbitMQHolidayPendingConsumerController : IRabbitMQHolidayPendingConsumerController
    {
<<<<<<< HEAD
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private List<string> _errorMessages = new List<string>();
        private string _queueName;
 
        public RabbitMQHolidayPendingConsumerController(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _factory = new ConnectionFactory { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
 
            _channel.ExchangeDeclare(exchange: "holiday_Pending_Logs", type: ExchangeType.Fanout);
 
            Console.WriteLine(" [*] Waiting for messages from pending holidays.");


        }

        
 
       
 
 
        public void StartConsuming()
        {
            Console.WriteLine(" h");
            
            var consumer = new EventingBasicConsumer(_channel);
            
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                HolidayDTO holidayAmpqDTO = HolidayGatewayDTO.Deserialize(message);
                Console.WriteLine($" [x] Received {message}");
                //_channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

                using (var scope = _scopeFactory.CreateScope()){
                var holidayPendingService = scope.ServiceProvider.GetRequiredService<HolidayPendingService>();
                await holidayPendingService.Add(holidayAmpqDTO, _errorMessages);
                };
            };
            _channel.BasicConsume(queue: _queueName,
                                autoAck: true,
                                consumer: consumer);
            
       
=======
        private List<string> _errorMessages = new List<string>();
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private string _queueName;

        public RabbitMQHolidayPendingConsumerController(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _factory = new ConnectionFactory { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();


            _channel.ExchangeDeclare(exchange: "holidayPendentResponse", type: ExchangeType.Fanout);

            Console.WriteLine(" [*] Waiting for messages from Holiday.");
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
        }

        public void ConfigQueue(string queueName)
        {
<<<<<<< HEAD
            _queueName = queueName;
=======
            _queueName = "pending" + queueName;
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33

            _channel.QueueDeclare(queue: _queueName,
                                            durable: true,
                                            exclusive: false,
                                            autoDelete: false,
                                            arguments: null);

<<<<<<< HEAD
            _channel.QueueBind(queue: _queueName, 
                  exchange: "holiday_Pending_Logs",
                  routingKey: string.Empty);
        }
 
        public void StopConsuming()
        {
            throw new NotImplementedException();
=======
            _channel.QueueBind(queue: _queueName,
                  exchange: "holidayPendentResponse",
                  routingKey: string.Empty);
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                HolidayDTO holidayDTO = HolidayGatewayDTO.Deserialize(message);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var associationService = scope.ServiceProvider.GetRequiredService<HolidayService>();

                    if (message.StartsWith("Ok"))
                    {
                        await associationService.Add(holidayDTO, _errorMessages);
                    }
                    else {
                        Console.WriteLine($"Holiday not approved.");
                    }
                    
                }

                Console.WriteLine($" [x] Received {message}");
            };
            _channel.BasicConsume(queue: _queueName,
                                autoAck: true,
                                consumer: consumer);
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
        }
    }
}