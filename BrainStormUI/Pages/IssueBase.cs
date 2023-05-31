using BrainStormUI.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace BrainStormUI.Pages
{
    public class IssueBase : ComponentBase
    {
        [Parameter]
        public int ProjectId { get; set; }

        [Parameter]
        public int IssueId{ get; set; }


        [Inject]
        public IBrainStormerService BrainStormerService { get; set; }


        public String ProblemName { get; set; }

        public IEnumerable<BrainStormModel> BrainStorms { get; set; }

        public IssueModel Issue { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Issue = await BrainStormerService.GetIssue(IssueId);
                BrainStorms = await BrainStormerService.GetBrainStormsByIssueId(IssueId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                ProblemName = "Issue not found";
            }
        }
    }
}