using MassTransit;
using UrFUCoworkingsModels.DTOs;
using UrFUCoworkingsModels.Requests.Reservations;
using UrFUCoworkingsModels.Responses.Reservations;
using CreateReservationRequest = UrFUCoworkingsModels.Requests.Reservations.CreateReservationRequest;
using UpdateReservationRequest = UrFUCoworkingsModels.Requests.Reservations.UpdateReservationRequest;

namespace UrFUCoworkingsAPIGateway.Rabbit.Services
{
    public class Reservations
    {
        private readonly IServiceProvider Provider;
        public Reservations(IServiceProvider provider) => Provider = provider;

        public async Task<List<ReservationView>> GetReservationsAsync(UserDTO user)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<GetReservationsRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<GetReservationsRequest>>();
            return (await requestClient.GetResponse<GetReservationsResponse>(new() { User = user })).Message.ResponseData;
        }

        public async Task<ReservationEdit> CreateReservationAsync(ReservationEdit reservation, CSDTO settings)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<CreateReservationRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<CreateReservationRequest>>();
            return (await requestClient.GetResponse<CreateReservationResponse>(new() { RequestData = reservation, Setting = settings })).Message.ResponseData;
        }

        public async Task<ReservationEdit> GetReservationByIdAsync(Guid reservationId)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<GetReservationByIdRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<GetReservationByIdRequest>>();
            return (await requestClient.GetResponse<GetReservationByIdResponse>(new() { ReservationId = reservationId })).Message.ResponseData;
        }

        public async Task<ReservationEdit> UpdateReservationAsync(ReservationEdit reservation, CSDTO settings)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<UpdateReservationRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<UpdateReservationRequest>>();
            return (await requestClient.GetResponse<UpdateReservationResponse>(new() { RequestData = reservation, Setting = settings })).Message.ResponseData;
        }

        public async Task<ReservationEdit> DeleteReservationAsync(Guid reservationId)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<DeleteReservationRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<DeleteReservationRequest>>();
            return (await requestClient.GetResponse<DeleteReservationResponse>(new() { ReservationId = reservationId})).Message.ResponseData;
        }
    }
}
