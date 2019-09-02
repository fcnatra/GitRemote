using GitRemote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GitRemote.Domain
{
    public class UI
    {
        private enum ParameterName
        {
            GitApiUrl,
            GitToken,
            GroupName,
            ListProjects,
        };

        public enum OperationName
        {
            ListProjects
        }

        private readonly string[][] VALID_ARGUMENTS =
        {
            new string[] { ParameterName.GitApiUrl.ToString(), "Git API URL (Parameter) - ** mandatory" },
            new string[] { ParameterName.GitToken.ToString(), "User's token on Git  (Parameter) - ** mandatory" },
            new string[] { ParameterName.GroupName.ToString(), "Group to take the projects from  (Parameter)" },
            new string[] { ParameterName.ListProjects.ToString(), "List all projects in the specified group (Operation) - Needs parameters: Group" },
        };

        public List<string> ValidArguments => VALID_ARGUMENTS.Select(x => $"-{x[0]}:\t{x[1]}").ToList();

        public string[] Arguments { get; set; }

        public Parameters ProcessArguments()
        {
            var parameters = new Parameters();

            parameters.GitApiUrl = ExtractParameter(VALID_ARGUMENTS[(int)ParameterName.GitApiUrl][0]);
            parameters.GitToken = ExtractParameter(VALID_ARGUMENTS[(int)ParameterName.GitToken][0]);
            parameters.GroupName = ExtractParameter(VALID_ARGUMENTS[(int)ParameterName.GroupName][0]);

            parameters.Operation = GetOperationName(VALID_ARGUMENTS[(int)ParameterName.ListProjects][0]);

            return parameters;
        }

        private string ExtractParameter(string parameterToExtract)
        {
            var gitUrlValue = Arguments.FirstOrDefault(x => x.ToLower().StartsWith("-" + parameterToExtract.ToLower()));
            return gitUrlValue?.Substring(parameterToExtract.Length + 2).Trim();
        }

        private string GetOperationName(string parameter)
        {
            var operation = Arguments.FirstOrDefault(x => x.ToLower().StartsWith("-" + parameter.ToLower()));
            return operation?.Substring(1);
        }
    }
}
