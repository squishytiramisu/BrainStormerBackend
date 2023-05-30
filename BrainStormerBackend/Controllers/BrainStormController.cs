using BrainStormerBackend.Data;
using BrainStormerBackend.Models.Entities;
using BrainStormerBackend.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrainStormerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrainStormController : Controller
    {

        private readonly BrainStormerDBContext _brainStormerDBContext;
        public BrainStormController(BrainStormerDBContext brainStormerDBContext)
        {
            this._brainStormerDBContext = brainStormerDBContext;
        }



        [HttpGet]
        [Route("GetAllBrainStormsByProjectId/{id:int}")]
        public async Task<IActionResult> GetAllBrainStormsByProjectId(int id)
        {
            var brainstorms = await _brainStormerDBContext.BrainStorms.Where(x => x.IssueId== id).ToListAsync();
            return Ok(brainstorms);
        }

        [HttpGet]
        [Route("GetBrainStormById/{id:int}")]
        public async Task<IActionResult> GetBrainStormById(int id)
        {
            var brainstorm = await _brainStormerDBContext.BrainStorms.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(brainstorm);
        }

        [HttpPatch]
        [Route("SetToChosen/{id:int}")]
        public async Task<IActionResult> SetToChosen(int id)
        {
            var brainstorm = await _brainStormerDBContext.BrainStorms.FirstOrDefaultAsync(x => x.Id == id);
            brainstorm.IsChosen = true;
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
            brainstorm.Visibility = false;
            await _brainStormerDBContext.SaveChangesAsync();
            return Ok(brainstorm);
        }



    }
}
