using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace lan_scanner.network
{
    // =========================================================================================================
    
    public class NetworkInterfaceInfo
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public NetworkInterfaceType Type { get; set; }
        public IPAddress? MainInterface { get; set; }
        public IPAddress? SubnetMask { get; set; }
        public IPAddress? DefaultGateway { get; set; }
    }

    // =========================================================================================================

    public static class NetworkInterfaceService
    {
        // =====================================================================================================
        public static List<NetworkInterfaceInfo> GetAllInterfaces(bool onlyActive = false)
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            var networkInterfaceInfos = new List<NetworkInterfaceInfo>(networkInterfaces.Length);

            foreach (var nic in networkInterfaces)
            {
                if (onlyActive && nic.OperationalStatus != OperationalStatus.Up)
                    continue;

                var interfaceInfo = new NetworkInterfaceInfo
                {
                    Name = nic.Name,
                    Description = nic.Description,
                    Type = nic.NetworkInterfaceType,
                    MainInterface = GetPrimaryIPAddress(nic),
                    SubnetMask = GetSubnetMask(nic),
                    DefaultGateway = GetDefaultGateway(nic)
                };

                networkInterfaceInfos.Add(interfaceInfo);
            }

            return networkInterfaceInfos;
        }
        // =====================================================================================================
        public static List<NetworkInterfaceInfo> FilterByLan(List<NetworkInterfaceInfo> interfaces)
        {
            var result = new List<NetworkInterfaceInfo>();

            foreach (var iface in interfaces)
            {
                if (iface.Type != NetworkInterfaceType.Ethernet &&
                    iface.Type != NetworkInterfaceType.Wireless80211)
                    continue;

                if (iface.DefaultGateway == null)
                    continue;

                if (iface.MainInterface == null ||
                    !IsPrivateNetworkAddress(iface.MainInterface))
                    continue;

                result.Add(iface);
            }

            return result;
        }
        // =====================================================================================================
        public static bool IsPrivateNetworkAddress(IPAddress address)
        {
            if (address.AddressFamily != AddressFamily.InterNetwork)
                return false;

            var bytes = address.GetAddressBytes();

            // 10.0.0.0/8
            if (bytes[0] == 10)
                return true;

            // 172.16.0.0/12
            if (bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31)
                return true;

            // 192.168.0.0/16
            if (bytes[0] == 192 && bytes[1] == 168)
                return true;

            return false;
        }
        // =====================================================================================================
        public static IPAddress? GetPrimaryIPAddress(NetworkInterface networkInterface)
        {
            var ipProperties = networkInterface.GetIPProperties();
            foreach (var ip in ipProperties.UnicastAddresses)
            {
                if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.Address;
                }
            }
            return null;
        }
        // =====================================================================================================
        public static IPAddress? GetSubnetMask(NetworkInterface networkInterface)
        {
            var ipProperties = networkInterface.GetIPProperties();
            foreach (var ip in ipProperties.UnicastAddresses)
            {
                if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.IPv4Mask;
                }
            }
            return null;
        }
        // =====================================================================================================
        public static IPAddress? GetDefaultGateway(NetworkInterface networkInterface)
        {
            var ipProperties = networkInterface.GetIPProperties();

            if (ipProperties.GatewayAddresses.Count > 0)
            {
                var gateway = ipProperties.GatewayAddresses[0].Address;

                if (!IPAddress.IsLoopback(gateway))
                    return gateway;
            }

            return null;
        }
        // =====================================================================================================
    }
}
