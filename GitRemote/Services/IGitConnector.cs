using GitRemote.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitRemote.Services
{
    public interface IGitConnector
    {
        string GitApiUrl { get; set; }
        string GitToken { get; set; }
        IEnumerable<GroupInformation> GetGroups(string groupName);
        IEnumerable<ProjectInformation> GetProjects(GroupInformation group);
    }
}
