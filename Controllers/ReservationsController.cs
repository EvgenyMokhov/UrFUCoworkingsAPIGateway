using Microsoft.AspNetCore.Mvc;
using UrFUCoworkingsAPIGateway.Rabbit;
using UrFUCoworkingsModels.DTOs;

namespace UrFUCoworkingsAPIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly RabbitManager Rabbit;
        public ReservationsController(IServiceProvider provider) => Rabbit = new(provider);

        [HttpGet("getall", Name = "GetReservations")]
        public async Task<IActionResult> GetReservations([FromQuery] UserDTO user)
        {
            return Ok(await Rabbit.Reservations.GetReservationsAsync(user));
        }

        [HttpGet("get/{reservationId}", Name = "GetReservation")]
        public async Task<IActionResult> GetReservation([FromRoute] Guid reservationId)
        {
            return Ok(Rabbit.Reservations.GetReservationByIdAsync(reservationId));
        }

        [HttpPost("new", Name = "CreateReservation")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationRequest request)
        {
            return Ok(await Rabbit.Reservations.CreateReservationAsync(request.Reservation, request.Setting));
        }

        [HttpPut("update", Name = "UpdateReservation")]
        public async Task<IActionResult> UpdateReservation([FromBody] UpdateReservationRequest request)
        {
            return Ok(await Rabbit.Reservations.UpdateReservationAsync(request.Reservation, request.Setting));
        }

        [HttpDelete("delete/{reservationId}", Name = "DeleteReservation")]
        public async Task<IActionResult> DeleteResesrvation([FromRoute] Guid reservationId)
        {
            await Rabbit.Reservations.DeleteReservationAsync(reservationId);
            return Ok();
        }

    }
}
