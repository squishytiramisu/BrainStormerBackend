﻿using BrainStormerBackend.Data;
using BrainStormerBackend.Models.Entities;
using BrainStormerBackend.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrainStormerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {

        private readonly BrainStormerDBContext _brainStormerDBContext;
        public ProjectController(BrainStormerDBContext brainStormerDBContext)
        {
            this._brainStormerDBContext = brainStormerDBContext;
        }


        [HttpGet]
        [Route("getAllProject")]
        public async Task<IActionResult>GetAllProject()
        {
            var projects = await _brainStormerDBContext.Projects.ToListAsync();
            return Ok(projects);
        }


        [HttpGet]
        [Route("getProjectById/{id:int}")]
        [ActionName("GetProjectById")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _brainStormerDBContext.Projects.FirstOrDefaultAsync(x => x.Id == id);
            
            if(project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        [Route("createProject")]
        public async Task<IActionResult> CreateProject(CreateProjectRequest projectRequest)
        {
            var project = new Project
            {
                Name = projectRequest.Name,
                ProjectDescription = projectRequest.ProjectDescription,
                Visibility = projectRequest.Visibility
            };
            await _brainStormerDBContext.Projects.AddAsync(project);
            await _brainStormerDBContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProjectById),new { id = project.Id}, project);
        }

        [HttpDelete]
        [Route("deleteProject/{id:int}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _brainStormerDBContext.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if(project == null)
            {
                return NotFound();
            }
            _brainStormerDBContext.Projects.Remove(project);
            await _brainStormerDBContext.SaveChangesAsync();
            return NoContent();
        }
    }
}