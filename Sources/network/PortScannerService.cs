/*
 * Unused class. 
 * Was developed for analyse device type and OS fingerprinting in the future
*/


using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Net;

namespace lan_scanner.network
{
    public static class PortScanner
    {
        public static async Task<List<int>> Scan(IPAddress ip, int[] ports, int timeout)
        {
            // гse ConcurrentBag to avoid racing when adding found ports.
            var open = new ConcurrentBag<int>();

            // Make individual task for each port.
            var tasks = ports.Select(async p =>
            {
                using var cts = new CancellationTokenSource(timeout);
                try
                {
                    using var client = new TcpClient();
                    var connectTask = client.ConnectAsync(ip, p);

                    // Wait for connection or timeout
                    var completed = await Task.WhenAny(connectTask, Task.Delay(timeout, cts.Token));
                    if (completed == connectTask && client.Connected)
                        open.Add(p);
                }
                catch
                {
                    // port is closed
                }
            });

            await Task.WhenAll(tasks);
            return open.ToList();
        }
    }
}
