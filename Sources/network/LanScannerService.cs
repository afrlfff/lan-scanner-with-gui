using System.Diagnostics;
using System.Net;
using lan_scanner.utils;

namespace lan_scanner.network
{
    public static class LanScannerService
    {
        // =====================================================================================================
        public static async Task<Dictionary<IPAddress, DeviceInfo>> ScanSubnet(
            IPAddress networkIp, 
            IPAddress subnetMask, 
            IPAddress? interfaceIp = null)
        {
            Debug.WriteLine($"Поиск активных хостов в локальной сети ... Адрес:  {networkIp.ToString()}; Маска: {subnetMask.ToString()}");

            // ips to scan
            var ips = NetworkUtils.EnumerateIps(networkIp, subnetMask, excludeBroadcast: true);

            // scan IP-addresses
            var result =  await ScanIps(ips, interfaceIp: interfaceIp);

            Debug.WriteLine($"Поиск активных хостов в локальной сети завершен");

            return result;
        }
        // =====================================================================================================
        private static async Task ScanIp(IPAddress ip, DeviceInfo deviceInfo)
        {
            // Arp
            Debug.WriteLine($"Arp {ip.ToString()} ...");
            string? mac = await ArpService.TryGetMacAddress(ip);
            Debug.WriteLine($"Arp {ip.ToString()} усепешно");

            // Ping
            Debug.WriteLine($"Ping {ip.ToString()} ...");
            bool pingResult = await PingService.PingDeviceAsync(ip, timeout: 500);
            Debug.WriteLine($"Ping {ip.ToString()} усепешно");
        }
        // =====================================================================================================
        public static async Task<Dictionary<IPAddress, DeviceInfo>> ScanIps(
            IEnumerable<IPAddress> ips, IPAddress? interfaceIp = null, int threadLimit = 10)
        {
            // for debug
            var stopwatch = Stopwatch.StartNew();

            // ===== Initialization ===== //
            var deviceDict = new Dictionary<IPAddress, DeviceInfo>();
            foreach (IPAddress ip in ips)
            {
                deviceDict[ip] = new DeviceInfo(ip);
            }

            // ===== Ping ===== //
            var pingResults = await PingService.PingDevicesAsync(ips, timeout: 500, maxConcurrency: 50);
            foreach (var pingResult in pingResults)
            {
                deviceDict[pingResult.Key].PingSuccess = pingResult.Value;
            }

            // ===== read ARP table ===== //
            if (interfaceIp != null && ArpService.TryReadArpTable(interfaceIp, out List<ArpEntry> result))
            {
                // read full ARP table

                foreach (ArpEntry arpEntry in result)
                {
                    if (deviceDict.ContainsKey(arpEntry.Ip))
                    {
                        deviceDict[arpEntry.Ip].MacAddress = arpEntry.Mac;
                    }
                }
            }
            else
            {
                // read only IPs which ping was successfull

                foreach (var pingResult in pingResults)
                {
                    if (pingResult.Value == true)
                    {
                        // mac address is now on the ARP table
                        string? mac = await ArpService.TryGetMacAddress(pingResult.Key);
                        deviceDict[pingResult.Key].MacAddress = mac;
                    }
                }
            }

            // get all unreached IPs
            List<IPAddress> unreachedIps = new List<IPAddress>();
            foreach (IPAddress ip in ips)
            {
                if (deviceDict[ip].MacAddress == null)
                    unreachedIps.Add(ip);
            }

            // ===== ARP-requests ===== //
            var arpResults = await ArpService.TryGetMacAddresses(unreachedIps);
            foreach (var arpResult in arpResults)
            {
                // arpResult.Value store either MAC-address or null
                deviceDict[arpResult.Key].MacAddress = arpResult.Value;
            }

            // ===== Filter active hosts ===== //
            var filteredDeviceDict = FilterActiveHosts(deviceDict);


            stopwatch.Stop();
            Debug.WriteLine($"Общее время поиска активных хостов: {stopwatch.ElapsedMilliseconds} ms");

            return filteredDeviceDict;
        }
        // =====================================================================================================
        private static Dictionary<IPAddress, DeviceInfo> FilterActiveHosts(Dictionary<IPAddress, DeviceInfo> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return source.Where(kvp => kvp.Value.MacAddress != null)
                         .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
        // =====================================================================================================
    }
}
