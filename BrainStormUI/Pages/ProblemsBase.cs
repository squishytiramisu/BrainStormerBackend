using BrainStormUI.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace BrainStormUI.Pages
{
    public class ProblemsBase:ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IProjectService ProjectService { get; set; }

        public ProjectModel Project { get; set; }

        public String ProblemName { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                Project = await ProjectService.GetProject(Id);
            }
            catch (Exception)
            {
                ProblemName = "No Project Found";
            }
        }
    }
}
