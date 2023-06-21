using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class BrainStormTemp
    {
        public string name { get; set; }
        public IssueTemp issue { get; set; }
    }


    public class IssueTemp
    {
        public string name { get; set; }
        public ProjectTemp project { get; set; }
    }

    public class ProjectTemp
    {
        public string name { get; set; }
    }

    public class GraphQLBrainStormResponse
    {
        public List<BrainStormTemp> brainStorms { get; set; }
    }


    public class GrapQLIssueResponse
    {
        public List<IssueTemp> issues { get; set; }
    }

}


