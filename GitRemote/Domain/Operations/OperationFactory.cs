﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GitRemote.Domain.Operations
{
    public class OperationFactory : IOperationFactory
    {
        public IGitOperation CreateOperation(OperationType operationToCreate)
        {
            IGitOperation operation = null;

            switch (operationToCreate)
            {
                case OperationType.ListProjects:
                    operation = new Operations.ListProjects();
                    break;
                default:
                    throw new NotImplementedException("Operation not implemented");
            }

            return operation;
        }
    }
}
