namespace Mekajiki.Types.ServerInfo
{
    public interface IServerInfo
    {
        public ITemperatureInfo? TemperatureInfo { get; }
        public IMemoryInfo? MemoryInfo { get; }
        public INetworkInfo? NetworkInfo { get; }
        public IDiskInfo? DiskInfo { get; }
        public IUptimeInfo? UptimeInfo { get; }
    }
}