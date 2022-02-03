using System.Text.RegularExpressions;
using Mekajiki.Types.ServerInfo;

namespace Mekajiki.Server.Types.ServerInfo;

public class UptimeInfo : IUptimeInfo
{
    public UptimeInfo()
    {
        Time = DateTime.Now;
        var text = File.ReadAllText("/proc/uptime");
        var value = Regex.Match(text, @"^[\x21-\x7E]+").Value;
        ServerUptime = TimeSpan.FromSeconds(double.Parse(value));
        ServerStartupTime = Time.Subtract(ServerUptime);

        StartupTime = Program.StartupTime;
        Uptime = Time.Subtract(StartupTime);
    }

    public DateTime Time { get; set; }
    public DateTime ServerStartupTime { get; set; }
    public TimeSpan ServerUptime { get; set; }
    public DateTime StartupTime { get; set; }
    public TimeSpan Uptime { get; set; }
}