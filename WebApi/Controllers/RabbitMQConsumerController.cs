using RabbitMQ.Client;

using Application.DTO;
using RabbitMQ.Client.Events;
using System.Text;
 
namespace WebApi.Controllers
{
    public class RabbitMQConsumerController : IRabbitMQConsumerController
    {
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
 
        public RabbitMQConsumerController()
        {
            _factory = new ConnectionFactory { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
 
            _channel.ExchangeDeclare(exchange: "holiday_logs",
                                 type: ExchangeType.Fanout);
 
            _channel.QueueDeclare(queue: "holidayQueue",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
 
            _channel.QueueBind(queue: "holidayQueue",
                               exchange: "holiday_logs",
                               routingKey: "holidayKey");
 
 
            Console.WriteLine(" [*] Waiting for messages.");


        }

        
 
       
 
 
        public void StartConsuming()
        {
            Console.WriteLine(" h");
            
            var consumer = new EventingBasicConsumer(_channel);
            
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                HolidayDTO holidayAmpqDTO = HolidayGatewayDTO.Deserialize(message);
                Console.WriteLine($" [x] Received {message}");
                //_channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
            _channel.BasicConsume(queue: "holidayQueue",
                                autoAck: true,
                                consumer: consumer);
            
       
        }
 
        public void StopConsuming()
        {
            throw new NotImplementedException();
        }
    }
}