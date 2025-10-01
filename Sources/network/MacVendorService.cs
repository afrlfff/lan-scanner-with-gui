using System.Diagnostics;
using System.Text.Json;

namespace lan_scanner.network
{
    public sealed class MacVendorService
    {
        private static readonly MacVendorService _instance = new MacVendorService();
        public static MacVendorService Instance => _instance;

        private Dictionary<string, string>? _macToVendor = new();

        // =====================================================================================================
        public MacVendorService() { }
        // =====================================================================================================
        public void Initialize(string jsonPath)
        {
            if (string.IsNullOrEmpty(jsonPath))
                throw new ArgumentException($"'{nameof(jsonPath)}' cannot be null or empty.", nameof(jsonPath));

            // read data from json
            try
            {
                string jsonString = File.ReadAllText(jsonPath);
                _macToVendor = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Ошибка при чтении json файла: {jsonPath}");
                _macToVendor = null;
            }
        }
        // =====================================================================================================
        public string? GetVendor(string normalizedMacAddress)
        {
            if (_macToVendor != null)
            {
                string mac3bytes = string.Join("", normalizedMacAddress.Split(":").Take(3)).ToUpper();

                if (_macToVendor.ContainsKey(mac3bytes) )
                    return _macToVendor[mac3bytes];
            }
            return null;
        }
        // =====================================================================================================
    }
}
