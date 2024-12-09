using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using RabbitMQ.Client;
using UrFUCoworkingsAPIGateway.Rabbit;
using UrFUCoworkingsModels.Requests.Coworkings;
using UrFUCoworkingsModels.Requests.Places;
using UrFUCoworkingsModels.Requests.Reservations;
using UrFUCoworkingsModels.Requests.Settings;
using UrFUCoworkingsModels.Requests.Times;
using UrFUCoworkingsModels.Requests.Zones;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMassTransit(x =>
{ 
    x.AddRequestClient<GetReservationsRequest>(new Uri("exchange:get-reservations-requests"));
    x.AddRequestClient<CreateReservationRequest>(new Uri("exchange:create-reservation-requests"));
    x.AddRequestClient<UpdateReservationRequest>(new Uri("exchange:update-reservation-requests"));
    x.AddRequestClient<GetReservationByIdRequest>(new Uri("exchange:get-reservation-by-id-requests"));
    x.AddRequestClient<DeleteReservationRequest>(new Uri("exchange:delete-reservation-requests"));
    x.AddRequestClient<GetReservatedTimesRequest>(new Uri("exchange:get-reservated-times-requests"));
    x.AddRequestClient<CreateCoworkingRequest>(new Uri("exchange:create-coworking-requests"));
    x.AddRequestClient<DeleteCoworkingRequest>(new Uri("exchange:delete-coworking-requests"));
    x.AddRequestClient<UpdateCoworkingRequest>(new Uri("exchange:update-coworking-requests"));
    x.AddRequestClient<TryUpdateCoworkingRequest>(new Uri("exchange:try-update-coworking-requests"));
    x.AddRequestClient<GetCoworkingByIdRequest>(new Uri("exchange:get-coworking-by-id-requests"));
    x.AddRequestClient<GetCoworkingsRequest>(new Uri("exchange:get-coworkings-requests"));
    x.AddRequestClient<CreateSettingRequest>(new Uri("exchange:create-setting-requests"));
    x.AddRequestClient<DeleteSettingRequest>(new Uri("exchange:delete-setting-requests"));
    x.AddRequestClient<GetSettingsRequest>(new Uri("exchange:get-settings-requests"));
    x.AddRequestClient<TryUpdateSettingRequest>(new Uri("exchange:try-update-setting-requests"));
    x.AddRequestClient<UpdateSettingAnywayRequest>(new Uri("exchange:update-setting-anyway-requests"));
    x.AddRequestClient<CreateZoneRequest>(new Uri("exchange:create-zone-requests"));
    x.AddRequestClient<DeleteZoneRequest>(new Uri("exchange:delete-zone-requests"));
    x.AddRequestClient<GetZonesRequest>(new Uri("exchange:get-zones-requests"));
    x.AddRequestClient<UpdateZoneRequest>(new Uri("exchange:update-zone-requests"));
    x.AddRequestClient<TryUpdateZoneRequest>(new Uri("exchange:try-delete-zone-requests"));
    x.AddRequestClient<CreatePlaceRequest> (new Uri("exchange:create-place-requests"));
    x.AddRequestClient<DeletePlaceRequest>(new Uri("exchange:delete-place-requests"));
    x.AddRequestClient<GetPlacesRequest>(new Uri("exchange:get-places-requests"));
    x.AddRequestClient<UpdatePlaceRequest>(new Uri("exchange:update-place-requests"));
    x.AddRequestClient<TryUpdatePlaceRequest>(new Uri("exchange:try-update-place-requests"));
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "vh9", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ExchangeType = ExchangeType.Fanout;
        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddMassTransit<IMessageBus>(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("message_provider_host", "vhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
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
