using System;

namespace Mekajiki.Types
{
    public class AnimeEpisode : IAnimeEpisode
    {
        public string FilePath { get; set; }
        public int? EpisodeIndex { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public Guid EpisodeId { get; set; }
        public long Length { get; set; }
    }
}