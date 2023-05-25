using BrainStormUI.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace BrainStormUI.Pages
{
    public class ProjectBase: ComponentBase
    {
        [Inject]
        public IProjectService ProjectService { get; set; }

        public IEnumerable<ProjectModel> Projects { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Projects = await ProjectService.GetProjects();
        }
    }
}
