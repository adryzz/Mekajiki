using System;
using System.Collections.Generic;
using System.Linq;
using Mekajiki.Types;
using Mekajiki.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mekajiki.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeListingController : ControllerBase
    {
        private readonly ILogger<AnimeListingController> _logger;

        public AnimeListingController(ILogger<AnimeListingController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAnimeListing")]
        public AnimeListing Get()
        {
            return AnimeListingUtils.GetListing();
        }
    }
}
