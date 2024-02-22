using CoreLayer.Core;
using CoreLayer.Entities;
using MassTransit;
using StockService.Controllers;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddSingleton<DataStore>();

//builder.Services.AddMassTransit(x =>
//{
//    x.AddConsumer<StockController>();
//    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
//    {
//        cfg.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
//        {
//            h.Username(RabbitMqConsts.UserName);
//            h.Password(RabbitMqConsts.Password);
//        });
//        cfg.ReceiveEndpoint("product-Queue", ep =>
//        {
//            ep.ExchangeType = ExchangeType.Topic.ToString();
//            ep.PrefetchCount = 16;
//            ep.UseMessageRetry(r => r.Interval(2, 100));
//            ep.ConfigureConsumer<StockController>(provider);
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
//builder.Services.AddMassTransit(busConfigurator =>
//{
//    busConfigurator.SetKebabCaseEndpointNameFormatter();
//    busConfigurator.AddConsumer<StockController>();
//    busConfigurator.UsingRabbitMq((context, Configurator) =>
//    {
//        Configurator.Host(new Uri(RabbitMqConsts.RabbitMqRootUri)!, h =>
//        {
//            h.Username(RabbitMqConsts.UserName);
//            h.Password(RabbitMqConsts.Password);
//        });

//        Configurator.ReceiveEndpoint("product-Queue-Topic", re =>
//        {
//            re.Bind("product-Queue_Topic", e =>
//            {
//                e.RoutingKey = "private.*";
//                e.Durable = false;
//                e.AutoDelete = true;
//                e.ExchangeType = ExchangeType.Topic.ToString();
//            });
//        });
//        Configurator.ConfigureEndpoints(context);
//    });
//});
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.AddConsumer<StockController>();
    busConfigurator.UsingRabbitMq((context, Configurator) =>
    {
        Configurator.Host(new Uri(RabbitMqConsts.RabbitMqRootUri), h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });

        Configurator.ReceiveEndpoint("product-Queue", re =>
        {

            re.Bind("product-Queue-Topic", e =>
            {
                e.RoutingKey = "private.*";
                e.ExchangeType = RabbitMQ.Client.ExchangeType.Topic;
            });
        });
        Configurator.ConfigureEndpoints(context);
    });
});
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
