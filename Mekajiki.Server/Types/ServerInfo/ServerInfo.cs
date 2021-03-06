using Mekajiki.Types.ServerInfo;

namespace Mekajiki.Server.Types.ServerInfo;

public class ServerInfo : IServerInfo
{
    public ServerInfo()
    {
        try
        {
            TemperatureInfo = new TemperatureInfo();
        }
        catch (Exception)
        {
            TemperatureInfo = null;
        }

        try
        {
            MemoryInfo = new MemoryInfo();
        }
        catch (Exception)
        {
            MemoryInfo = null;
        }

        try
        {
            NetworkInfo = new NetworkInfo();
        }
        catch (Exception)
        {
            NetworkInfo = null;
        }

        try
        {
            DiskInfo = new DiskInfo();
        }
        catch (Exception)
        {
            DiskInfo = null;
        }

        try
        {
            UptimeInfo = new UptimeInfo();
        }
        catch (Exception)
        {
            UptimeInfo = null;
        }
    }

    public ITemperatureInfo? TemperatureInfo { get; }
    public IMemoryInfo? MemoryInfo { get; }
    public INetworkInfo? NetworkInfo { get; }
    public IDiskInfo? DiskInfo { get; }
    public IUptimeInfo? UptimeInfo { get; }
}