﻿using BrainStormUI.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace BrainStormUI.Pages
{
    public class ProblemsBase:ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IBrainStormerService BrainStormerService { get; set; }

        public ProjectModel Project { get; set; }

        public String ProblemName { get; set; }

        public IEnumerable<IssueModel> Issues { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Project = await BrainStormerService.GetProject(Id);
                Issues = await BrainStormerService.GetIssuesByProjectId(Id);
            }
            catch (Exception)
            {
                ProblemName = "No Project Found";
            }
        }
    }
}
