using Mekajiki.Server.Security;
using Mekajiki.Server.Types.ServerInfo;
using Mekajiki.Types.ServerInfo;
using Microsoft.AspNetCore.Mvc;

namespace Mekajiki.Server.Controllers;

[ApiController]
[Route("api/v1/GetServerInfo")]
public class ServerInfoController : ControllerBase
{
    private readonly ILogger<ServerInfoController> _logger;

    public ServerInfoController(ILogger<ServerInfoController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetServerInfo")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IServerInfo))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Get([FromHeader] string token)
    {
        if (SecurityManager.IsUser(token))
            return Ok(new ServerInfo());
        return Unauthorized();
    }
}