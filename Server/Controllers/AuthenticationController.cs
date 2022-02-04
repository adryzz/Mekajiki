using Mekajiki.Server.Security;
using Microsoft.AspNetCore.Mvc;

namespace Mekajiki.Server.Controllers;

[ApiController]
[Route("api/v1/Authentication")]
public class TokenGenerationController : ControllerBase
{
    private readonly ILogger<AnimeListingController> _logger;

    public TokenGenerationController(ILogger<AnimeListingController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "NewUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult Post([FromHeader] string user, [FromHeader] string serverToken)
    {
        try
        {
            return Ok(SecurityManager.NewUserTotp(user, serverToken, Request.Headers["User-Agent"].ToString(), out var key, out var image));
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