
using Shared.Models;

namespace BrainStormUI.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectModel>> GetProjects();
        Task<ProjectModel> GetProject(int id);
        Task<ProjectModel> CreateProject(ProjectModel project);
        Task<ProjectModel> UpdateProject(ProjectModel project);
        Task DeleteProject(int id);
    }
}
