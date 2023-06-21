using System.ComponentModel.DataAnnotations;
using BrainStormUI.Services.Interfaces;
using Shared.Models;
using System.Net.Http.Json;
using BrainStormerBackend.Models.Requests;
using GraphQL;
using GraphQL.Client.Http;

namespace BrainStormUI.Services
{
    public class BrainStormerService : IBrainStormerService
    {
        private readonly HttpClient client;

        private readonly GraphQLHttpClient graphQLClient;

        public BrainStormerService(HttpClient client, GraphQLHttpClient graphQlClient)
        {
            this.graphQLClient = graphQlClient;
            this.client = client;

            
        }


        public async Task<RootResponse> JudgeBrainStorm(int brainStormId, string token)
        {

            try
            {
                graphQLClient.HttpClient.DefaultRequestHeaders.Remove("Authorization");
                graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var data = await this.graphQLClient.SendQueryAsync<GraphQLBrainStormResponse>(new GraphQLRequest
                {
                    Query = @"query BrainStorms($id : Int) {
                                brainStorms(where: {id: {eq: $id}}) {
                                    name
                                    issue {
                                        name
                                        project {
                                            name
                                        }
                                    }
                                }
                            }

                    ",
                    Variables = new { id = brainStormId }
                });

                var parsed = data.Data.brainStorms[0];
                var response = await client.PostAsJsonAsync<JudgeRequest>("api/v2/GPT/JudgeIdea", new JudgeRequest
                {
                    BrainStormName = parsed.name,IssueName = parsed.issue.name,ProjectName = parsed.issue.project.name, IssueDescription = ""
                });
                return await response.Content.ReadFromJsonAsync<RootResponse>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<string> GenerateBrainStorm(int issueId, string token)
        {
            graphQLClient.HttpClient.DefaultRequestHeaders.Remove("Authorization");
            graphQLClient.HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            try
            {
                var data = await this.graphQLClient.SendQueryAsync<GrapQLIssueResponse>(new GraphQLRequest
                {
                    Query = @"query Issues($id : Int) {
                                issues(where: {id: {eq: $id}}) {
                                    name
                                    project {
                                        name
                                    }
                                }
                            }
                            ",
                    Variables = new { id = issueId }
                });

                var parsed = data.Data.issues[0];
                var response = await client.PostAsJsonAsync<GenerateRequest>("api/v2/GPT/GenerateIdea", new GenerateRequest
                {
                    IssueName = parsed.name,
                    ProjectName = parsed.project.name,
                    IssueDescription = ""
                });
                var message= await response.Content.ReadFromJsonAsync<RootResponse>();
                return message.result.choices[0].message.content;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<string> Login(string username, string password)
        {
            // /api/v1/identity/token
            try
            {
                var response = await client.PostAsJsonAsync<LoginRequest>("api/v1/identity/token", new LoginRequest
                {
                    Username = username,
                    Secret = password
                });
                var message = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return message.token;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<ProjectModel> CreateProject(ProjectModel project)
        {
            try
            {
                var response = await client.PostAsJsonAsync<ProjectModel>("api/v1/Project/createProject", project);
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
                var response = await client.DeleteAsync($"api/v1/Project/deleteProject/{id}");
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
                var issues = await this.client.GetFromJsonAsync<IEnumerable<IssueModel>>($"api/v1/Issue/GetAllIssuesByProjectId/{projectId}");
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
                var issue = await this.client.GetFromJsonAsync<IssueModel>($"api/v1/Issue/GetIssueById/{id}");
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
                var response = await client.PostAsJsonAsync<IssueModel>("api/v1/Issue/CreateNewIssue", issue);
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
                var response = await client.DeleteAsync($"api/v1/Issue/DeleteIssueById/{id}");
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
                        $"api/v1/BrainStorm/GetAllBrainStormsByIssueId/{issueId}");
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
                var brainStorm = await this.client.GetFromJsonAsync<BrainStormModel>($"api/v1/BrainStorm/GetBrainStormById/{id}");
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
                var response = await client.PostAsJsonAsync<BrainStormModel>("api/v1/BrainStorm/CreateNewBrainstorm", brainStorm);
                return await response.Content.ReadFromJsonAsync<BrainStormModel>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<BrainStormModel> SetToChosen(int id)
        {
            var respone = await client.PatchAsync($"api/v1/BrainStorm/SetToChosen/{id}", null);
            return respone.Content.ReadFromJsonAsync<BrainStormModel>().Result;
        }

        public async Task<BrainStormModel> HideBrainStorm(int id)
        {
            var respone = await client.PatchAsync($"api/v1/BrainStorm/HideBrainStorm/{id}", null);
            return respone.Content.ReadFromJsonAsync<BrainStormModel>().Result;
        }

        public async Task<BrainStormModel> EditBrainStorm(int id, BrainStormModel brainStorm)
        {
            var response = await client.PutAsJsonAsync($"api/v1/BrainStorm/EditBrainStorm/{id}", brainStorm);
            return response.Content.ReadFromJsonAsync<BrainStormModel>().Result;
        }

        public async Task<IEnumerable<ActionStepModel>> GetActionItemsByBrainStormId(int issueId)
        {
            var actionItems =
                await this.client.GetFromJsonAsync<IEnumerable<ActionStepModel>>(
                                       $"api/v1/ActionStep/GetAllActionStepsBrainStormById/{issueId}");
            return actionItems;
        }

        public async Task<ActionStepModel> GetActionItem(int id)
        {
            var actionItem = await this.client.GetFromJsonAsync<ActionStepModel>($"api/v1/ActionStep/GetActionStepById/{id}");
            return actionItem;
        }

        public async Task<ActionStepModel> CreateActionItem(ActionStepModel actionItem)
        {
            var response = await client.PostAsJsonAsync<ActionStepModel>("api/v1/ActionStep/CreateNewActionStep", actionItem);
            return await response.Content.ReadFromJsonAsync<ActionStepModel>();
        }

        public async Task<ActionStepModel> DeleteActionItem(int id)
        {
            var response = await client.DeleteAsync($"api/v1/ActionStep/DeleteActionStepById/{id}");
            return await response.Content.ReadFromJsonAsync<ActionStepModel>();

        }

        public async Task<ProjectModel> GetProject(int id)
        {
            try
            {
                var project = await this.client.GetFromJsonAsync<ProjectModel>($"api/v1/Project/getProjectById/{id}");

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
                var projects = await this.client.GetFromJsonAsync<IEnumerable<ProjectModel>>("api/v1/Project/getAllProject");
                return projects;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public Task<ProjectModel> UpdateProject(ProjectModel project)
        {
            var response = client.PutAsJsonAsync<ProjectModel>("api/v1/Project/UpdateProject", project);
            return response.Result.Content.ReadFromJsonAsync<ProjectModel>();
        }
    }
}
