using Mekajiki.Server.Utils;
using Mekajiki.Types.ServerInfo;

namespace Mekajiki.Server.Types.ServerInfo;

public class DiskInfo : IDiskInfo
{
    public DiskInfo()
    {
        var listing = AnimeListingUtils.GetListing();
        foreach (var episode in listing.Episodes.Values)
        {
            TotalLibrarySize++;
            var info = new FileInfo(episode.FilePath);
            TotalLibrarySizeBytes += info.Length;
        }

        FreeSpaceBytes += new DriveInfo(Program.Config.LibraryPath).AvailableFreeSpace;
    }

    public long TotalLibrarySize { get; }

    public long TotalLibrarySizeBytes { get; }

    public long FreeSpaceBytes { get; }
}