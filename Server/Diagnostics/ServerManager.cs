using NodaTime;
using CircularBuffer;
using Mekajiki.Shared;

namespace Mekajiki.Server.Diagnostics;

public class ServerManager
{
    public const int BufferSize = 64;

    public Duration Uptime { get; private set; }
    public CircularBuffer<(ulong, ulong)> MemoryInfo { get; } = new(BufferSize);

    public CircularBuffer<int> Load { get; } = new(BufferSize);

    public CircularBuffer<int> Temperature { get; } = new CircularBuffer<int>(BufferSize);
    
    public SystemInfoDataPoint? Latest { get; private set; }

    public SystemInfoDataPoint Update()
    {
        SystemInfoDataPoint p = new SystemInfoDataPoint();
        
        var data = systemInfo.sysinfo();
        Uptime = Duration.FromSeconds(data.uptime);
        p.Uptime = Uptime;
        
        p.TotalMem = data.totalram;

        p.MemUsage = data.totalram - data.freeram;
        MemoryInfo.PushFront((p.TotalMem, p.MemUsage));

        p.CpuUsage = 0;
        Load.PushFront(p.CpuUsage);
        
        if (File.Exists("/sys/class/thermal/thermal_zone0/temp")) ;
            var temp = File.ReadAllText("/sys/class/thermal/thermal_zone0/temp");
            p.Temp = int.Parse(temp);
            Temperature.PushFront(p.Temp);

        Latest = p;
        return p;
    }
}