namespace Gateway;
using System.Text;
using RabbitMQ.Client;
public class HolidayAmpqGateway
{
    private readonly ConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    public HolidayAmpqGateway()
    {
        _factory = new ConnectionFactory { HostName = "localhost" };
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.ExchangeDeclare(exchange: "holiday_logs", type: ExchangeType.Fanout);
    }
 
    public void Publish(string holiday)
    {
        var body = Encoding.UTF8.GetBytes(holiday);
        _channel.BasicPublish(exchange: "holiday_logs",
                              routingKey: "holidayKey",
                              basicProperties: null,
                              body: body);
    }
    
    public void PublishNewHolidayPeriod(string holiday)
    {
        var body = Encoding.UTF8.GetBytes(holiday);
        _channel.BasicPublish(exchange: "holiday_WithHolidayPeriod_logs",
                              routingKey: "holiday_WithHolidayPeriod_Key",
                              basicProperties: null,
                              body: body);
    }
 
 
}