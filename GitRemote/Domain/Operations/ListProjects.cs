using GitRemote.Entities;
using GitRemote.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitRemote.Domain.Operations
{
    public class ListProjects : IGitOperation, IListOperation<ProjectInformation>
    {
        public Parameters OperationParameters { get; set; }
        public IEnumerable<ProjectInformation> Result { get; set; }
        public IGitConnector GitConnector { get; set; }

        public void Run()
        {
            ValidateParameters();

            GitConnector.GitApiUrl = OperationParameters.GitApiUrl;
            GitConnector.GitToken = OperationParameters.GitToken;

            var groups = GitConnector.GetGroups(OperationParameters.GroupName);
            var projects = GitConnector.GetProjects(groups.First());

            Result = projects;
        }

        private void ValidateParameters()
        {
            MustHaveANotEmptyValue(OperationParameters?.GitApiUrl, nameof(OperationParameters.GitApiUrl));
            MustHaveANotEmptyValue(OperationParameters?.GitToken, nameof(OperationParameters.GitToken));
            MustHaveANotEmptyValue(OperationParameters?.GroupName, nameof(OperationParameters.GroupName));
        }

        private void MustHaveANotEmptyValue(string parameterValue, string parameterName)
        {
            if (string.IsNullOrEmpty(parameterValue))
                throw new ArgumentException($"This argument must have a NOT empty value: {parameterName}");
        }

        public void PrintResultsToTraceListeners()
        {
            foreach (var project in Result)
                System.Diagnostics.Debug.WriteLine(project.WebUrl);
        }
    }
}
