using System.Runtime.CompilerServices;
using BrainStormerBackend.Data;
using BrainStormerBackend.Models.Entities;
using BrainStormerBackend.Models.HATEOAS;
using BrainStormerBackend.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;


namespace BrainStormerBackend.Controllers.v2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ProjectHATEOSController : Controller
    {

        private readonly BrainStormerDBContext _brainStormerDBContext;
        private readonly LinkGenerator linkGenerator;


        public ProjectHATEOSController(BrainStormerDBContext brainStormerDbContext,LinkGenerator linkGenerator)
        {
            this.linkGenerator = linkGenerator;
            _brainStormerDBContext = brainStormerDbContext;

        }


        [HttpGet(Name = nameof(GetAllProject))]
        public async Task<IActionResult> GetAllProject()
        {
            var projects = await _brainStormerDBContext.Projects.ToListAsync();

            var projectDtos = projects.Select(x => new ProjectDto
            {
                Id = x.Id,
                Name = x.Name,
                ProjectDescription = x.ProjectDescription,
                Visibility = x.Visibility
            }).ToList();

            var linkedProjectDtos = projectDtos.Select(CreateLinksForProject);

            var wrapper = new LinkedCollectionBaseDto<ProjectDto>(linkedProjectDtos);
            return Ok(CreateLinksForProjects(wrapper));
        }


        [HttpGet("{id}",Name = nameof(GetProjectById))]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _brainStormerDBContext.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            var projectDto = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                ProjectDescription = project.ProjectDescription,
                Visibility = project.Visibility
            };
            return Ok(CreateLinksForProject(projectDto));
        }

        [HttpPost(Name = nameof(CreateProject))]
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

        [HttpDelete("{id}",Name = nameof(DeleteProject))]
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

        private ProjectDto CreateLinksForProject(ProjectDto project)
        {
            project.Links.Add(new LinkDto(linkGenerator.GetPathByName(nameof(GetProjectById), new { id = project.Id }), "self", "GET"));
            project.Links.Add(new LinkDto(linkGenerator.GetPathByName(nameof(DeleteProject), new { id = project.Id }), "delete_project", "DELETE"));
            return project;
        }

        private LinkedCollectionBaseDto<ProjectDto> CreateLinksForProjects(
            LinkedCollectionBaseDto<ProjectDto> projectsWrapper)
        {
            projectsWrapper.Links.Add(new LinkDto(linkGenerator.GetPathByName(nameof(GetAllProject),new {}), "self", "GET"));
            return projectsWrapper;
        }
    }
}
