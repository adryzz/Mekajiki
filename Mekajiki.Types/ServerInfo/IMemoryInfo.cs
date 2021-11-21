namespace Mekajiki.Types.ServerInfo
{
    public interface IMemoryInfo
    {
        public long TotalMemorySizeBytes { get; }
        public long AvailableMemorySizeBytes { get; }
    }
}