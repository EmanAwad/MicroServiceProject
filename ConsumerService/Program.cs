using OrderService;
using OrderService.Controllers;
using CoreLayer.Core;
using CoreLayer.Entities;
using MassTransit;
using MediatR;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddSingleton<DataStore>();
// Assuming this code is within a ConfigureServices method of your Startup.cs or similar
//builder.Services.AddMassTransit(x =>
//{
//    x.AddConsumer<OrderConsumerController>();
//    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
//    {
//        cfg.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
//        {
//            h.Username(RabbitMqConsts.UserName);
//            h.Password(RabbitMqConsts.Password);
//        });
//        cfg.ReceiveEndpoint("product-Queue", ep =>
//        {

//            ep.PrefetchCount = 16;
//            ep.UseMessageRetry(r => r.Interval(2, 100));
//            ep.ConfigureConsumer<OrderConsumerController>(provider);
//            //ep.Bind("product-Queue-Topic", e =>
//            //{
//            //    e.RoutingKey = "private.*";
//            //    e.ExchangeType = ExchangeType.Topic;
//            //    e.Durable = true;
//            //    e.AutoDelete = false;
//            //});
//            //ep.Bind("product-Queue-Direct", e =>
//            //{
//            //    e.RoutingKey = "private.*";
//            //    e.ExchangeType = ExchangeType.Direct;
//            //    e.Durable = true;
//            //    e.AutoDelete = false;
//            //});
//            //ep.Bind("product-Queue-Headers", e =>
//            //{
//            //    e.RoutingKey = "private.*";
//            //    e.ExchangeType = ExchangeType.Headers;
//            //    e.Durable = true;
//            //    e.AutoDelete = false;
//            //});
//        });
//    }));
//});
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.AddConsumer<OrderConsumerController>();
    busConfigurator.UsingRabbitMq((context, Configurator) =>
    {
        Configurator.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });

        Configurator.ReceiveEndpoint("product-Queue", re => {});
        Configurator.ConfigureEndpoints(context);
    });
});

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
