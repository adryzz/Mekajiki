using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Mekajiki.Types.ServerInfo
{
    public class UptimeInfo : IUptimeInfo
    {
        public DateTime Time { get; set; }
        public DateTime ServerStartupTime { get; set; }
        public TimeSpan ServerUptime { get; set; }
        public DateTime StartupTime { get; set; }
        public TimeSpan Uptime { get; set; }

        public UptimeInfo()
        {
            Time = DateTime.Now;
            string text = File.ReadAllText("/proc/uptime");
            string value = Regex.Match(text, @"^[\x21-\x7E]+").Value;
            ServerUptime = TimeSpan.FromSeconds(double.Parse(value));
            ServerStartupTime = Time.Subtract(ServerUptime);

            StartupTime = Program.StartupTime;
            Uptime = Time.Subtract(StartupTime);
        }
    }
}