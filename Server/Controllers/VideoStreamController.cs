using Mekajiki.Server.Security;
using Mekajiki.Server.Utils;
using Mekajiki.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Mekajiki.Server.Controllers;

[ApiController]
[Route("api/v1/GetAnimeEpisode")]
public class VideoStreamController : ControllerBase
{
    private readonly ILogger<VideoStreamController> _logger;

    public VideoStreamController(ILogger<VideoStreamController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetAnimeEpisode")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Get(string token, Guid videoId)
    {
        if (SecurityManager.IsUser(token))
        {
            var listing = AnimeListingUtils.GetListing();
            IAnimeEpisode? episode;
            var found = listing.Episodes.TryGetValue(videoId, out episode);
            if (!found) return NotFound();
            if (episode == null) //shouldn't happen, but handle it anyway
                return NoContent();

            return PhysicalFile(episode.FilePath, "application/octet-stream", episode.FileName, true);
        }

        return Unauthorized();
    }
}