using System;
using System.IO;
using System.Threading.Tasks;
using Mekajiki.Data;
using Mekajiki.Security;
using Mekajiki.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mekajiki.Controllers
{
    [ApiController]
    [Route($"api/v1/GetAnimeEpisode")]
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
        public async Task<IActionResult> Get(string token, Guid videoId)
        {
            if (SecurityManager.IsUser(token))
            {
                
                var listing = AnimeListingUtils.GetListing();
                IAnimeEpisode episode;
                bool found = listing.Episodes.TryGetValue(videoId, out episode);
                if (!found)
                {
                    return NotFound();
                }

                return PhysicalFile(episode.FilePath, "application/octet-stream", episode.FileName);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}