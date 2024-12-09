using Microsoft.AspNetCore.Mvc;
using UrFUCoworkingsAPIGateway.Rabbit;
using UrFUCoworkingsModels.DTOs;

namespace UrFUCoworkingsAPIGateway.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CoworkingsController : ControllerBase
	{
		private readonly RabbitManager Rabbit;
		public CoworkingsController(IServiceProvider provider) => Rabbit = new(provider);

		[HttpGet("getall", Name = "GetCoworkings")]
		public async Task<IActionResult> GetCoworkings()
		{
			return Ok(await Rabbit.Coworkings.GetCoworkingsAsync());
		}

		[HttpGet("get/{coworkingId}", Name = "GetCoworking")]
		public async Task<IActionResult> GetCoworking([FromRoute] Guid coworkingId)
		{
			return Ok(await Rabbit.Coworkings.GetCoworkingByIdAsync(coworkingId));
		}

		[HttpPost("new", Name = "CreateCoworking")]
		public async Task<IActionResult> CreateCoworking()
		{
			await Rabbit.Coworkings.CreateCoworkingAsync();
			return Ok();
		}

        [HttpPut("update/try", Name = "TryUpdateCoworking")]
        public async Task<IActionResult> TryUpdateCoworking([FromBody] CoworkingDTO coworking)
        {
            return Ok(await Rabbit.Coworkings.TryUpdateCoworkingAsync(coworking));
        }

        [HttpPut("update/dirty", Name = "UpdateCoworking")]
		public async Task<IActionResult> UpdateCoworking([FromBody] CoworkingDTO coworking)
		{
			return Ok(await Rabbit.Coworkings.UpdateCoworkingAsync(coworking));
		}

        [HttpDelete("delete/{coworkingId}", Name = "DeleteCoworking")]
        public async Task<IActionResult> DeleteCoworking([FromRoute] Guid coworkingId)
        {
            return Ok(await Rabbit.Coworkings.DeleteCoworkingAsync(coworkingId));
        }
    }
}
