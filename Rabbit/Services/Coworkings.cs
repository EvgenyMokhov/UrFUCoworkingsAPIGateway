using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using UrFUCoworkingsModels.DTOs;
using UrFUCoworkingsModels.Requests.Coworkings;
using UrFUCoworkingsModels.Responses.Coworkings;

namespace UrFUCoworkingsAPIGateway.Rabbit.Services
{
    public class Coworkings
    {
        private readonly IServiceProvider Provider;
        public Coworkings(IServiceProvider provider) => Provider = provider;

        public async Task CreateCoworkingAsync()
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<CreateCoworkingRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<CreateCoworkingRequest>>();
            await requestClient.GetResponse<CreateCoworkingResponse>(new());
        }

        public async Task<List<CoworkingDTO>> GetCoworkingsAsync()
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<GetCoworkingsRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<GetCoworkingsRequest>>();
            return (await requestClient.GetResponse<GetCoworkingsResponse>(new())).Message.ResponseData;
        }

        public async Task<CoworkingDTO> GetCoworkingByIdAsync(Guid coworkingId)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<GetCoworkingByIdRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<GetCoworkingByIdRequest>>();
            return (await requestClient.GetResponse<GetCoworkingByIdResponse>(new() { CoworkingId = coworkingId })).Message.ResponseData;
        }

        public async Task<List<ReservationEdit>> UpdateCoworkingAsync(CoworkingDTO coworking)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<UpdateCoworkingRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<UpdateCoworkingRequest>>();
            return (await requestClient.GetResponse<UpdateCoworkingResponse>(new() { RequestData = coworking })).Message.ResponseData;
        }

        public async Task<List<Guid>> TryUpdateCoworkingAsync(CoworkingDTO coworking)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<TryUpdateCoworkingRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<TryUpdateCoworkingRequest>>();
            return (await requestClient.GetResponse<TryUpdateCoworkingResponse>(new() { RequestData = coworking })).Message.ResponseData;
        }

        public async Task<List<ReservationEdit>> DeleteCoworkingAsync(Guid coworkingId)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<DeleteCoworkingRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<DeleteCoworkingRequest>>();
            return (await requestClient.GetResponse<DeleteCoworkingResponse>(new() { CoworkingId = coworkingId })).Message.ResponseData;
        }

    }
}
