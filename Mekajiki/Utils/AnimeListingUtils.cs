using Mekajiki.Types;

namespace Mekajiki.Data
{
    public static class AnimeListingUtils
    {
        /// <summary>
        /// Reads an anime listing from the specified path using this format:
        /// Root -> Series -> Season -> Video files
        /// </summary>
        /// <param name="path"></param>
        public static AnimeListing GetListing(string path)
        {
            return new AnimeListing();
        }
    }
}