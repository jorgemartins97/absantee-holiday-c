using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
public interface IRabbitMQHolidayPendingConsumerController
{
    void StartConsuming();
<<<<<<<< HEAD:WebApi/Controllers/IRabbitMQHolidayPendingConsumerController.cs
========
    void StopConsuming();
>>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33:WebApi/Controllers/IRabbitMQConsumerController.cs
    void ConfigQueue(string queueName);
=======

namespace WebApi.Controllers
{
    public interface IRabbitMQHolidayPendingConsumerController
    {
        void StartConsuming();
        void ConfigQueue(string queueName);
    }
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
}