using NodaTime;

namespace Mekajiki.Shared;

public struct SystemInfoDataPoint
{
    public Duration Uptime { get; set; }
    
    public int CpuUsage { get; set; }
    
    public ulong TotalMem { get; set; }
    
    public ulong MemUsage { get; set; }
    
    public int Temp { get; set; }
}