using System;

namespace Mekajiki.Types.ServerInfo
{
    public interface IUptimeInfo
    {
        /// <summary>
        /// The current time for the server
        /// </summary>
        public DateTime Time { get; set; }
        
        /// <summary>
        /// The time the server started up
        /// </summary>
        public DateTime ServerStartupTime { get; set; }
        
        /// <summary>
        /// The uptime of the server
        /// </summary>
        public TimeSpan ServerUptime { get; set; }
        
        /// <summary>
        /// The startup time of the api
        /// </summary>
        public DateTime StartupTime { get; set; }
        
        /// <summary>
        /// The uptime of the api
        /// </summary>
        public TimeSpan Uptime { get; set; }
    }
}