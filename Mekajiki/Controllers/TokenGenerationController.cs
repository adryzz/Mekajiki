using System;
using Mekajiki.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mekajiki.Controllers
{
    [ApiController]
    [Route($"api/v1/GenerateToken")]
    public class TokenGenerationController : ControllerBase
    {
        private readonly ILogger<AnimeListingController> _logger;

        public TokenGenerationController(ILogger<AnimeListingController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "GenerateToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Post([FromHeader] string user, [FromHeader] int otp)
        {
            try
            {
                return Ok(SecurityManager.NewUser(user, otp));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ArgumentException)
            {
                return Conflict();
            }
        }
    }
}