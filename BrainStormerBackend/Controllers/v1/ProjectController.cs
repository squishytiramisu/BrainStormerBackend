using BrainStormerBackend.Data;
using BrainStormerBackend.Models.Entities;
using BrainStormerBackend.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrainStormerBackend.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectController : Controller
    {

        private readonly BrainStormerDBContext _brainStormerDBContext;

        private ProjectsCache _projectsCache;
        public ProjectController(BrainStormerDBContext brainStormerDbContext)
        {
            _brainStormerDBContext = brainStormerDbContext;
            _projectsCache = new ProjectsCache(brainStormerDbContext);
        }


        [HttpGet]
        [Route("getAllProject")]
        public async Task<IActionResult> GetAllProject()
        {
            var projects = await _brainStormerDBContext.Projects.ToListAsync();

            return Ok(projects);
        }


        [HttpGet]
        [Route("getProjectById/{id:int}")]
        [ActionName("GetProjectById")]
        public Task<IActionResult> GetProjectById(int id)
        {
            var project = _projectsCache.GetProjectFromCache(id);

            if (project == null)
            {
                return Task.FromResult<IActionResult>(NotFound());
            }
            return Task.FromResult<IActionResult>(Ok(project));
        }

        [HttpPost]
        [Route("createProject")]
        public async Task<IActionResult> CreateProject(CreateProjectRequest projectRequest)
        {
            var project = new Project
            {
                Name = projectRequest.Name,
                ProjectDescription = projectRequest.ProjectDescription,
                Visibility = true
            };
            await _brainStormerDBContext.Projects.AddAsync(project);
            await _brainStormerDBContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }

        [HttpDelete]
        [Route("deleteProject/{id:int}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _brainStormerDBContext.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            _brainStormerDBContext.Projects.Remove(project);
            await _brainStormerDBContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
