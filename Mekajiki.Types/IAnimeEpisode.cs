namespace Mekajiki.Types
{
    public interface IAnimeEpisode
    {
        public int? EpisodeIndex { get; set; }
        public string FileName { get; set; }

        public string Name { get; set; }
    }
}