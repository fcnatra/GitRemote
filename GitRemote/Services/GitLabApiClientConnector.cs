using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GitLabApiClient.Models.Groups.Responses;
using GitRemote.Entities;

namespace GitRemote.Services
{
    public class GitLabApiClientConnector : IGitConnector
    {
        public string GitApiUrl { get; set; }
        public string GitToken { get; set; }

        private GitLabApiClient.GitLabClient _gitClient = null;

        private void ConnectToGitLab()
        {
            if (_gitClient == null)
                _gitClient = new GitLabApiClient.GitLabClient(GitApiUrl, GitToken);
        }

        public IEnumerable<GroupInformation> GetGroups(string groupName)
        {
            ConnectToGitLab();
            var taskGroups = _gitClient.Groups.SearchAsync(groupName);
            taskGroups.Wait();
            var groups = taskGroups.Result;

            IEnumerable<GroupInformation> parsedGroups = this.ParseGroups(groups);
            return parsedGroups;
        }

        public IEnumerable<ProjectInformation> GetProjects(GroupInformation group)
        {
            ConnectToGitLab();
            var taskProjects = _gitClient.Groups.GetProjectsAsync(group.Id.ToString());
            taskProjects.Wait();
            var projects = taskProjects.Result;

            var parsedProjects = ParseProjects(projects);
            return parsedProjects;
        }

        private IEnumerable<GroupInformation> ParseGroups(IList<Group> groups)
        {
            return groups.Select(group => new Entities.GroupInformation
            {
                Id = group.Id
            });
        }

        private IEnumerable<ProjectInformation> ParseProjects(IList<GitLabApiClient.Models.Projects.Responses.Project> projects)
        {
            return projects.Select(project => new ProjectInformation
            {
                WebUrl = project.WebUrl
            });
        }

    }
}
