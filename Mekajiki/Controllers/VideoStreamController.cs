using System;
using Mekajiki.Data;
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
        public IActionResult Get(Guid videoId)
        {
            var listing = AnimeListingUtils.GetListing();
            IAnimeEpisode episode;
            bool found = listing.Episodes.TryGetValue(videoId, out episode);
            if (!found)
            {
                return NotFound();
            }

            return Ok();

        }
    }
}