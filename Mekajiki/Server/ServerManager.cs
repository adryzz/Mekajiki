using CircularBuffer;
using NodaTime;

namespace Mekajiki.Server;

public class ServerManager
{
    public const int BufferSize = 64;
    public Duration Uptime { get; private set; }
    public CircularBuffer<(ulong, ulong)> MemoryInfo { get; private set; } = new CircularBuffer<(ulong, ulong)>(BufferSize);

    public CircularBuffer<int> Load { get; private set; } = new CircularBuffer<int>(BufferSize);

    public ServerManager()
    {
        
    }
    
    public void Update()
    {
        var data = systemInfo.sysinfo();
        Uptime = Duration.FromSeconds(data.uptime);
        MemoryInfo.PushFront((data.totalram, data.totalram - data.freeram));
        Load.PushFront(0);
    }
}