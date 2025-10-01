/*
 * Unused class. Not finished.
 * Was developed as a part of manual Ping implementation
 * (using raw sockets)
*/

namespace lan_scanner.network
{
    // =========================================================================================================

    public enum IcmpMessage : ushort
    {
        EchoReply = 0x0000,

        DestinationNetworkUnreachable = 0x0300,
        DestinationHostUnreachable = 0x0301,
        DestinationProtocolUnreachable = 0x0302,
        DestinationPortUnreachable = 0x0303,
        FragmentationNeededAndDfFlagSet = 0x0304,
        SourceRouteFailed = 0x0305,

        RedirectDatagramForTheNetwork = 0x0500,
        RedirectDatagramForTheHost = 0x0501,
        RedirectDatagramForTheTypeOfServiceAndNetwork = 0x0502,
        RedirectDatagramForTheServiceAndHost = 0x0503,

        EchoRequest = 0x0800,

        RouterAdvertisement = 0x0900,
        RouterSolicitation = 0x0A00,

        TimeToLiveExceededInTransit = 0x0B00,
        FragmentReassemblyTimeExceeded = 0x0B01,

        PointerIndicatesError = 0x0C00,
        MissingRequiredOption = 0x0C01,
        BadLength = 0x0C02,

        Timestamp = 0x0D00,
        TimestampReply = 0x0E00,
    }

    // =========================================================================================================

    public static class IcmpService
    {
        public static byte[] CreateIcmpPacket(IcmpMessage message, ushort id, ushort sequence, byte[] payload)
        {
            byte[] packet = new byte[8 + payload.Length]; // 8 байт — заголовок ICMP

            byte type = (byte)(((ushort)message) >> 8);
            byte code = (byte)(((ushort)message) & 0xFF);

            // Заголовок
            packet[0] = type;                       // Type
            packet[1] = code;                       // Code
            packet[2] = 0;                          // Checksum (старший байт)
            packet[3] = 0;                          // Checksum (младший байт)
            packet[4] = (byte)(id >> 8);            // ID (старший байт)
            packet[5] = (byte)(id & 0xFF);          // ID (младший)
            packet[6] = (byte)(sequence >> 8);      // Sequence (старший)
            packet[7] = (byte)(sequence & 0xFF);    // Sequence (младший)

            // Данные
            Buffer.BlockCopy(payload, 0, packet, 8, payload.Length);

            // Вычисляем checksum
            ushort checksum = CalculateChecksum(packet);
            packet[2] = (byte)(checksum >> 8);
            packet[3] = (byte)(checksum & 0xFF);

            return packet;
        }

        // =====================================================================================================

        private static ushort CalculateChecksum(ReadOnlySpan<byte> data)
        {
            uint sum = 0;
            int i = 0;

            while (i < data.Length - 1)
            {
                sum += (uint)(data[i] << 8 | data[i + 1]);
                i += 2;
            }

            if (i < data.Length)
            {
                sum += (uint)(data[i] << 8);
            }

            while (sum > 0xFFFF)
            {
                sum = (sum & 0xFFFF) + (sum >> 16);
            }

            return (ushort)~sum;
        }

        // =====================================================================================================

        private static bool TryParseIcmpReply(ReadOnlySpan<byte> buffer, ushort expectedId, ushort expectedSeq, out byte type, out int dataLength)
        {
            type = 0;
            dataLength = 0;

            if (buffer.Length < 8) return false;

            type = buffer[0];
            // ushort code = buffer[1]; // обычно 0
            // ushort checksum = (ushort)((buffer[2] << 8) | buffer[3]);
            ushort id = (ushort)((buffer[4] << 8) | buffer[5]);
            ushort seq = (ushort)((buffer[6] << 8) | buffer[7]);

            if (id != expectedId || seq != expectedSeq)
                return false;

            dataLength = buffer.Length - 8;
            return true;
        }

        // =====================================================================================================
    }
}
