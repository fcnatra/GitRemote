using GitRemote.Entities;
using GitRemote.Domain;
using System;
using System.Linq;
using GitRemote.Domain.Operations;
using GitRemote.Services;

namespace GitRemote
{
    public class Program
    {
        private static UI _ui;
        static void Main(string[] args)
        {
            _ui = new UI() { Arguments = args };

            Parameters parameters = _ui.ProcessArguments();

            if (string.IsNullOrEmpty(parameters.Operation))
            {
                Console.WriteLine("\r\nERROR: No operation has been specified\r\n");
                ShowHelp();
            }
            else
            {
                var operation = (OperationType)Enum.Parse(typeof(OperationType), parameters.Operation);
                var git = new GitClient() { OperationFactory = new OperationFactory() };
                git.OperationParameters = parameters;
                git.GitConnector = new GitLabApiClientConnector();

                try
                {
                    git.Run(operation);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\r\nERROR: ({ex.GetType().ToString()}) {ex.Message}\r\n");
                    ShowHelp();
                }
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Please use: GitRemote [parameters] [operation]\r\n");
            Console.WriteLine("Just one operation will be executed at a time.");
            Console.WriteLine("What follows is a description of the allowed parameters:");
            var validArguments = _ui.ValidArguments;
            foreach (var validArgument in validArguments)
                Console.WriteLine($"\t{validArgument}");
        }
    }
}
