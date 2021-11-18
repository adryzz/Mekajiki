using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mekajiki.Data;

namespace Mekajiki.Types
{
    public class AnimeSeason : IAnimeSeason
    {
        public string Name { get; set; }
        public string DirectoryName { get; set; }
        public List<IAnimeEpisode> Episodes { get; set; }

        public AnimeSeason(string seasonDir, string[] fileTypes)
        {
            DirectoryName = Path.GetFileName(seasonDir);
            Name = TextUtils.RemoveTextInBrackets(DirectoryName);
                    
            //find all episodes
            IEnumerable<string> episodeFiles = Directory.EnumerateFiles(seasonDir);
            Episodes = new();
            foreach (string episodeFile in episodeFiles)
            {
                //check if they have a valid extension
                bool found = false;
                foreach (var type in fileTypes)
                {
                    if (episodeFile.EndsWith(type))
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    //add each file
                    Episodes.Add(new AnimeEpisode
                    {
                        FileName = Path.GetFileName(episodeFile)
                    });
                }
            }
        }

        public void Sort()
        {
            //order episodes by their number
            string[] remove = { "144p", "240p", "360p", "480p", "720p", "1080p", "1440p", "2160p", "mp4" };
            SortedDictionary<int, IAnimeEpisode> episodes2 = new();
            foreach (IAnimeEpisode e in Episodes)
            {
                string name = e.FileName;
                foreach (string s in remove)
                {
                    name = name.Replace(s, "");
                }
                        
                episodes2.Add(int.Parse(string.Join("", name.Where(char.IsDigit))), e);
            }

            Episodes = episodes2.Values.ToList();
            //add episode indexes
            for (int i = 0; i < Episodes.Count; i++)
            {
                Episodes[i].EpisodeIndex = i;
            }
        }
    }
}