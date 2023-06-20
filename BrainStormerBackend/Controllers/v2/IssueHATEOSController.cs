using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


using Microsoft.EntityFrameworkCore;
using BrainStormerBackend.Data;
using BrainStormerBackend.Models.Entities;
using BrainStormerBackend.Models.Requests;

namespace BrainStormerBackend.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class IssueHATEOSController : Controller
    {

        private readonly BrainStormerDBContext _brainStormerDBContext;
        public IssueHATEOSController(BrainStormerDBContext brainStormerDbContext)
        {
            this._brainStormerDBContext = brainStormerDbContext;
        }


        [HttpGet]
        [Route("GetAllIssuesByProjectId/{id:int}")]
        public async Task<IActionResult> GetAllIssuesByProjectId(int id)
        {
            var issues = await _brainStormerDBContext.Issues.Where(x => x.ProjectId == id).ToListAsync();
            if (issues==null)
            {
                NotFound();
            }
            return Ok(issues);
        }



        [HttpGet]
        [Route("GetIssueById/{id:int}")]
        public async Task<IActionResult> GetIssueById(int id)
        {
            var issue = await _brainStormerDBContext.Issues.FirstOrDefaultAsync(x => x.Id == id);
            if (issue == null)
            {
                return NotFound();
            }
            return Ok(issue);
        }


        [HttpPost]
        [Route("CreateNewIssue")]
        public async Task<IActionResult> CreateNewIssue([FromBody] CreateIssueRequest newIssueRequest)
        {
            var issue = new Issue
            {
                ProjectId = newIssueRequest.ProjectId,
                Name = newIssueRequest.Name,
                Description = newIssueRequest.Description,
            };
            await _brainStormerDBContext.Issues.AddAsync(issue);
            await _brainStormerDBContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetIssueById), new { id = issue.Id }, issue);
        }


        [HttpDelete]
        [Route("DeleteIssueById/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var issue = await _brainStormerDBContext.Issues.FirstOrDefaultAsync(x => x.Id == id);
            if (issue == null)
            {
                return NotFound();
            }
            _brainStormerDBContext.Issues.Remove(issue);
            await _brainStormerDBContext.SaveChangesAsync();
            return Ok();
        }
    }
}
