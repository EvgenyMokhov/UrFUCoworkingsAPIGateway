using Microsoft.AspNetCore.Mvc;
using UrFUCoworkingsAPIGateway.Rabbit;
using UrFUCoworkingsModels.DTOs;

namespace UrFUCoworkingsAPIGateway.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ZonesController : ControllerBase
	{
		private readonly RabbitManager Rabbit;
		public ZonesController(IServiceProvider provider) => Rabbit = new(provider);

		[HttpGet("getallfor/{coworkingId}",Name = "GetZones")]
		public async Task<IActionResult> GetZones([FromRoute] Guid coworkingId)
		{
			return Ok(await Rabbit.Zones.GetZonesAsync(coworkingId));
		}

		[HttpPost("newin/{coworkingId}", Name = "CreateZone")]
		public async Task<IActionResult> CreateZone([FromRoute] Guid coworkingId)
		{
			await Rabbit.Zones.CreateZoneAsync(coworkingId);
			return Ok();
		}

        [HttpPut("update/try", Name = "TryUpdateZone")]
        public async Task<IActionResult> TryUpdateZone([FromBody] ZoneDTO zone)
        {
            return Ok(await Rabbit.Zones.TryUpdateZoneAsync(zone));
        }

        [HttpPut("update/dirty", Name = "UpdateZone")]
		public async Task<IActionResult> UpdateZone([FromBody] ZoneDTO zone)
		{
			return Ok(await Rabbit.Zones.UpdateZoneAsync(zone));
		}

		[HttpDelete("delete/{zoneId}", Name = "DeleteZone")]
		public async Task<IActionResult> DeleteZone([FromRoute] Guid zoneId)
		{
			return Ok(await Rabbit.Zones.DeleteZoneAsync(zoneId));
		}
	}
}
