namespace Mekajiki.Shared;

public interface IAnimeSeason
{
    public string Name { get; set; }
    public string DirectoryName { get; set; }
    public List<IAnimeEpisode> Episodes { get; set; }
}