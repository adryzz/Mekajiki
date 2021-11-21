using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace Mekajiki.Types.ServerInfo
{
    public class NetworkInfo : INetworkInfo
    {
        /// <summary>
        /// Total number of bytes sent
        /// </summary>
        public long SentBytes { get; }
        public long ReceivedBytes { get; }
        
        public long TotalRequests { get; }

        public NetworkInfo()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces().Where(x => x.NetworkInterfaceType != NetworkInterfaceType.Loopback).ToArray();

            foreach (NetworkInterface nic in nics)
            {
                SentBytes += nic.GetIPStatistics().BytesSent;
                ReceivedBytes += nic.GetIPStatistics().BytesReceived;
            }
        }
    }
}