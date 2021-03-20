using Api.Domain.Interfaces.Services.Game;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private ILeaderboardService _service;
        public LeaderboardController(ILeaderboardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetTop100()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetTop100());
            }
            catch (Exception ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

}
