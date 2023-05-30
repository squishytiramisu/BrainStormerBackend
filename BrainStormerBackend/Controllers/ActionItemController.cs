using BrainStormerBackend.Data;
using BrainStormerBackend.Models.Entities;
using BrainStormerBackend.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrainStormerBackend.Controllers
{
    public class ActionStepController : Controller
    {

        private readonly BrainStormerDBContext _brainStormerDBContext;
        public ActionStepController(BrainStormerDBContext brainStormerDBContext)
        {
            this._brainStormerDBContext = brainStormerDBContext;
        }


        [HttpGet]
        [Route("GetAllActionStepsIssueById/{id:int}")]
        public async Task<IActionResult> GetAllActionStepsByIssueId(int id)
        {
            var actionSteps = await _brainStormerDBContext.ActionSteps.Where(x => x.IssueId == id).ToListAsync();
            return Ok(actionSteps);

        }

        [HttpGet]
        [Route("GetActionStepById/{id:int}")]
        public async Task<IActionResult> GetActionStepById(int id)
        {
            var actionStep = await _brainStormerDBContext.ActionSteps.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(actionStep);
        }

        [HttpPost]
        [Route("CreateNewActionStep")]
        public async Task<IActionResult> CreateNewActionStep([FromBody] CreateActionStepRequest newActionStepRequest)
        {
            var actionStep = new ActionStep
            {
                IssueId = newActionStepRequest.BrainStormId,
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
            _brainStormerDBContext.ActionSteps.Remove(actionStep);
            await _brainStormerDBContext.SaveChangesAsync();
            return Ok();
        }

    }
}
