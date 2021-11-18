using System.Collections.Generic;
using System.IO;

namespace Mekajiki.Types
{
    public class AnimeSeries : IAnimeSeries
    {
        public string Name { get; set; }
        public string DirectoryName { get; set; }
        public List<IAnimeSeason> Seasons { get; set; }

        public AnimeSeries(string seriesDir, string[] fileTypes)
        {
            Seasons = new List<IAnimeSeason>();
            DirectoryName = Path.GetFileName(seriesDir);
            Name = DirectoryName;
            //find all seasons
            IEnumerable<string> seasonDirs = Directory.EnumerateDirectories(seriesDir);
            foreach (var seasonDir in seasonDirs)
            {
                AnimeSeason season = new(seasonDir, fileTypes);
                season.Sort();
                Seasons.Add(season);
            }
        }
    }
}