using GitRemote.Entities;
using System.Collections.Generic;

namespace GitRemote.Domain.Operations
{
    public interface IListOperation<T>
    {
        IEnumerable<T> Result { get; }
    }
}