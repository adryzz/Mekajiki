namespace Mekajiki.Types.ServerInfo
{
    public interface IDiskInfo
    {
        /// <summary>
        /// The number of anime episodes in the library
        /// </summary>
        public long TotalLibrarySize { get; }
        
        /// <summary>
        /// The total size of the library, in bytes
        /// </summary>
        public long TotalLibrarySizeBytes { get; }
        
        /// <summary>
        /// The free space in the library disk, in bytes
        /// </summary>
        public long FreeSpaceBytes { get; }
    }
}