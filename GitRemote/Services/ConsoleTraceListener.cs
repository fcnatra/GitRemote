﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GitRemote.Services
{
    internal class ConsoleTraceListener : TraceListener
    {
        public override void Write(string message) { Console.Write(message); }
        public override void WriteLine(string message) { Console.WriteLine(message); }
    }
}
