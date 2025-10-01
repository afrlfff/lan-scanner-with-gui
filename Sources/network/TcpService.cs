/*
 * Unused class. Not finished.
 * Was develpoing for parsing TCP data (for analyse device type in future)
*/

using System.Net;

namespace lan_scanner.network
{
    [Flags]
    public enum TcpFlags : byte
    {
        FIN = 0x01,  // 00000001 // No more data from sender
        SYN = 0x02,  // 00000010 // Synchronize sequence numbers
        RST = 0x04,  // 00000100 // Reset the connection
        PSH = 0x08,  // 00001000 // Push Function
        ACK = 0x10,  // 00010000 // Acknowledgment field significant
        URG = 0x20,  // 00100000 // Urgent Pointer field significant
        ECE = 0x40,  // 01000000 // ECN-Echo
        CWR = 0x80   // 10000000 // Congestion Window Reduced
    }
    // =========================================================================================================
    public enum TcpOptionKind : byte
    {
        EOL = 0,
        NOP = 1,
        MSS = 2,
        WindowScale = 3,
        SackPermitted = 4,
        Timestamps = 8,
        Unknown = 255  // Для неизвестных опций
    }
    // =========================================================================================================
    public class TcpOption
    {
        public TcpOptionKind Kind { get; }
        public byte[] Data { get; }  // сырые данные

        public TcpOption(TcpOptionKind kind, byte[] data)
        {
            Kind = kind;
            Data = data ?? new byte[0];
        }

        /// <summary>
        /// Длина всей опции в байтах (включая Type и Length, если есть)
        /// </summary>
        public int TotalLength => Data.Length + (Kind == TcpOptionKind.NOP || Kind == TcpOptionKind.EOL ? 1 : 2);

        /// <summary>
        /// Попытка получить значение MSS (Maximum Segment Size).
        /// </summary>
        /// <param name="option">TCP-опция</param>
        /// <param name="mss">Значение MSS, если удалось извлечь</param>
        /// <returns>True, если опция — MSS и данные корректны</returns>
        public static bool TryGetMss(TcpOption option, out ushort mss)
        {
            if (option.Kind == TcpOptionKind.MSS && option.Data.Length == 2)
            {
                mss = (ushort)((option.Data[0] << 8) | option.Data[1]);
                return true;
            }
            mss = 0;
            return false;
        }

        /// <summary>
        /// Попытка получить значение Window Scale.
        /// </summary>
        /// <param name="option">TCP-опция</param>
        /// <param name="scale">Значение шкалы окна (0–14)</param>
        /// <returns>True, если опция — Window Scale и данные корректны</returns>
        public static bool TryGetWindowScale(TcpOption option, out byte scale)
        {
            if (option.Kind == TcpOptionKind.WindowScale && option.Data.Length == 1)
            {
                scale = option.Data[0];
                return true;
            }
            scale = 0;
            return false;
        }

        /// <summary>
        /// Попытка получить значения временных меток (TSval и TSecr).
        /// </summary>
        /// <param name="option">TCP-опция</param>
        /// <param name="tsVal">Timestamp Value</param>
        /// <param name="tsEcr">Timestamp Echo Reply</param>
        /// <returns>True, если опция — Timestamps и данные корректны</returns>
        public static bool TryGetTimestamps(TcpOption option, out uint tsVal, out uint tsEcr)
        {
            if (option.Kind == TcpOptionKind.Timestamps && option.Data.Length == 8)
            {
                tsVal = (uint)((option.Data[0] << 24) | (option.Data[1] << 16) | (option.Data[2] << 8) | option.Data[3]);
                tsEcr = (uint)((option.Data[4] << 24) | (option.Data[5] << 16) | (option.Data[6] << 8) | option.Data[7]);
                return true;
            }
            tsVal = 0;
            tsEcr = 0;
            return false;
        }
    }


    // =========================================================================================================
    public static class TcpService
    {
        // =====================================================================================================
        private static byte[] TcpOptionToBytes(TcpOption option)
        {
            var type = (byte)option.Kind;

            // Однобайтовые опции
            if (option.Kind == TcpOptionKind.EOL || option.Kind == TcpOptionKind.NOP)
                return new byte[] { (byte)option.Kind };

            // Двухбайтовые или с данными
            return option.Kind switch
            {
                TcpOptionKind.MSS => option.Data.Length == 2
                    ? new byte[] { 0x02, 0x04, option.Data[0], option.Data[1] }
                    : throw new ArgumentException("MSS option must have exactly 2 bytes of data."),

                TcpOptionKind.WindowScale => option.Data.Length == 1
                    ? new byte[] { 0x03, 0x03, option.Data[0] }
                    : throw new ArgumentException("Window Scale option must have exactly 1 byte of data."),

                TcpOptionKind.SackPermitted => new byte[] { 0x04, 0x02 },

                // Timestamps: Type=8, Length=10, Data=8 байт (TSval + TSecr)
                TcpOptionKind.Timestamps when option.Data.Length == 8 =>
                    new byte[]
                    {
                        0x08,                           // Type
                        0x0A,                           // Length = 10
                        option.Data[0], option.Data[1],
                        option.Data[2], option.Data[3], // TSval
                        option.Data[4], option.Data[5],
                        option.Data[6], option.Data[7]  // TSecr
                    },

                TcpOptionKind.Timestamps =>
                    throw new ArgumentException("Timestamps option must have exactly 8 bytes of data (TSval and TSecr)."),

                _ => throw new NotSupportedException($"Unsupported TCP option: {option.Kind}")
            };
        }
        // =====================================================================================================
        public static byte[] BuildTcpHeader(
            int srcPort, int destPort, int seqNum, int askNum, byte flags, int windowSize, TcpOption[] options
            )
        {
            byte[] header = new byte[28];

            byte[] srcPortBytes = BitConverter.GetBytes(
                (ushort)IPAddress.HostToNetworkOrder((short)srcPort)
                );

            byte[] destPortBytes = BitConverter.GetBytes(
                (ushort)IPAddress.HostToNetworkOrder((short)destPort)
                );

            byte[] seqNumBytes = BitConverter.GetBytes(
                (uint)IPAddress.HostToNetworkOrder(seqNum)
                );

            byte[] askNumBytes = BitConverter.GetBytes(
                (uint)IPAddress.HostToNetworkOrder(askNum)
                );

            // Source port
            Array.Copy(srcPortBytes, 0, header, 0, 2);

            // Destination port
            Array.Copy(destPortBytes, 0, header, 2, 2);

            // Sequence number
            Array.Copy(seqNumBytes, 0, header, 4, 4);

            // Acknowledgment Number
            Array.Copy(askNumBytes, 0, header, 8, 4);

            // Data Offset (4 бита, мин = 5, макс = 15) + Reserved (4 бит, всегда 0)
            int dataOffset = CalculateDataOffset(options);
            header[12] = (byte)((dataOffset & 0x0F) << 4);

            // Flags
            header[13] = flags;

            // Window Size
            byte[] windowSizeBytes = BitConverter.GetBytes(
                (ushort)IPAddress.HostToNetworkOrder((short)windowSize)
                );
            Array.Copy(windowSizeBytes, 0, header, 14, 2);

            // Checksum (сначала 0)
            header[16] = 0;
            header[17] = 0;

            // Urgent Pointer
            Array.Clear(header, 18, 2);

            // NOTE: temporary finish
            return header;

            // ===== TCP Options (начиная с 20 байта) =====
            //PadTcpOptions(options);
            
            //int tcpHeaderLength = 20;
            //foreach ( var option in options )
            //{
            //    Array.Copy(option.Data, 0, header, tcpHeaderLength, option.TotalLength);
            //    tcpHeaderLength += option.TotalLength;
            //}

            //int tcpHeaderLen = 20 + optionsLen;

            //// Пересоздадим массив нужной длины
            //byte[] finalHeader = new byte[tcpHeaderLen];
            //Array.Copy(header, finalHeader, tcpHeaderLen);

            //// Теперь вычислим TCP checksum
            //// Он включает Pseudo Header + TCP Header + Data (у нас нет данных)
            //byte[] pseudoHeader = BuildPseudoHeader(srcIp, destIp, (ushort)tcpHeaderLen);
            //byte[] checksumInput = Combine(pseudoHeader, finalHeader);

            //// Check sum
            //ushort checksum = CalculateChecksum(checksumInput, 0, checksumInput.Length);
            //byte[] checkSumBytes = BitConverter.GetBytes(
            //    (ushort)IPAddress.HostToNetworkOrder(checksum)
            //    );
            //Array.Copy(checkSumBytes, 0, finalHeader, 16, 2);

            //// return result
            //return finalHeader;
        }
        // =====================================================================================================
        private static int CalculateDataOffset(TcpOption[] options)
        {
            int optionsTotalLegnth = 0;
            foreach (var option in options)
                optionsTotalLegnth += option.TotalLength;

            // pad the length to be multiple of 4 bytes
            int paddedLength = (optionsTotalLegnth + 3) & ~3;  // Округление вверх до кратного 4

            // 20 - размер постоянных данных в TCP-заголовке
            int headerLength = 20 + paddedLength;

            return headerLength / 4;
        }
        // =====================================================================================================
        private static void PadTcpOptions(TcpOption[] options)
        {
            if (options == null || options.Length == 0)
                return;

            int totalLength = 0;
            foreach (var option in options)
                totalLength += option.TotalLength;

            int paddingNeeded = (4 - (totalLength % 4)) % 4;

            if (paddingNeeded == 0)
                return;

            // Создаём новый массив с NOP
            var result = new TcpOption[options.Length + paddingNeeded];
            Array.Copy(options, result, options.Length);

            for (int i = 0; i < paddingNeeded; ++i)
            {
                result[options.Length + i] = new TcpOption(TcpOptionKind.NOP, Array.Empty<byte>());
            }

            options = result;
        }
        // =====================================================================================================
    }
}
