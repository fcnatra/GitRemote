using System;
using System.Collections.Generic;
using System.Text;
using GitRemote.Domain;

namespace GitRemote.Entities
{
    public class Parameters
    {
        public string GitApiUrl { get; set; }
        public string GitToken { get; set; }
        public string GroupName { get; set; }
        public string Operation { get; set; }
    }
}
