using Mekajiki.Types;
using Mekajiki.Data;
using Mekajiki.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mekajiki.Controllers
{
    [ApiController]
    [Route($"api/v1/GetAnimeListing")]
    public class AnimeListingController : ControllerBase
    {
        private readonly ILogger<AnimeListingController> _logger;

        public AnimeListingController(ILogger<AnimeListingController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAnimeListing")]
        [ProducesResponseType(StatusCodes.Status302Found, Type = typeof(AnimeListing))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Get([FromHeader] string token)
        {
            if (SecurityManager.IsUser(token))
            {
                return Ok(AnimeListingUtils.GetListing());
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
