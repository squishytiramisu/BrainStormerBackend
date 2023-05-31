using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class BrainStormModel
    {
        public int Id { get; set; }
        public int IssueId { get; set; }
        public string Name { get; set; }
        public bool IsChosen { get; set; }
        public bool IsHidden { get; set; }
    }
}
