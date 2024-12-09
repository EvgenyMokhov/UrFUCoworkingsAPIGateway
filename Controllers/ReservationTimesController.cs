using Microsoft.AspNetCore.Mvc;
using UrFUCoworkingsAPIGateway.Rabbit;
using UrFUCoworkingsModels.DTOs;

namespace UrFUCoworkingsAPIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationTimesController : ControllerBase
    {
        private readonly RabbitManager Rabbit;
        public ReservationTimesController(IServiceProvider provider) => Rabbit = new(provider);

        [HttpGet("get", Name = "GetReservationTimes")]
        public async Task<IActionResult> GetReservationTimesAsync([FromQuery] Guid placeId, [FromQuery] DateOnly date, [FromBody] CSDTO settings)
        {
            return Ok(await Rabbit.ReservationTimes.GetReservatedTimesAsync(placeId, date, settings));
        }

    }
}
