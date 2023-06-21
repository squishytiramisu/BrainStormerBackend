using System.Diagnostics;
using BrainStormerBackend.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace BrainStormerBackend.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class GPTController : ControllerBase
    {

        private HttpClient client;

        public GPTController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.openai.com/v1/chat/");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        [HttpPost]
        [Route("JudgeIdea")]
        public async Task<IActionResult> GetGPT([FromBody] JudgeRequest judgeRequest)
        {

            var brainstorms = new[]{
                new {
                        role = "user",
                        content = $"Given that we are in a project named ${judgeRequest.ProjectName}, and we have a problem to solve called ${judgeRequest.IssueName}, is this idea good to solve our problem: ${judgeRequest.BrainStormName}? Answer yes or no, nothing else."
                    }
            };
            var requestBody = new {
                messages = brainstorms,
                temperature = 1,
                max_tokens= 96,
                top_p= 1,
                frequency_penalty= 0,
                presence_penalty= 0,
                model= "gpt-3.5-turbo"
            };
            var response = await client.PostAsJsonAsync("completions", requestBody);
            var content = response.Content.ReadFromJsonAsync<object>();
            return Ok(content);
        }

        [HttpPost]
        [Route("GenerateIdea")]
        public async Task<IActionResult> GenerateIdea([FromBody] GenerateRequest generateRequest)
        {

            var brainstorms = new[]{
                new {
                    role = "user",
                    content = $"Given that we are in a project named ${generateRequest.ProjectName}, and we have a problem to solve called ${generateRequest.IssueName} and we are brainstorming. Tell me a good solution idea in 4 words max? Just the 1-4 words, nothing else."
                }
            };
            var requestBody = new
            {
                messages = brainstorms,
                temperature = 1,
                max_tokens = 110,
                top_p = 1,
                frequency_penalty = 0,
                presence_penalty = 0,
                model = "gpt-3.5-turbo"
            };
            var response = await client.PostAsJsonAsync("completions", requestBody);
            var content = response.Content.ReadFromJsonAsync<object>();
            return Ok(content);
        }
    }
}
