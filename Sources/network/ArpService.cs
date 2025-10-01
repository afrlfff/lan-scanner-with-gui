using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace lan_scanner.network
{
    // =========================================================================================================

    public record ArpEntry(IPAddress InterfaceIp, IPAddress Ip, string Mac, string Type);

    // =========================================================================================================

    public static class ArpService
    {
        // =====================================================================================================

        // import SendARP function
        [System.Runtime.InteropServices.DllImport("iphlpapi.dll", ExactSpelling = true)]
        private static extern int SendARP(int destIp, int srcIp, byte[] macAddr, ref uint physAddrLen);

        // =====================================================================================================

        static readonly Regex Line = new(@"^\s*(\d+\.\d+\.\d+\.\d+)\s+([0-9a-fA-F\-]{17})\s+(\S+)", RegexOptions.Compiled);

        // =====================================================================================================

        public static async Task<string?> TryGetMacAddress(IPAddress targetIp)
        {
            return await Task.Run(() =>
            {
                try
                {
                    byte[] ipBytes = targetIp.GetAddressBytes();
                    uint destIp = (uint)(ipBytes[0] | (ipBytes[1] << 8) | (ipBytes[2] << 16) | (ipBytes[3] << 24));
                    byte[] macBytes = new byte[6];
                    uint macLen = 6;

                    int result = SendARP((int)destIp, 0, macBytes, ref macLen);

                    if (result == 0 && macLen == 6)
                    {
                        string mac = BitConverter.ToString(macBytes, 0, (int)macLen);
                        return mac; // Формат: "aa-bb-cc-dd-ee-ff"
                    }
                }
                catch
                {

                }

                return null;
            }).ConfigureAwait(false);
        }

        // =====================================================================================================

        public static async Task<Dictionary<IPAddress, string?>> TryGetMacAddresses(
            List<IPAddress> ipAddresses, 
            int maxConcurrency = 20
            )
        {
            using var semaphore = new SemaphoreSlim(maxConcurrency, maxConcurrency);

            var tasks = ipAddresses.Select(async ip =>
            {
                await semaphore.WaitAsync();
                try
                {
                    var macAddress = await TryGetMacAddress(ip);
                    Debug.WriteLine($"Получен MAC-адрес: {ip.ToString()} -> {macAddress}");
                    return new { Ip = ip, Mac = macAddress };
                }
                finally
                {
                    semaphore.Release();
                }
            }).ToArray();

            var results = await Task.WhenAll(tasks);

            return results.ToDictionary(x => x.Ip, x => x.Mac);
        }

        // =====================================================================================================

        private static string ResolveArpPath()
        {
            var systemPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
            var arpPath = Path.Combine(systemPath, "arp.exe");

            return File.Exists(arpPath) ? arpPath : "arp";
        }

        // =====================================================================================================

        public static bool TryReadArpTable(IPAddress interfaceIp, out List<ArpEntry> result)
        {
            result = new List<ArpEntry>();
            Process p;

            try
            {
                p = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = ResolveArpPath(),
                        Arguments = $"-a -N {interfaceIp.ToString()}",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };
            }
            catch (Win32Exception ex) when (ex.NativeErrorCode == 2) // 2 = File not found
            {
                // "ARP utility not found in system PATH"
                return false;
            }

            p.Start();
            var output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            foreach (var raw in output.Split('\n'))
            {
                var line = raw.TrimEnd();
                var m = Line.Match(line);
                if (!m.Success) continue;

                var ip = m.Groups[1].Value;
                var mac = m.Groups[2].Value.ToLowerInvariant();

                // drop multicast/broadcast out
                if (ip.StartsWith("224.") || ip.StartsWith("239.") || ip.EndsWith("255")) continue;

                Debug.WriteLine($"Прочитан MAC-адрес: {ip.ToString()} -> {mac}");

                result.Add(new ArpEntry(interfaceIp, IPAddress.Parse(ip), mac, m.Groups[3].Value.ToLowerInvariant()));
            }
            return true;
        }
        // =====================================================================================================
    }
}
