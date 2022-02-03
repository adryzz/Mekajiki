using System.Net.NetworkInformation;
using Mekajiki.Types.ServerInfo;

namespace Mekajiki.Server.Types.ServerInfo;

public class NetworkInfo : INetworkInfo
{
    public NetworkInfo()
    {
        var nics = NetworkInterface.GetAllNetworkInterfaces()
            .Where(x => x.NetworkInterfaceType != NetworkInterfaceType.Loopback).ToArray();

        foreach (var nic in nics)
        {
            SentBytes += nic.GetIPStatistics().BytesSent;
            ReceivedBytes += nic.GetIPStatistics().BytesReceived;
        }
    }

    /// <summary>
    ///     Total number of bytes sent
    /// </summary>
    public long SentBytes { get; }

    public long ReceivedBytes { get; }

    public long TotalRequests { get; }
}