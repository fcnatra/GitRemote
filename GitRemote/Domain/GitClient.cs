using GitRemote.Domain.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitRemote.Domain
{
    public class GitClient
    {
        public IOperationFactory OperationFactory { get; set; }

        public Entities.Parameters OperationParameters { get; set; }

        public void Run(Operation expectedOperation)
        {
            var operationFactory = new OperationFactory();
            IGitOperation operation = operationFactory.CreateOperation(expectedOperation);
            operation.OperationParameters = OperationParameters;
            operation?.Run();
        }
    }
}
