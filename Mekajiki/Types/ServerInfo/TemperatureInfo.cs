using System.IO;

namespace Mekajiki.Types.ServerInfo
{
    public class TemperatureInfo : ITemperatureInfo
    {
        public double CpuTemperature { get; }

        public TemperatureInfo()
        {
            string temp = File.ReadAllText("/sys/class/thermal/thermal_zone0/temp");
            CpuTemperature = double.Parse(temp) / 1000d;
        }
    }
}