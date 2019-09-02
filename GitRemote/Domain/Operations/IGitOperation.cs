using GitRemote.Entities;

namespace GitRemote.Domain.Operations
{
    public interface IGitOperation
    {
        Parameters OperationParameters { get; set; }

        void Run();
    }
}