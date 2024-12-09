using MassTransit;
using UrFUCoworkingsModels.DTOs;
using UrFUCoworkingsModels.Requests.Times;
using UrFUCoworkingsModels.Responses.Times;

namespace UrFUCoworkingsAPIGateway.Rabbit.Services
{
    public class ReservationTimes
    {
        private readonly IServiceProvider Provider;
        public ReservationTimes(IServiceProvider provider) => Provider = provider;
        public async Task<List<(TimeOnly reservationBegin, TimeOnly reservationEnd)>> GetReservatedTimesAsync(Guid placeId, DateOnly date, CSDTO CSettings)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<GetReservatedTimesRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<GetReservatedTimesRequest>>();
            return (await requestClient.GetResponse<GetReservatedTimesResponse>(new() { PlaceId = placeId, Date = date, Setting = CSettings })).Message.ResponseData;
        }
    }
}
