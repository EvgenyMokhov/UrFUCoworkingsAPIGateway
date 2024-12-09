using MessageSenderModels;
using Microsoft.AspNetCore.Mvc;
using UrFUCoworkingsAPIGateway.Rabbit;
using UrFUCoworkingsModels.DTOs;

namespace UrFUCoworkingsAPIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly RabbitManager Rabbit;
        public SettingsController(IServiceProvider provider) => Rabbit = new(provider);

        [HttpGet("getallfor/{coworkingId}", Name = "GetSettings")]
        public async Task<IActionResult> GetSettings([FromRoute] Guid coworkingId)
        {
            return Ok(await Rabbit.CoworkingSettings.GetSettingsAsync(coworkingId));
        }

        [HttpPost("newin/{coworkingId}", Name = "CreateSetting")]
        public async Task<IActionResult> CreateSetting([FromRoute] Guid coworkingId)
        {
            await Rabbit.CoworkingSettings.CreateSettingAsync(coworkingId);
            return Ok();
        }

        [HttpPut("update/try/", Name = "TryUpdateSetting")]
        public async Task<IActionResult> TryUpdateSetting([FromBody] CSDTO setting)
        {
            return Ok(await Rabbit.CoworkingSettings.TryUpdateSettingAsync(setting));
        }

        [HttpPut("update/dirty/",Name = "UpdateSettingAnyway")]
        public async Task<IActionResult> UpdateSettingAnyway([FromBody] CSDTO setting)
        {
            List<ReservationEdit> deletedReservations = await Rabbit.CoworkingSettings.UpdateSettingAnywayAsync(setting);
            Sender sender = new() { Id = Guid.Empty, Host = "host:localhost vhost:/", Login = "guest", Password = "guest" };
            List<Message> messages = deletedReservations.Select(reservation => new Message() { SendDate = DateTime.Now, Id = Guid.NewGuid(), Sender = sender, MessageData = $"Ваше бронирование {reservation.ReservationDay} c {reservation.ReservationBegin} по {reservation.ReservationEnd} было отменено администратором" }).ToList();
            foreach (Message message in messages)
                await Rabbit.MessageProvider.SendMessage(message);
            return Ok();
        }

        [HttpDelete("delete/{settingId}", Name = "DeleteSetting")]
        public async Task<IActionResult> DeleteSetting([FromRoute] Guid settingId)
        {
            await Rabbit.CoworkingSettings.DeleteSettingAsync(settingId);
            return Ok();
        }
    }
}
