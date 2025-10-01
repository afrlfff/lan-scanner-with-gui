using System.Diagnostics;

namespace lan_scanner.logger
{
    public static class Logger
    {
        // =====================================================================================================
        public static void Initialize(string fileName)
        {
            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            string logPath = Path.Combine(appDir, $"{fileName}");

            //Trace.Listeners.Clear();
            Trace.Listeners.Add(new FileLogTraceListener(logPath));
        }
        // =====================================================================================================
    }
}
