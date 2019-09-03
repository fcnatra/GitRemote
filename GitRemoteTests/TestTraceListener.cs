using System.Collections.Generic;
using System.Diagnostics;

namespace GitRemoteTests
{
    internal class TestTraceListener : TraceListener
    {
        public List<string> WritenMessages { get; set; } = new List<string>();
        public override void Write(string message) { this.WritenMessages.Add(message); }
        public override void WriteLine(string message) { this.WritenMessages.Add(message); }
    }
}
