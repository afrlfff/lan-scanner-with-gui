using lan_scanner.network;
using lan_scanner.core;
using lan_scanner.gui.other;
using lan_scanner.logger;
using System.Diagnostics;

namespace lan_scanner.gui.windows
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // initialize appliation
            ApplicationConfiguration.Initialize();

            // initialize logger
            //DateTime now = DateTime.Now;
            //string dateTimeString = now.ToString("yyyy-MM-dd_HH-mm");
            //Logger.Initialize($"log_{dateTimeString}.txt");
            Logger.Initialize($"log.txt");

            Debug.WriteLine("Логгер запущен.");

            // initialize app core
            AppCore.Instance.Initialize();

            // load mac-to-vendor dictionary
            string jsonPath = Path.Combine(AppCore.Instance.JsonResourcesDir, "mac_vendors.json");
            MacVendorService.Instance.Initialize(jsonPath);

            // run mainForm
            Application.Run(new MainForm());

            // clear resources cache on exit
            ResourceCache.Clear();
        }
    }
}