using System.Net;

namespace lan_scanner.utils
{
    public static class NetworkUtils
    {
        // =====================================================================================================
        /// <summary>
        /// Computes the network address from a host IP and subnet mask using bitwise AND.
        /// </summary>
        /// <returns>True if the operation succeeded; otherwise, false (e.g., invalid mask or non-IPv4).</returns>
        public static bool TryGetNetworkAddress(IPAddress hostIp, IPAddress subnetMask, out IPAddress? networkIp)
        {
            if (!NetworkUtils.IsValidSubnetMask(subnetMask))
            {
                networkIp = null;
                return false;
            }

            byte[] hostIpBytes = hostIp.GetAddressBytes();
            byte[] maskBytes = subnetMask.GetAddressBytes();
            byte[] networkBytes = new byte[hostIpBytes.Length];

            for (int i = 0; i < networkBytes.Length; ++i)
            {
                networkBytes[i] = (byte)(hostIpBytes[i] & maskBytes[i]);
            }

            networkIp = new IPAddress(networkBytes);
            return true;
        }
        // =====================================================================================================
        /// <summary>
        /// Calculates the next IP address by incrementing the specified IP by a given number of steps.
        /// </summary>
        /// <returns>
        /// The resulting IP address after incrementing.
        /// </returns>
        public static IPAddress GetNextIp(IPAddress startIp, int step = 1)
        {
            if (step < 1)
                throw new ArgumentException("Step parameter should be positive");

            byte[] ipBytes = startIp.GetAddressBytes();

            int index = 3;
            while (step > 0 && index > -1)
            {
                if (ipBytes[index] + step <= 255)
                {
                    ipBytes[index] += (byte)step;
                    return new IPAddress(ipBytes);
                }
                else
                {
                    step -= (255 - ipBytes[index]);
                    ipBytes[index--] = 0;
                }
            }

            return new IPAddress(ipBytes);
        }
        // =====================================================================================================
        /// <summary>
        /// Calculates the prefix length (CIDR notation) of a subnet mask.
        /// </summary>
        /// <returns>
        /// The number of contiguous set bits in the subnet mask, representing the network prefix length.
        /// </returns>
        public static int GetMaskPrefixLength(IPAddress mask)
        {
            byte[] maskBytes = mask.GetAddressBytes();
            int counter = 0;
            for (int i = 0; i < maskBytes.Length; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    if ((maskBytes[i] >> 7 - j & 1) == 1) ++counter;
                    else return counter;
                }
            }
            return counter;
        }
        // =====================================================================================================
        /// <summary>
        /// Checks if the specified IP address is a valid subnet mask.
        /// </summary>
        /// <returns>
        /// True if the mask has contiguous leading 1s followed by 0s; otherwise, false.
        /// </returns>
        public static bool IsValidSubnetMask(IPAddress subnetMask)
        {
            byte[] bytes = subnetMask.GetAddressBytes();
            bool foundZero = false;

            for (int i = 0; i < bytes.Length; ++i)
            {
                byte b = bytes[i];
                for (int j = 7; j >= 0; --j)
                {
                    if ((b >> j & 1) == 1)
                    {
                        if (foundZero)
                            return false; // "1" after "0"
                    }
                    else
                    {
                        foundZero = true;
                    }
                }
            }

            return true;
        }
        // =====================================================================================================
        /// <summary>
        /// Gets the total number of IP addresses in a subnet defined by network address and subnet mask.
        /// </summary>
        /// <returns>
        /// The count of IPs in the range, or -1 if the mask is invalid.
        /// </returns>
        public static long GetHostsNumber(IPAddress networkIp, IPAddress subnetMask)
        {
            if (!NetworkUtils.IsValidSubnetMask(subnetMask))
                return -1L;
            else
                return GetHostsNumber(networkIp, GetMaskPrefixLength(subnetMask));
        }
        // =====================================================================================================
        /// <summary>
        /// Gets the total number of IP addresses in a subnet based on prefix length.
        /// </summary>
        /// <returns>
        /// 2^(32 - prefixLength), or -1 if the prefix length is out of valid range.
        /// </returns>
        public static long GetHostsNumber(IPAddress networkIp, int maskPrefixLength)
        {
            if (maskPrefixLength < 0 || maskPrefixLength > 32) return -1;

            return (1L << (32 - maskPrefixLength)) - 1L; // exclude network IP
        }
        // =====================================================================================================
        /// <summary>
        /// Enumerates all IP addresses in a subnet starting from the first host after the network address.
        /// </summary>
        /// <returns>
        /// A lazy sequence of IPs; empty if the mask is invalid.
        /// </returns>
        public static IEnumerable<IPAddress> EnumerateIps(IPAddress networkIp, IPAddress subnetMask, bool excludeBroadcast = true)
        {
            if (!NetworkUtils.IsValidSubnetMask(subnetMask))
                return Enumerable.Empty<IPAddress>();
            else
                return EnumerateIps(networkIp, NetworkUtils.GetMaskPrefixLength(subnetMask), excludeBroadcast);
        }
        // =====================================================================================================
        /// <summary>
        /// Enumerates all IP addresses in a subnet using a given prefix length.
        /// </summary>
        /// <returns>
        /// A lazy sequence of IPs starting from networkIp + 1; empty if prefix length is invalid.
        /// </returns>
        public static IEnumerable<IPAddress> EnumerateIps(IPAddress networkIp, int maskPrefixLength, bool excludeBroadcast = true)
        {
            if (maskPrefixLength > 32 || maskPrefixLength < 0)
                yield break;

            long totalHosts = NetworkUtils.GetHostsNumber(networkIp, maskPrefixLength);
            IPAddress currentIp = NetworkUtils.GetNextIp(networkIp);

            for (long i = 0; i < ((excludeBroadcast) ? (totalHosts - 1) : (totalHosts)); ++i)
            {
                yield return currentIp;
                currentIp = NetworkUtils.GetNextIp(currentIp);
            }
        }
        // =====================================================================================================
        /// <summary>
        /// Нормализует строку MAC-адреса: удаляет разделители, приводит к верхнему регистру и объединяет байты через ':'.
        /// Поддерживает распространённые форматы (с :, -, пробелами).
        /// </summary>
        /// <returns>Нормализованный MAC-адрес в формате XX:XX:XX:XX:XX:XX, или null, если вход некорректен.</returns>
        public static string? NormalizeMacAddress(string? macAddress)
        {
            if (string.IsNullOrWhiteSpace(macAddress))
                return null;

            // Удаляем все не-шестнадцатеричные символы
            var clean = new string(macAddress.Where(char.IsLetterOrDigit).ToArray());

            // Должно быть ровно 12 шестнадцатеричных символов (6 байт)
            if (clean.Length != 12 || !clean.All(c => Uri.IsHexDigit(c)))
                return null;

            // Разбиваем по два символа и соединяем через ':'
            return string.Join(":",
                Enumerable.Range(0, 6)
                          .Select(i => clean.Substring(i * 2, 2).ToUpper()));
        }
        // =====================================================================================================
    }
}
