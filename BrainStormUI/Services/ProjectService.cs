using BrainStormUI.Services.Interfaces;
using Shared.Models;
using System.Net.Http.Json;

namespace BrainStormUI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient client;

        public ProjectService(HttpClient client)
        {
            this.client = client;
        }

        public Task<ProjectModel> CreateProject(ProjectModel project)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProject(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProjectModel> GetProject(int id)
        {
            try
            {
                var project = await this.client.GetFromJsonAsync<ProjectModel>($"api/Project/GetProjectById/{id}");

                return project;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProjectModel>> GetProjects()
        {
            try
            {
                var projects = await this.client.GetFromJsonAsync<IEnumerable<ProjectModel>>("api/Project/GetAllProject");
                return projects;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<ProjectModel> UpdateProject(ProjectModel project)
        {
            throw new NotImplementedException();
        }
    }
}
