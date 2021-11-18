using System.Collections.Generic;

namespace Mekajiki.Types
{
    public class AnimeSeries
    {
        public string Name { get; set; }
        public string DirectoryName { get; set; }
        public List<AnimeSeason> Seasons { get; set; }
    }
}