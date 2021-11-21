using System.IO;
using Mekajiki.Data;

namespace Mekajiki.Types.ServerInfo
{
    public class DiskInfo : IDiskInfo
    {
        public long TotalLibrarySize { get; }
        
        public long TotalLibrarySizeBytes { get; }
        
        public long FreeSpaceBytes { get; }

        public DiskInfo()
        {
            var listing = AnimeListingUtils.GetListing();
            foreach (IAnimeEpisode episode in listing.Episodes.Values)
            {
                TotalLibrarySize++;
                FileInfo info = new FileInfo(episode.FilePath);
                TotalLibrarySizeBytes += info.Length;
            }

            FreeSpaceBytes += new DriveInfo(Program.Config.LibraryPath).AvailableFreeSpace;
        }
    }
}