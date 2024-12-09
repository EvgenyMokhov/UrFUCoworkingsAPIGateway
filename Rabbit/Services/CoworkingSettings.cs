using MassTransit;
using UrFUCoworkingsModels.DTOs;
using UrFUCoworkingsModels.Requests.Settings;
using UrFUCoworkingsModels.Responses.Settings;

namespace UrFUCoworkingsAPIGateway.Rabbit.Services
{
    public class CoworkingSettings
    {
        private readonly IServiceProvider Provider;
        public CoworkingSettings(IServiceProvider provider) => Provider = provider;

        public async Task CreateSettingAsync(Guid coworkingId)
        { 
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<CreateSettingRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<CreateSettingRequest>>();
            await requestClient.GetResponse<CreateSettingResponse>(new() { CoworkingId = coworkingId});
        }

        public async Task<List<ReservationEdit>> UpdateSettingAnywayAsync(CSDTO settingData)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<UpdateSettingAnywayRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<UpdateSettingAnywayRequest>>();
            return (await requestClient.GetResponse<UpdateSettingAnywayResponse>(new() { SettingData = settingData})).Message.ResponseData;
        }

        public async Task<List<Guid>> TryUpdateSettingAsync(CSDTO settingData)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<TryUpdateSettingRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<TryUpdateSettingRequest>>();
            return (await requestClient.GetResponse<TryUpdateSettingResponse>(new() { SettingData = settingData })).Message.ResponseData;
        }

        public async Task DeleteSettingAsync(Guid settingId)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<DeleteSettingRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<DeleteSettingRequest>>();
            await requestClient.GetResponse<DeleteSettingResponse>(new() { SettingId = settingId });
        }

        public async Task<List<CSDTO>> GetSettingsAsync(Guid coworkingId)
        {
            using IServiceScope scope = Provider.CreateScope();
            IRequestClient<GetSettingsRequest> requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<GetSettingsRequest>>();
            return (await requestClient.GetResponse<GetSettingsResponse>(new() { CoworkingId = coworkingId })).Message.ResponseData;
        }
    }
}
