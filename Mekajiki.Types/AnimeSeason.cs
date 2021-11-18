using System.Collections.Generic;

namespace Mekajiki.Types
{
    public class AnimeSeason
    {
        public string Name { get; set; }
        public string DirectoryName { get; set; }
        public List<AnimeEpisode> Episodes { get; set; }
    }
}