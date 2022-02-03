using Mekajiki.Server.Security;
using Mekajiki.Server.Utils;
using Mekajiki.Types;
using Microsoft.AspNetCore.Mvc;

namespace Mekajiki.Server.Controllers;

[ApiController]
[Route("api/v1/GetAnimeListing")]
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
            return Ok(AnimeListingUtils.GetListing());
        return Unauthorized();
    }
}