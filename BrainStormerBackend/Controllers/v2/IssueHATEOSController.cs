using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


using Microsoft.EntityFrameworkCore;
using BrainStormerBackend.Data;
using BrainStormerBackend.Models.Entities;
using BrainStormerBackend.Models.HATEOAS;
using BrainStormerBackend.Models.Requests;
using System.Linq;

namespace BrainStormerBackend.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class IssueHATEOSController : Controller
    {

        private readonly BrainStormerDBContext _brainStormerDBContext;
        private readonly LinkGenerator _linkGenerator;
        public IssueHATEOSController(BrainStormerDBContext brainStormerDbContext, LinkGenerator linkGenerator)
        {
            this._brainStormerDBContext = brainStormerDbContext;
            this._linkGenerator = linkGenerator;

        }


        [HttpGet("{id}",Name = nameof(GetAllIssuesByProjectId))]
        public async Task<IActionResult> GetAllIssuesByProjectId(int id)
        {
            var issues = await _brainStormerDBContext.Issues.Where(x => x.ProjectId == id).ToListAsync();
            
            if (issues == null)
            {
                NotFound();
            }

            var issueDtos = issues.Select(x => new IssueDto
            {
                Id = x.Id,
                ProjectId = x.ProjectId,
                Name = x.Name,
                Description = x.Description,
            }).ToList();
            var linkedIssuesDtos = issueDtos.Select(CreateLinksForIssue);
            var wrapper = new LinkedCollectionBaseDto<IssueDto>(linkedIssuesDtos);
            return Ok(CreateLinksForIssues(wrapper));

        }



        [HttpGet("{id}", Name = nameof(GetIssueById))]
        public async Task<IActionResult> GetIssueById(int id)
        {
            var issue = await _brainStormerDBContext.Issues.FirstOrDefaultAsync(x => x.Id == id);
            if (issue == null)
            {
                return NotFound();
            }
            var issueDto = new IssueDto
            {
                Id = issue.Id,
                ProjectId = issue.ProjectId,
                Name = issue.Name,
                Description = issue.Description,
            };

            return Ok(CreateLinksForIssue(issueDto));
        }


        [HttpPost(Name = nameof(CreateNewIssue))]
        public async Task<IActionResult> CreateNewIssue([FromBody] CreateIssueRequest newIssueRequest)
        {
            var issue = new Issue { Name = newIssueRequest.Name, Description = newIssueRequest.Description, ProjectId = newIssueRequest.ProjectId };
            await _brainStormerDBContext.Issues.AddAsync(issue);
            await _brainStormerDBContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetIssueById), new { id = issue.Id }, issue);
        }


        [HttpDelete("{id}", Name = nameof(Delete))]
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


        private IssueDto CreateLinksForIssue(IssueDto issue)
        {
            var idObj = new { id = issue.Id };
            issue.Links.Add(new LinkDto(_linkGenerator.GetPathByName(nameof(GetIssueById), idObj), "self", "GET"));
            issue.Links.Add(new LinkDto(_linkGenerator.GetPathByName(nameof(Delete), idObj), "delete_issue", "DELETE"));
            issue.Links.Add(new LinkDto(_linkGenerator.GetPathByName(nameof(CreateNewIssue), idObj), "create_issue", "POST"));
            return issue;
        }

        private LinkedCollectionBaseDto<IssueDto> CreateLinksForIssues(LinkedCollectionBaseDto<IssueDto> issuesWrapper)
        {
            issuesWrapper.Links.Add(new LinkDto(_linkGenerator.GetPathByName(nameof(GetAllIssuesByProjectId),new {}), "self", "GET"));
            return issuesWrapper;
        }


    }
}
