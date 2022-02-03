using System.Collections.Immutable;
using Mekajiki.Server.Shared;
using Mekajiki.Shared;

namespace Mekajiki.Server.Utils;

public static class AnimeListingUtils
{
    /// <summary>
    ///     Reads an anime listing from the specified path using this format:
    ///     Root -> Series -> Season -> Video files
    /// </summary>
    public static AnimeListing GetListing()
    {
        if (AnimeListing.Cached == null || AnimeListing.CacheCreationTime == null)
            return getListing(Program.Config.LibraryPath, Program.Config.VideoFileTypes, true);

        if (DateTime.Now - AnimeListing.CacheCreationTime < Program.Config.LibraryCacheInvalidationTime)
            return AnimeListing.Cached;
        return getListing(Program.Config.LibraryPath, Program.Config.VideoFileTypes, false);
    }

    /// <summary>
    ///     Reads an anime listing from the specified path using this format:
    ///     Root -> Series -> Season -> Video files
    /// </summary>
    /// <param name="path"></param>
    private static AnimeListing getListing(string path, string[] fileTypes, bool setIds)
    {
        AnimeListing listing = new();
        List<IAnimeSeries> seriesList = new();

        //find all directories
        var seriesDirs = Directory.EnumerateDirectories(path);
        foreach (var seriesDir in seriesDirs)
        {
            AnimeSeries series = new(seriesDir, fileTypes, setIds);
            seriesList.Add(series);
        }

        listing.Series = seriesList.ToImmutableArray();

        return listing;
    }
}