namespace Mekajiki.Types.ServerInfo
{
    /// <summary>
    /// These measurements are made for a Raspberry Pi 4, and may not work on other systems
    /// </summary>
    public interface ITemperatureInfo
    {
        /// <summary>
        /// The CPU temperature, in Celsius degrees
        /// </summary>
        public double CpuTemperature { get; }
    }
}