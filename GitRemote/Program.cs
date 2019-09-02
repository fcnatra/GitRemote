using GitRemote.Entities;
using GitRemote.Domain;
using System;
using System.Linq;

namespace GitRemote
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ui = new UI() { Arguments = args };
            Parameters parameters = ui.ProcessArguments();
        }
    }
}
