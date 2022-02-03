using Mekajiki.Server.Utils;
using Mekajiki.Shared;

namespace Mekajiki.Server.Shared;

public class AnimeSeries : IAnimeSeries
{
    public AnimeSeries(string seriesDir, string[] fileTypes, bool setIds)
    {
        Seasons = new List<IAnimeSeason>();
        DirectoryName = Path.GetFileName(seriesDir);
        Name = TextUtils.RemoveTextInBrackets(DirectoryName);
        //find all seasons
        var seasonDirs = Directory.EnumerateDirectories(seriesDir);
        foreach (var seasonDir in seasonDirs)
        {
            AnimeSeason season = new(seasonDir, fileTypes, setIds);
            season.Sort();
            Seasons.Add(season);
        }
    }

    public string Name { get; set; }
    public string DirectoryName { get; set; }
    public List<IAnimeSeason> Seasons { get; set; }
}