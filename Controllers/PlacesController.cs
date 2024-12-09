using Microsoft.AspNetCore.Mvc;
using UrFUCoworkingsAPIGateway.Rabbit;
using UrFUCoworkingsModels.DTOs;

namespace UrFUCoworkingsAPIGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly RabbitManager Rabbit;
        public PlacesController(IServiceProvider provider) => Rabbit = new(provider);

        [HttpGet("getallfor/{zoneId}", Name = "GetPlaces")]
        public async Task<IActionResult> GetPlaces([FromRoute] Guid zoneId)
        {
            return Ok(await Rabbit.Places.GetPlacesAsync(zoneId));
        }

        [HttpPost("newin/{zoneId}", Name = "CreatePlace")]
        public async Task<IActionResult> CreatePlace([FromRoute] Guid zoneId)
        {
            await Rabbit.Places.CreatePlaceAsync(zoneId);
            return Ok();
        }

        [HttpPut("update/try", Name = "TryUpdatePlace")]
        public async Task<IActionResult> TryUpdatePlace([FromRoute] PlaceDTO place)
        {
            return Ok(await Rabbit.Places.TryUpdatePlaceAsync(place));
        }

        [HttpPut("update/dirty", Name = "UpdatePlace")]
        public async Task<IActionResult> UpdatePlace([FromRoute] PlaceDTO place)
        {
            return Ok(await Rabbit.Places.UpdatePlaceAsync(place));
        }

        [HttpDelete("delete", Name = "DeletePlace")]
        public async Task<IActionResult> DeletePlace([FromRoute] Guid placeId)
        {
            return Ok(await Rabbit.Places.DeletePlaceAsync(placeId));
        }
    }
}
