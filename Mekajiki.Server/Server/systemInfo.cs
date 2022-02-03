using System.Runtime.InteropServices;

namespace Mekajiki.Server.Server;

[StructLayout(LayoutKind.Sequential, Size = 64)]
internal unsafe struct systemInfo
{
    [DllImport("libc")]
    internal static extern systemInfo sysinfo();

    internal long uptime; /* Seconds since boot */
    internal fixed ulong loads[3]; /* 1, 5, and 15 minute load averages */
    internal ulong totalram; /* Total usable main memory size */
    internal ulong freeram; /* Available memory size */
    internal ulong sharedram; /* Amount of shared memory */
    internal ulong bufferram; /* Memory used by buffers */
    internal ulong totalswap; /* Total swap space size */
    internal ulong freeswap; /* Swap space still available */
    internal ushort procs; /* Number of current processes */
    internal ulong totalhigh; /* Total high memory size */
    internal ulong freehigh; /* Available high memory size */
    internal uint mem_unit; /* Memory unit size in bytes */
}