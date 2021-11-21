using System.IO;
using System.Text.RegularExpressions;

namespace Mekajiki.Types.ServerInfo
{
    public class MemoryInfo : IMemoryInfo
    {
        public long TotalMemorySizeBytes { get; }
        public long AvailableMemorySizeBytes { get; }

        public MemoryInfo()
        {
            string[] lines = File.ReadAllLines("/proc/meminfo");
            foreach (string line in lines)
            {
                string property = Regex.Match(line, @"^[a-zA-Z]+").Value;
                string valuestring = Regex.Match(line, @"\d+").Value;
                long value = long.Parse(valuestring) * 1024;
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
    }
}