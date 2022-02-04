using System.Data;
using NodaTime;
using CircularBuffer;
using Mekajiki.Shared;

namespace Mekajiki.Server.Diagnostics;

public static class ServerManager
{
    private static Timer _timer = new Timer(_callback, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

    public const int BufferSize = 64;
    public static CircularBuffer<SystemInfoDataPoint> SystemInfo { get; } = new(BufferSize);

    private static void _callback(object? state)
    {
        SystemInfo.PushFront(Update());
    }
    
    public static SystemInfoDataPoint Update()
    {
        SystemInfoDataPoint p = new SystemInfoDataPoint();
        
        var data = systemInfo.sysinfo();
        
        p.Uptime = Duration.FromSeconds(data.uptime);
        
        p.TotalMem = data.totalram;

        p.MemUsage = data.totalram - data.freeram;

        p.CpuUsage = 0;

        if (File.Exists("/sys/class/thermal/thermal_zone0/temp")) ;
            var temp = File.ReadAllText("/sys/class/thermal/thermal_zone0/temp");
            p.Temp = int.Parse(temp);

        return p;
    }
}