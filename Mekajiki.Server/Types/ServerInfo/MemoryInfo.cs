using System.Text.RegularExpressions;
using Mekajiki.Types.ServerInfo;

namespace Mekajiki.Server.Types.ServerInfo;

public class MemoryInfo : IMemoryInfo
{
    public MemoryInfo()
    {
        var lines = File.ReadAllLines("/proc/meminfo");
        foreach (var line in lines)
        {
            var property = Regex.Match(line, @"^[a-zA-Z]+").Value;
            var valuestring = Regex.Match(line, @"\d+").Value;
            var value = long.Parse(valuestring) * 1024;
            switch (property)
            {
                case "MemTotal":
                    TotalMemorySizeBytes = value;
                    break;
                case "MemAvailable":
                    AvailableMemorySizeBytes = value;
                    break;
            }
        }
    }

    public long TotalMemorySizeBytes { get; }
    public long AvailableMemorySizeBytes { get; }
}