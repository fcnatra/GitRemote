using GitRemote.Entities;
using GitRemote.Services;

namespace GitRemote.Domain.Operations
{
    public interface IGitOperation
    {
        IGitConnector GitConnector { get; set; }
        Parameters OperationParameters { get; set; }
        void Run();
        void PrintResultsToTraceListeners();
    }
}