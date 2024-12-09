using MassTransit;
using UrFUCoworkingsModels.DTOs;
using UrFUCoworkingsModels.Requests.Places;
using UrFUCoworkingsModels.Responses.Places;

namespace UrFUCoworkingsAPIGateway.Rabbit.Services
{
    public class Places
    {
        private readonly IServiceProvider Provider;
        public Places(IServiceProvider provider) => Provider = provider;

        public async Task CreatePlaceAsync(Guid zoneId)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<CreatePlaceRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<CreatePlaceRequest>>();
            await requestClient.GetResponse<CreatePlaceResponse>(new() { ZoneId = zoneId });
        }

        public async Task<List<ReservationEdit>> DeletePlaceAsync(Guid placeId)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<DeletePlaceRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<DeletePlaceRequest>>();
            return (await requestClient.GetResponse<DeletePlaceResponse>(new() { PlaceId = placeId })).Message.ResponseData;
        }

        public async Task<List<PlaceDTO>> GetPlacesAsync(Guid zoneId)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<GetPlacesRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<GetPlacesRequest>>();
            return (await requestClient.GetResponse<GetPlacesResponse>(new() { ZoneId = zoneId })).Message.ResponseData;
        }

        public async Task<List<Guid>> TryUpdatePlaceAsync(PlaceDTO placeDTO)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<TryUpdatePlaceRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<TryUpdatePlaceRequest>>();
            return (await requestClient.GetResponse<TryUpdatePlaceResponse>(new() { RequestData = placeDTO })).Message.ResponseData;
        }

        public async Task<List<ReservationEdit>> UpdatePlaceAsync(PlaceDTO placeDTO)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<UpdatePlaceRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<UpdatePlaceRequest>>();
            return (await requestClient.GetResponse<UpdatePlaceResponse>(new() { RequestData = placeDTO })).Message.ResponseData;
        }
    }
}
