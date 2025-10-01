using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;

namespace lan_scanner.network
{
    public class PingService
    {
        // =====================================================================================================
        /// <summary>
        /// Pings the specified IP address asynchronously to check if the device is reachable.
        /// </summary>
        /// <returns>
        /// True if the device responded successfully; otherwise, false.
        /// </returns>
        public static async Task<bool> PingDeviceAsync(IPAddress ip, int timeout = 500)
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = await ping.SendPingAsync(ip, timeout);
                    return (reply.Status == IPStatus.Success);
                }
            }
            catch { return false; }
        }
        // =====================================================================================================
        public static async Task<Dictionary<IPAddress, bool>> PingDevicesAsync(
            IEnumerable<IPAddress> ips, int timeout = 500, int maxConcurrency = 50
            )
        {
            using var semaphore = new SemaphoreSlim(maxConcurrency, maxConcurrency);

            var tasks = ips.Select(async ip =>
            {
                await semaphore.WaitAsync();
                try
                {
                    Debug.WriteLine($"Ping {ip.ToString()} ...");
                    bool success = await PingDeviceAsync(ip, timeout);
                    return new { Ip = ip, Success = success };
                }
                finally
                {
                    semaphore.Release();
                }
            }).ToArray();

            var results = await Task.WhenAll(tasks);
            return results.ToDictionary(x => x.Ip, x => x.Success);
        }
        // =====================================================================================================
    }
}
