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
            if (AnimeListing.Cached == null || AnimeListing.CacheCreationTime == null)
            {
                return getListing(Program.Config.LibraryPath, Program.Config.VideoFileTypes, true);
            }
            else
            {
                if ((DateTime.Now - AnimeListing.CacheCreationTime) < Program.Config.LibraryCacheInvalidationTime)
                {
                    return AnimeListing.Cached;
                }
                else
                {
                    return getListing(Program.Config.LibraryPath, Program.Config.VideoFileTypes, false);
                }
            }
            
        }
        
        /// <summary>
        /// Reads an anime listing from the specified path using this format:
        /// Root -> Series -> Season -> Video files
        /// </summary>
        /// <param name="path"></param>
        private static AnimeListing getListing(string path, string[] fileTypes, bool setIds)
        {
            AnimeListing listing = new();
            List<IAnimeSeries> seriesList = new();
            
            //find all directories
            IEnumerable<string> seriesDirs = Directory.EnumerateDirectories(path);
            foreach (var seriesDir in seriesDirs)
            {
                AnimeSeries series = new(seriesDir, fileTypes, setIds);
                seriesList.Add(series);
            }

            listing.Series = seriesList.ToImmutableArray();
            
            return listing;
        }
    }
}