
using System.Net;

namespace lan_scanner.network
{
    // ========================================================================================================

    public class DeviceInfo
    {
        public IPAddress? Ip { get; set; }
        public string? MacAddress { get; set; }
        public bool PingSuccess { get; set; }
        public string? MacVendor {  get; set; }
        public string? HostName { get; set; }
        public List<int> OpenPorts { get; set; }
        public int TTL {  get; set; }

        public DeviceInfo()
        {
            this.Ip = null;
            this.MacAddress = null;
            this.MacVendor = null;
            this.HostName = null;
            this.PingSuccess = false;
            this.OpenPorts = new List<int>();
        }

        public DeviceInfo(IPAddress ip) : this()
        {
            this.Ip = ip;
        }
    }
    // ========================================================================================================
}
