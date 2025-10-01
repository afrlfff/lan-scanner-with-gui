using System.Diagnostics;

namespace lan_scanner.logger
{
    public class FileLogTraceListener : TraceListener
    {
        private readonly StreamWriter _writer;

        // =====================================================================================================
        public FileLogTraceListener(string filePath)
        {
            _writer = new StreamWriter(filePath) { AutoFlush = true };
        }
        // =====================================================================================================
        public override void Write(string? line)
        {
            if (line != null)
            {
                DateTime now = DateTime.Now;
                string logLine = now.ToString("[HH:mm:ss.fff] ") + line;
                _writer.Write(logLine);
            }
        }
        // =====================================================================================================
        public override void WriteLine(string? line)
        {
            if (line != null)
            {
                DateTime now = DateTime.Now;
                string logLine = now.ToString("[HH:mm:ss.fff] ") + line;
                _writer.WriteLine(logLine);
            }
        }
        // =====================================================================================================
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _writer.Dispose();
            }
            base.Dispose(disposing);
        }
        // =====================================================================================================
    }
}
