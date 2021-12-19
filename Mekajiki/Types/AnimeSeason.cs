using System;
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

        public AnimeSeason(string seasonDir, string[] fileTypes, bool setIds)
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
                    string name = Path.GetFileName(episodeFile);
                    //add each file
                    Episodes.Add(new AnimeEpisode
                    {
                        FileName = name,
                        Name = Path.GetFileNameWithoutExtension(TextUtils.RemoveTextInBrackets(name).Trim()),
                        FilePath = episodeFile,
                        EpisodeId = setIds ?  Guid.NewGuid() : Guid.Empty,
                        Length = new FileInfo(episodeFile).Length
                    });
                }
            }
        }

        public void Sort()
        {
            int addIndex = 0;
            //order episodes by their number
            string[] remove = { "144p", "240p", "360p", "480p", "720p", "1080p", "1440p", "2160p", "mp4" };
            SortedDictionary<int, IAnimeEpisode> episodes2 = new();
            foreach (IAnimeEpisode e in Episodes)
            {
                string name = e.Name;
                foreach (string s in remove)
                {
                    name = name.Replace(s, "");
                }

                string indexString = string.Join("", name.Where(char.IsDigit));
                if (string.IsNullOrEmpty(indexString))
                {
                    indexString = Random.Shared.Next().ToString();
                }

                int index = int.Parse(indexString) + addIndex;
                while (episodes2.ContainsKey(index))
                {
                    addIndex++;
                    index++;
                }
                episodes2.Add(index, e);
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