using Microsoft.EntityFrameworkCore;

using Application.Services;
using DataModel.Repository;
using DataModel.Mapper;
using Domain.Factory;
using Domain.IRepository;
using WebApi.Controllers;
using Gateway;


var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var holidayQueueName = config["HolidayCommands:" + args[0]];
var colaboratorQueueName = config["ColaboratorCommands:" + args[0]];
var holidayPeriodQueueName = config["HolidayPeriodCommands:" + args[0]];
var holidayPendingQueueName = config["HolidayPendingCommands:" + args[0]];
var associationQueueName = config["AssociationPendingCommands:" + args[0]];
<<<<<<< HEAD
var holidayPendingResponseQueueName = config["HolidayPendingResponseCommands:" + args[0]];

=======
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
var connection = config["ConnectionStrings:" + args[0]];

var port = getPort(holidayQueueName);
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AbsanteeContext>(opt =>
    //opt.UseInMemoryDatabase("AbsanteeList")
    //opt.UseSqlite("Data Source=AbsanteeDatabase.sqlite")
    opt.UseSqlite(Host.CreateApplicationBuilder().Configuration.GetConnectionString(holidayQueueName))
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IHolidayRepository, HolidayRepository>();
builder.Services.AddTransient<IHolidayFactory, HolidayFactory>();
builder.Services.AddTransient<HolidayMapper>();
builder.Services.AddTransient<HolidayService>();
builder.Services.AddTransient<HolidayPendingService>();
builder.Services.AddTransient<HolidayAmpqGateway>();
builder.Services.AddTransient<HolidayPendentAmqpGateway>();
builder.Services.AddTransient<AssociationVerificationAmqpGateway>();

builder.Services.AddSingleton<IHolidayPeriodFactory, HolidayPeriodFactory>();

<<<<<<< HEAD
builder.Services.AddSingleton<IRabbitMQAssociationConsumerController, RabbitMQAssociationConsumerController>();
builder.Services.AddTransient<AssociationService>();
=======
builder.Services.AddSingleton<IRabbitMQConsumerController, RabbitMQConsumerController>();
builder.Services.AddSingleton<IRabbitMQAssociationConsumerController, RabbitMQAssociationConsumerController>();
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33

builder.Services.AddTransient<IColaboratorsIdRepository, ColaboratorsIdRepository>();
builder.Services.AddTransient<IColaboratorIdFactory, ColaboratorIdFactory>();
builder.Services.AddTransient<ColaboratorsIdMapper>();
builder.Services.AddTransient<ColaboratorIdService>();
builder.Services.AddTransient<IRabbitMQColabConsumerController, RabbitMQColabConsumerController>();

<<<<<<< HEAD
builder.Services.AddSingleton<IRabbitMQHolidayPendingConsumerController, RabbitMQHolidayPendingConsumerController>(); 

builder.Services.AddSingleton<IRabbitMQHolidayPendingResponseConsumerController, RabbitMQHolidayPendingResponseConsumerController>(); 

builder.Services.AddSingleton<IRabbitMQHolidayCreatedConsumerController, RabbitMQHolidayCreatedConsumerController>(); 

builder.Services.AddTransient<IHolidayPendingRepository, HolidayPendingRepository>();
builder.Services.AddTransient<HolidayPendingMapper>();
builder.Services.AddTransient<HolidayPendingService>();

=======
builder.Services.AddTransient<IHolidayPendingRepository, HolidayPendingRepository>();
builder.Services.AddTransient<HolidayPendingMapper>();
builder.Services.AddTransient<HolidayPendingService>();
builder.Services.AddTransient<IRabbitMQHolidayPendingConsumerController, RabbitMQHolidayPendingConsumerController>();
>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); 

app.UseAuthorization();

<<<<<<< HEAD

var rabbitMQHolidayPendingConsumerService = app.Services.GetRequiredService<IRabbitMQHolidayPendingConsumerController>();
var rabbitMQHolidayPendingResponseConsumerService = app.Services.GetRequiredService<IRabbitMQHolidayPendingResponseConsumerController>();
var rabbitMQColabConsumerService = app.Services.GetRequiredService<IRabbitMQColabConsumerController>();
var rabbitMQAssociationConsumerService = app.Services.GetRequiredService<IRabbitMQAssociationConsumerController>();
var rabbitMQHolidayCreatedConsumerService = app.Services.GetRequiredService<IRabbitMQHolidayCreatedConsumerController>();


rabbitMQColabConsumerService.ConfigQueue(colaboratorQueueName);
rabbitMQHolidayPendingResponseConsumerService.ConfigQueue(holidayPendingResponseQueueName);
rabbitMQHolidayPendingConsumerService.ConfigQueue(holidayPendingQueueName);
rabbitMQAssociationConsumerService.ConfigQueue(associationQueueName);
rabbitMQHolidayCreatedConsumerService.ConfigQueue(holidayQueueName);

rabbitMQHolidayPendingResponseConsumerService.StartConsuming();
rabbitMQColabConsumerService.StartConsuming();
rabbitMQHolidayPendingConsumerService.StartConsuming();
rabbitMQAssociationConsumerService.StartConsuming();
rabbitMQHolidayCreatedConsumerService.StartConsuming();

app.MapControllers();

app.Run($"https://localhost:{port}");

=======

var rabbitMQHolidayPendingConsumerService = app.Services.GetRequiredService<IRabbitMQHolidayPendingConsumerController>();
var rabbitMQConsumerService = app.Services.GetRequiredService<IRabbitMQConsumerController>();
var rabbitMQColabConsumerService = app.Services.GetRequiredService<IRabbitMQColabConsumerController>();
var rabbitMQAssociationConsumerService = app.Services.GetRequiredService<IRabbitMQAssociationConsumerController>();

rabbitMQColabConsumerService.ConfigQueue(colaboratorQueueName);
rabbitMQConsumerService.ConfigQueue(holidayQueueName);
rabbitMQHolidayPendingConsumerService.ConfigQueue(holidayPendingQueueName);
rabbitMQAssociationConsumerService.ConfigQueue(associationQueueName);

rabbitMQConsumerService.StartConsuming();
rabbitMQColabConsumerService.StartConsuming();
rabbitMQHolidayPendingConsumerService.StartConsuming();
rabbitMQAssociationConsumerService.StartConsuming();

app.MapControllers();

app.Run($"https://localhost:{port}");

>>>>>>> 864ec9f506683fe48251ac746366aeeaab6e5f33
int getPort(string name)
{
    // Implement logic to map queue name to a unique port number
    // Example: Assign a unique port number based on the queue name suffix
    int basePort = 5020; // Start from port 5000
    int queueIndex = int.Parse(name.Substring(2)); // Extract the numeric part of the queue name (assuming it starts with 'Q')
    return basePort + queueIndex;
}