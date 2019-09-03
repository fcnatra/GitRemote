using GitRemote.Domain.Operations;
using GitRemote.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitRemote.Domain
{
    public class GitClient
    {
        public IGitConnector GitConnector { get; set; }
        private IOperationFactory _operationFactory;
        public IOperationFactory OperationFactory {
            get
            {
                if (_operationFactory is null)
                    _operationFactory = new OperationFactory();
                return _operationFactory;
            }
            set
            {
                _operationFactory = value;
            }
        }
        public Entities.Parameters OperationParameters { get; set; }

        public IGitOperation Run(OperationType operationType)
        {
            IGitOperation operation = OperationFactory.CreateOperation(operationType);

            operation.GitConnector = GitConnector;
            operation.OperationParameters = OperationParameters;
            operation?.Run();
            
            //operation?.PrintResultsToTraceListeners();

            return operation;
        }
    }
}
