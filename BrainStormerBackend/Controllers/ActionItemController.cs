using BrainStormerBackend.Data;
using BrainStormerBackend.Models.Entities;
using BrainStormerBackend.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrainStormerBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionStepController : Controller
    {

        private readonly BrainStormerDBContext _brainStormerDBContext;
        public ActionStepController(BrainStormerDBContext brainStormerDbContext)
        {
            this._brainStormerDBContext = brainStormerDbContext;
        }


        [HttpGet]
        [Route("GetAllActionStepsBrainStormById/{id:int}")]
        public async Task<IActionResult> GetAllActionStepsByBrainStormId(int id)
        {
            var actionSteps = await _brainStormerDBContext.ActionSteps.Where(x => x.BrainStormId == id).ToListAsync();

            return Ok(actionSteps);

        }

        [HttpGet]
        [Route("GetActionStepById/{id:int}")]
        public async Task<IActionResult> GetActionStepById(int id)
        {
            var actionStep = await _brainStormerDBContext.ActionSteps.FirstOrDefaultAsync(x => x.Id == id);
            if (actionStep == null)
            {
                return NotFound();
            }
            return Ok(actionStep);
        }

        [HttpPost]
        [Route("CreateNewActionStep")]
        public async Task<IActionResult> CreateNewActionStep([FromBody] CreateActionStepRequest newActionStepRequest)
        {
            var actionStep = new ActionStep
            {
                BrainStormId = newActionStepRequest.BrainStormId,
                Description = newActionStepRequest.Description,
            };
            await _brainStormerDBContext.ActionSteps.AddAsync(actionStep);
            await _brainStormerDBContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetActionStepById), new { id = actionStep.Id }, actionStep);
        }

        [HttpDelete]
        [Route("DeleteActionStepById/{id:int}")]
        public async Task<IActionResult> DeleteActionStepById(int id)
        {
            var actionStep = await _brainStormerDBContext.ActionSteps.FirstOrDefaultAsync(x => x.Id == id);
            if (actionStep == null)
            {
                return NotFound();
            }
            _brainStormerDBContext.ActionSteps.Remove(actionStep);
            await _brainStormerDBContext.SaveChangesAsync();
            return Ok();
        }

    }
}
