using BrainStormUI.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace BrainStormUI.Pages
{
    public class ActionStepsBase : ComponentBase
    {
        [Parameter] public int BrainStormId { get; set; }

        [Parameter] public int IssueId { get; set; }

        [Parameter] public int ProjectId { get; set; }


        [Inject] public IBrainStormerService BrainStormerService { get; set; }


        public String ProblemName { get; set; }

        public List<ActionStepModel> ActionStepsList { get; set; }

        public IssueModel Issue { get; set; }

        public BrainStormModel BrainStorm { get; set; }
    }
}