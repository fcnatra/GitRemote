using GitRemote.Domain.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitRemote.Domain
{
    public class GitToolsClient
    {
        public Entities.Parameters OperationParameters { get; set; }

        public void Run(Operation expectedOperation)
        {
            IGitOperation operation = null;

            switch (expectedOperation)
            {
                case Operation.ListProjects:
                    operation = new Operations.ListProjects() { OperationParameters = OperationParameters };
                    break;
                default:
                    throw new NotImplementedException("Operation not implemented");
            }

            operation?.Run();
        }
    }
}
