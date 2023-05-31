using System.Diagnostics;
using BrainStormerBackend.Data;
using BrainStormerBackend.Models.Entities;
using BrainStormerBackend.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrainStormerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrainStormController : Controller
    {

        private readonly BrainStormerDBContext _brainStormerDBContext;
        public BrainStormController(BrainStormerDBContext brainStormerDbContext)
        {
            this._brainStormerDBContext = brainStormerDbContext;
        }



        [HttpGet]
        [Route("GetAllBrainStormsByIssueId/{id:int}")]
        public async Task<IActionResult> GetAllBrainStormsByIssueId(int id)
        {
            var brainstorms = await _brainStormerDBContext.BrainStorms.Where(x => x.IssueId== id).ToListAsync();
            Console.WriteLine(brainstorms.Count);
            if (brainstorms == null)
            {
                return NotFound();
            }
            return Ok(brainstorms);
        }

        [HttpGet]
        [Route("GetBrainStormById/{id:int}")]
        public async Task<IActionResult> GetBrainStormById(int id)
        {
            var brainstorm = await _brainStormerDBContext.BrainStorms.FirstOrDefaultAsync(x => x.Id == id);
            if (brainstorm == null)
            {
                return NotFound();
            }
            return Ok(brainstorm);
        }

        [HttpPatch]
        [Route("SetToChosen/{id:int}")]
        public async Task<IActionResult> SetToChosen(int id)
        {
            var brainstorm = await _brainStormerDBContext.BrainStorms.FirstOrDefaultAsync(x => x.Id == id);
            if (brainstorm == null)
            {
                NotFound();
            }

            brainstorm!.IsChosen = !brainstorm!.IsChosen;
            await _brainStormerDBContext.SaveChangesAsync();
            return Ok(brainstorm);
        }



        [HttpPost]
        [Route("CreateNewBrainstorm")]
        public async Task<IActionResult> CreateNewBrainstorm([FromBody] CreateBrainStormRequest newBrainstormRequest)
        {
            var brainstorm = new BrainStorm
            {
                IssueId = newBrainstormRequest.IssueId,
                Name = newBrainstormRequest.Name,
                IsChosen = false,
                Visibility = true,
            };
            await _brainStormerDBContext.BrainStorms.AddAsync(brainstorm);
            await _brainStormerDBContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBrainStormById), new { id = brainstorm.Id }, brainstorm);

        }

        [HttpPatch]
        [Route("HideBrainStorm/{id:int}")]
        public async Task<IActionResult> HideBrainStorm(int id)
        {
            var brainstorm = await _brainStormerDBContext.BrainStorms.FirstOrDefaultAsync(x => x.Id == id);
            if (brainstorm == null)
            {
                NotFound();
            }
            brainstorm!.Visibility = false;
            await _brainStormerDBContext.SaveChangesAsync();
            return Ok(brainstorm);
        }

        [HttpPut]
        [Route("EditBrainStorm/{id:int}")]
        public async Task<IActionResult> EditBrainStorm(int id, [FromBody] CreateBrainStormRequest editBrainStormRequest)
        {
            var brainstorm = await _brainStormerDBContext.BrainStorms.FirstOrDefaultAsync(x => x.Id == id);
            if (brainstorm == null)
            {
                NotFound();
            }
            brainstorm.Name = editBrainStormRequest.Name;
            await _brainStormerDBContext.SaveChangesAsync();
            return Ok(brainstorm);
        }


    }
}
