
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
public interface IRabbitMQHolidayWithPeriodConsumerController
{
    void StartConsuming();
    void StopConsuming();
    void ConfigQueue(string queueName);
}