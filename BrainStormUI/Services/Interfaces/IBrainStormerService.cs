
using Shared.Models;

namespace BrainStormUI.Services.Interfaces
{
    public interface IBrainStormerService
    {

        //Project CRUD
        Task<IEnumerable<ProjectModel>> GetProjects();
        Task<ProjectModel> GetProject(int id);
        Task<ProjectModel> CreateProject(ProjectModel project);
        Task<ProjectModel> UpdateProject(ProjectModel project);
        Task<ProjectModel> DeleteProject(int id);

        //Issue CRUD

        Task<IEnumerable<IssueModel>> GetIssuesByProjectId(int projectId);
        Task<IssueModel> GetIssue(int id);
        Task<IssueModel> CreateIssue(IssueModel issue);
        Task<IssueModel> DeleteIssue(int id);

        //BrainStorm CRUD
        Task<IEnumerable<BrainStormModel>> GetBrainStormsByIssueId(int issueId);
        Task<BrainStormModel> GetBrainStorm(int id);
        Task<BrainStormModel> CreateBrainStorm(BrainStormModel brainStorm);
        Task<BrainStormModel> SetToChosen(int id);
        Task<BrainStormModel> HideBrainStorm(int id);

        //ActionItem CRUD
        Task<IEnumerable<ActionStepModel>> GetActionItemsByBrainStormId(int issueId);
        Task<ActionStepModel> GetActionItem(int id);
        Task<ActionStepModel> CreateActionItem(ActionStepModel actionItem);
        Task<ActionStepModel> DeleteActionItem(int id);




    }
}
