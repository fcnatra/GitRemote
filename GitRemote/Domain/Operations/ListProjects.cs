using GitRemote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitRemote.Domain.Operations
{
    public class ListProjects : IGitOperation, IListOperation<ProjectInformation>
    {
        public Entities.Parameters OperationParameters { get; set; }

        public IEnumerable<ProjectInformation> Result { get; set; }

        public void Run()
        {
            ValidateParameters();

            var gitClient = new GitLabApiClient.GitLabClient(OperationParameters.GitApiUrl, OperationParameters.GitToken);
            var groups = GetGroups(gitClient, OperationParameters.GroupName);
            var projects = GetProjectsByGroupId(gitClient, groups[0]);

            ParseProjects(projects);
        }

        private void ParseProjects(IList<GitLabApiClient.Models.Projects.Responses.Project> projects)
        {
            Result = projects.Select(project => new ProjectInformation
            {
                WebUrl = project.WebUrl
            });
        }

        private IList<GitLabApiClient.Models.Projects.Responses.Project> GetProjectsByGroupId(GitLabApiClient.GitLabClient gitClient, GitLabApiClient.Models.Groups.Responses.Group group)
        {
            var taskProjects = gitClient.Groups.GetProjectsAsync(group.Id.ToString());
            taskProjects.Wait();
            var projects = taskProjects.Result;
            return projects;
        }

        private IList<GitLabApiClient.Models.Groups.Responses.Group> GetGroups(GitLabApiClient.GitLabClient gitClient, string groupName)
        {
            var taskGroups = gitClient.Groups.SearchAsync(groupName);
            taskGroups.Wait();
            var groups = taskGroups.Result;
            return groups;
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
    }
}
