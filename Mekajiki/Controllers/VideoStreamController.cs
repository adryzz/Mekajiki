using System;
using System.IO;
using System.Threading.Tasks;
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Get(Guid videoId)
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

        async Task writeToOutputStream(Stream outputStream, string filePath)
        {
            byte[] buffer = new byte[Program.Config.VideoBufferSize];
            using (FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                long remainingBytes = stream.Length;

                while (remainingBytes > 0)
                {
                    int count = (int)(remainingBytes > buffer.Length ? buffer.Length : remainingBytes);
                    
                    int bytesRead = await stream.ReadAsync(buffer, 0, count);
                    
                    await outputStream.WriteAsync(buffer, 0, bytesRead);
                    remainingBytes -= bytesRead; 
                }
            }
        }
    }
}