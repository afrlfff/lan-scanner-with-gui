using System.Diagnostics;
using System.Net;

namespace lan_scanner.network
{
    public static class DnsService
    {
        /// <summary>
        /// Attempts to resolve the host name associated with the specified IP address.
        /// </summary>
        /// <returns>
        /// The host name as a string if the DNS lookup succeeds; otherwise, null.
        /// </returns>
        public static async Task<string?> GetHostNameAsync(IPAddress ip)
        {
            string? hostName = await Task.Run(async () =>
            {
                try
                {
                    IPHostEntry hostEntry = await Dns.GetHostEntryAsync(ip.ToString());
                    return hostEntry.HostName;
                }
                catch
                {
                    return null;
                }
            });
            return hostName;
        }
        // =====================================================================================================
        public static async Task<List<string?>> GetHostNamesAsync(
            IEnumerable<IPAddress> ips, int maxConcurrency = 20
            )
        {
            if (ips == null)
                return new List<string?>();

            var hostNames = new List<string?>(ips.Count());

            var semaphore = new SemaphoreSlim(maxConcurrency, maxConcurrency);
            var tasks = new List<Task<string?>>();

            foreach (var ip in ips)
            {
                await semaphore.WaitAsync();

                var task = Task.Run(async () =>
                {
                    try
                    {
                        string? hostName = await GetHostNameAsync(ip);
                        Debug.WriteLine($"Найдено имя хоста: {ip.ToString()} -> {hostName}");
                        return hostName;
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });

                tasks.Add(task);

            }

            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }
        // =====================================================================================================
    }
}
