namespace GitRemote.Domain.Operations
{
    public interface IOperationFactory
    {
        IGitOperation CreateOperation(OperationType operationToCreate);
    }
}