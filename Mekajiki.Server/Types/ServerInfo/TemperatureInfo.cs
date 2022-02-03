using Mekajiki.Types.ServerInfo;

namespace Mekajiki.Server.Types.ServerInfo;

public class TemperatureInfo : ITemperatureInfo
{
    public TemperatureInfo()
    {
        var temp = File.ReadAllText("/sys/class/thermal/thermal_zone0/temp");
        CpuTemperature = double.Parse(temp) / 1000d;
    }

    public double CpuTemperature { get; }
}