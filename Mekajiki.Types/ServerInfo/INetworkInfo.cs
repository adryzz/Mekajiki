namespace Mekajiki.Types.ServerInfo
{
    public interface INetworkInfo
    {
        public long SentBytes { get; }
        public long ReceivedBytes { get; }
        
        public long TotalRequests { get; }
    }
}