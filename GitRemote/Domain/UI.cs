using GitRemote.Entities;
using System;
using System.Linq;

namespace GitRemote.Domain
{
    public class UI
    {
        private enum ParameterName
        {
            GitApiUrl = 0,
            GitToken = 1,
            GroupName = 2,
            ListProjects = 3,
        };

        public enum OperationName
        {
            ListProjects
        }

        private string[][] VALID_PARAMETERS =
        {
            new string[] { ParameterName.GitApiUrl.ToString(), "Git API URL" },
            new string[] { ParameterName.GitToken.ToString(), "User's token on Git" },
            new string[] { ParameterName.GroupName.ToString(), "Group to take the projects from" },
            new string[] { ParameterName.ListProjects.ToString(), "List all projects in the specified group" },
        };

        public string[] Arguments { get; set; }

        public Parameters ProcessArguments()
        {
            var parameters = new Parameters();

            parameters.GitApiUrl = ExtractParameter(VALID_PARAMETERS[(int)ParameterName.GitApiUrl][0]);
            parameters.GitToken = ExtractParameter(VALID_PARAMETERS[(int)ParameterName.GitToken][0]);
            parameters.GroupName = ExtractParameter(VALID_PARAMETERS[(int)ParameterName.GroupName][0]);

            parameters.Operation = GetOperationName(VALID_PARAMETERS[(int)ParameterName.ListProjects][0]);

            return parameters;
        }

        private string ExtractParameter(string parameterToExtract)
        {
            var gitUrlValue = Arguments.FirstOrDefault(x => x.ToLower().StartsWith("-" + parameterToExtract.ToLower()));
            return gitUrlValue?.Substring(parameterToExtract.Length + 2);
        }

        private string GetOperationName(string parameter)
        {
            var operation = Arguments.FirstOrDefault(x => x.ToLower().StartsWith("-" + parameter.ToLower()));
            return operation?.Substring(1);
        }
    }
}
