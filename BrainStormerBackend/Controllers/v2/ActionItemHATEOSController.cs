using BrainStormerBackend.Data;
using BrainStormerBackend.Models.Entities;
using BrainStormerBackend.Models.HATEOAS;
using BrainStormerBackend.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrainStormerBackend.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ActionItemHATEOSController : Controller
    {
        private readonly LinkGenerator _linkGenerator;

        private readonly BrainStormerDBContext _brainStormerDBContext;
        public ActionItemHATEOSController(BrainStormerDBContext brainStormerDbContext, LinkGenerator linkGenerator)
        {
            _brainStormerDBContext = brainStormerDbContext;
            this._linkGenerator = linkGenerator;
        }


        [HttpGet("byIssueId/{id}", Name = nameof(GetAllActionStepsByBrainStormId))]
        public async Task<IActionResult> GetAllActionStepsByBrainStormId(int id)
        {
            var actionSteps = await _brainStormerDBContext.ActionSteps.Where(x => x.BrainStormId == id).ToListAsync();

            var actionStepDtos = actionSteps.Select(x => new ActionStepDto
            {
                Id = x.Id,
                BrainStormId = x.BrainStormId,
                Description = x.Description,
            }).ToList();

            var linkedActionStepDtos = actionStepDtos.Select(createLinksForActionStepDto);

            return Ok(linkedActionStepDtos);

        }

        [HttpGet("{id}", Name = nameof(GetActionStepById))]
        public async Task<IActionResult> GetActionStepById(int id)
        {
            var actionStep = await _brainStormerDBContext.ActionSteps.FirstOrDefaultAsync(x => x.Id == id);
            if (actionStep == null)
            {
                return NotFound();
            }
            var actionStepDto = new ActionStepDto
            {
                Id = actionStep.Id,
                BrainStormId = actionStep.BrainStormId,
                Description = actionStep.Description,
            };
            return Ok(actionStepDto);
        }

        [HttpPost(Name = nameof(CreateNewActionStep))]
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

        [HttpDelete("{id}", Name = nameof(DeleteActionStepById))]
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


        private ActionStepDto createLinksForActionStepDto(ActionStepDto actionStepDto)
        {
            actionStepDto.Links.Add(new LinkDto(_linkGenerator.GetUriByAction(HttpContext, nameof(GetActionStepById), values: new { id = actionStepDto.Id }), "self", "GET"));
            actionStepDto.Links.Add(new LinkDto(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteActionStepById), values: new { id = actionStepDto.Id }), "delete_action_step", "DELETE"));
            actionStepDto.Links.Add(new LinkDto(_linkGenerator.GetUriByAction(HttpContext, nameof(CreateNewActionStep), values: new { id = actionStepDto.Id }), "create_action_step", "POST"));
            return actionStepDto;
        }
    }
}
