using MassTransit;
using UrFUCoworkingsModels.DTOs;
using UrFUCoworkingsModels.Requests.Zones;
using UrFUCoworkingsModels.Responses.Zones;

namespace UrFUCoworkingsAPIGateway.Rabbit.Services
{
    public class Zones
    {
        private readonly IServiceProvider Provider;
        public Zones(IServiceProvider provider) => Provider = provider;

        public async Task CreateZoneAsync(Guid coworkingId)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<CreateZoneRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<CreateZoneRequest>>();
            await requestClient.GetResponse<CreateZoneResponse>(new() { CoworkingId = coworkingId });
        }

        public async Task<List<Guid>> TryUpdateZoneAsync(ZoneDTO zone)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<TryUpdateZoneRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<TryUpdateZoneRequest>>();
            return (await requestClient.GetResponse<TryUpdateZoneResponse>(new() { RequestData = zone })).Message.ResponseData;
        }

        public async Task<List<ReservationEdit>> UpdateZoneAsync(ZoneDTO zone)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<UpdateZoneRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<UpdateZoneRequest>>();
            return (await requestClient.GetResponse<UpdateZoneResponse>(new() { RequestData = zone })).Message.ResponseData;
        }

        public async Task<List<ZoneDTO>> GetZonesAsync(Guid coworkingId)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<GetZonesRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<GetZonesRequest>>();
            return (await requestClient.GetResponse<GetZonesResponse>(new() { CoworkingId = coworkingId})).Message.ResponseData;
        }

        public async Task<List<ReservationEdit>> DeleteZoneAsync(Guid zoneId)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<DeleteZoneRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<DeleteZoneRequest>>();
            return (await requestClient.GetResponse<DeleteZoneResponse>(new() { ZoneId = zoneId})).Message.ResponseData;
        }
    }
}
