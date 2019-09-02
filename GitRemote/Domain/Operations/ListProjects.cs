using GitRemote.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitRemote.Domain.Operations
{
    public class ListProjects : IGitOperation, IListOperation<ProjectInformation>
    {
        public Entities.Parameters OperationParameters { get; set; }

        public IEnumerable<ProjectInformation> Result => new List<ProjectInformation>();

        public void Run()
        {
            ValidateParameters();
        }

        private void ValidateParameters()
        {
            MustHaveANotEmptyValue(OperationParameters?.GitApiUrl);
            MustHaveANotEmptyValue(OperationParameters?.GitToken);
            MustHaveANotEmptyValue(OperationParameters?.GroupName);
        }

        private void MustHaveANotEmptyValue(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new ArgumentException($"This argument must have a NOT empty value: {parameter}");
        }
    }
}
