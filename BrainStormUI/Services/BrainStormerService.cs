﻿using System.ComponentModel.DataAnnotations;
using BrainStormUI.Services.Interfaces;
using Shared.Models;
using System.Net.Http.Json;

namespace BrainStormUI.Services
{
    public class BrainStormerService : IBrainStormerService
    {
        private readonly HttpClient client;

        public BrainStormerService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<ProjectModel> CreateProject(ProjectModel project)
        {
            try
            {
                var response = await client.PostAsJsonAsync<ProjectModel>("api/Project/createProject", project);
                return await response.Content.ReadFromJsonAsync<ProjectModel>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ProjectModel> DeleteProject(int id)
        {
            try
            {
                var response = await client.DeleteAsync($"api/Project/deleteProject/{id}");
                return await response.Content.ReadFromJsonAsync<ProjectModel>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<IssueModel>> GetIssuesByProjectId(int projectId)
        {
            try
            {
                var issues = await this.client.GetFromJsonAsync<IEnumerable<IssueModel>>($"api/Issue/GetAllIssuesByProjectId/{projectId}");
                return issues;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IssueModel> GetIssue(int id)
        {
            try
            {
                var issue = await this.client.GetFromJsonAsync<IssueModel>($"api/Issue/GetIssueById/{id}");
                return issue;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IssueModel> CreateIssue(IssueModel issue)
        {
            try
            {
                var response = await client.PostAsJsonAsync<IssueModel>("api/Issue/CreateNewIssue", issue);
                return await response.Content.ReadFromJsonAsync<IssueModel>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IssueModel> DeleteIssue(int id)
        {
            try
            {
                var response = await client.DeleteAsync($"api/Issue/DeleteIssueById/{id}");
                return await response.Content.ReadFromJsonAsync<IssueModel>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<BrainStormModel>> GetBrainStormsByIssueId(int issueId)
        {
            try
            {
                var brainStorms =
                    await this.client.GetFromJsonAsync<IEnumerable<BrainStormModel>>(
                        $"api/BrainStorm/GetAllBrainStormsByIssueId/{issueId}");
                return brainStorms;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<BrainStormModel> GetBrainStorm(int id)
        {
            try
            {
                var brainStorm = await this.client.GetFromJsonAsync<BrainStormModel>($"api/BrainStorm/GetBrainStormById/{id}");
                return brainStorm;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<BrainStormModel> CreateBrainStorm(BrainStormModel brainStorm)
        {
            try
            {
                var response = await client.PostAsJsonAsync<BrainStormModel>("api/BrainStorm/CreateNewBrainstorm", brainStorm);
                return await response.Content.ReadFromJsonAsync<BrainStormModel>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<BrainStormModel> SetToChosen(int id)
        {
            var respone = await client.PatchAsync($"api/BrainStorm/SetToChosen/{id}", null);
            return respone.Content.ReadFromJsonAsync<BrainStormModel>().Result;
        }

        public async Task<BrainStormModel> HideBrainStorm(int id)
        {
            var respone = await client.PatchAsync($"api/BrainStorm/HideBrainStorm/{id}", null);
            return respone.Content.ReadFromJsonAsync<BrainStormModel>().Result;
        }

        public async Task<IEnumerable<ActionStepModel>> GetActionItemsByBrainStormId(int issueId)
        {
            var actionItems =
                await this.client.GetFromJsonAsync<IEnumerable<ActionStepModel>>(
                                       $"api/ActionStep/GetAllActionStepsBrainStormById/{issueId}");
            return actionItems;
        }

        public async Task<ActionStepModel> GetActionItem(int id)
        {
            var actionItem = await this.client.GetFromJsonAsync<ActionStepModel>($"api/ActionStep/GetActionStepById/{id}");
            return actionItem;
        }

        public async Task<ActionStepModel> CreateActionItem(ActionStepModel actionItem)
        {
            var response = await client.PostAsJsonAsync<ActionStepModel>("api/ActionStep/CreateNewActionStep", actionItem);
            return await response.Content.ReadFromJsonAsync<ActionStepModel>();
        }

        public async Task<ActionStepModel> DeleteActionItem(int id)
        {
            var response = await client.DeleteAsync($"api/ActionStep/DeleteActionStepById/{id}");
            return await response.Content.ReadFromJsonAsync<ActionStepModel>();

        }

        public async Task<ProjectModel> GetProject(int id)
        {
            try
            {
                var project = await this.client.GetFromJsonAsync<ProjectModel>($"api/Project/getProjectById/{id}");

                return project;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<IEnumerable<ProjectModel>> GetProjects()
        {
            try
            {
                var projects = await this.client.GetFromJsonAsync<IEnumerable<ProjectModel>>("api/Project/getAllProject");
                return projects;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Task<ProjectModel> UpdateProject(ProjectModel project)
        {
            var response = client.PutAsJsonAsync<ProjectModel>("api/Project/UpdateProject", project);
            return response.Result.Content.ReadFromJsonAsync<ProjectModel>();
        }
    }
}