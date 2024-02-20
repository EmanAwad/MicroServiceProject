using ConsumerService;
using ConsumerService.Controllers;
using CrossCuttingLayer.Core;
using CrossCuttingLayer.Entities;
using MassTransit;
using MediatR;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddSingleton<DataStore>();
//builder.Services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
//builder.Services.AddMassTransit(busConfigurator =>
//{
//    busConfigurator.SetKebabCaseEndpointNameFormatter();
//    busConfigurator.AddConsumer<ConsumerController>();
//    busConfigurator.UsingRabbitMq((context, Configurator) =>
//    {
//        Configurator.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
//        {
//            h.Username(RabbitMqConsts.UserName);
//            h.Password(RabbitMqConsts.Password);
//        });

//        Configurator.ReceiveEndpoint("product-Queue", ep =>
//        {
//            ep.PrefetchCount = 16;
//            ep.UseMessageRetry(r => r.Interval(2, 100));
//            ep.Bind("send-report", e =>
//            {
//                e.RoutingKey = "private.*";
//                e.ExchangeType = ExchangeType.Topic;
//            });
//        });
//        Configurator.ConfigureEndpoints(context);
//    });
//});
// Assuming this code is within a ConfigureServices method of your Startup.cs or similar
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ConsumerController>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });
        cfg.ReceiveEndpoint("product-Queue", ep =>
        {
            ep.PrefetchCount = 16;
            ep.UseMessageRetry(r => r.Interval(2, 100));
            ep.ConfigureConsumer<ConsumerController>(provider);
            ep.Bind("product-Queue-Topic", e =>
            {
                e.RoutingKey = "private.*";
                e.ExchangeType = ExchangeType.Topic;
            });
        });
    }));
});
// Obtain an instance of IBusControl
var busControl = builder.Services.BuildServiceProvider().GetService<IBusControl>();
// Start the MassTransit bus
busControl.Start();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();