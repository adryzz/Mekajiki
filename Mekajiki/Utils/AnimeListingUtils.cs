using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using Mekajiki.Types;
using System.IO;
using System.Linq;

namespace Mekajiki.Data
{
    public static class AnimeListingUtils
    {
        /// <summary>
        /// Reads an anime listing from the specified path using this format:
        /// Root -> Series -> Season -> Video files
        /// </summary>
        public static AnimeListing GetListing()
        {
            if (AnimeListing.Cached != null && AnimeListing.CacheCreationTime != null &&
                (DateTime.Now - AnimeListing.CacheCreationTime) > Program.Config.LibraryCacheInvalidationTime)
            {
                return AnimeListing.Cached;
            }
            return getListing(Program.Config.LibraryPath, Program.Config.VideoFileTypes);
        }
        
        /// <summary>
        /// Reads an anime listing from the specified path using this format:
        /// Root -> Series -> Season -> Video files
        /// </summary>
        /// <param name="path"></param>
        private static AnimeListing getListing(string path, string[] fileTypes)
        {
            AnimeListing listing = new AnimeListing();
            List<AnimeSeries> seriesList = new List<AnimeSeries>();
            
            //find all directories
            IEnumerable<string> seriesDirs = Directory.EnumerateDirectories(path);
            foreach (var seriesDir in seriesDirs)
            {
                AnimeSeries series = new AnimeSeries();
                //find all seasons
                IEnumerable<string> seasonDirs = Directory.EnumerateDirectories(seriesDir);
                foreach (var seasonDir in seasonDirs)
                {
                    AnimeSeason season = new AnimeSeason();
                    season.DirectoryName = Path.GetDirectoryName(seasonDir);
                    season.Name = season.DirectoryName;
                    
                    //find all episodes
                    IEnumerable<string> episodeFiles = Directory.EnumerateFiles(seasonDir);
                    List<AnimeEpisode> episodes = new List<AnimeEpisode>();
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
                            episodes.Add(new AnimeEpisode
                            {
                                FileName = Path.GetFileName(episodeFile)
                            });
                        }
                    }
                    
                    //order episodes by their number
                    string[] remove = { "144p", "240p", "360p", "480p", "720p", "1080p", "1440p", "2160p", "mp4" };
                    SortedDictionary<int, AnimeEpisode> episodes2 = new SortedDictionary<int, AnimeEpisode>();
                    foreach (AnimeEpisode e in episodes)
                    {
                        string name = e.FileName;
                        foreach (string s in remove)
                        {
                            name = name.Replace(s, "");
                        }
                        episodes2.Add(int.Parse(string.Join("", name.Where(char.IsDigit))), e);
                    }

                    season.Episodes = episodes2.Values.ToList();
                    series.Seasons.Add(season);
                }
                seriesList.Add(series);
            }

            listing.Series = seriesList.ToImmutableArray();
            
            return listing;
        }
    }
}