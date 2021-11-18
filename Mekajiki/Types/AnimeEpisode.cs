namespace Mekajiki.Types
{
    public class AnimeEpisode : IAnimeEpisode
    {
        public int? EpisodeIndex { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
    }
}