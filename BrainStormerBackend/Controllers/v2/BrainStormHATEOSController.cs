using System.Diagnostics;
using BrainStormerBackend.Data;
using BrainStormerBackend.Models.Entities;
using BrainStormerBackend.Models.HATEOAS;
using BrainStormerBackend.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrainStormerBackend.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class BrainStormHATEOSController : Controller
    {
        private readonly LinkGenerator _linkGenerator;

        private readonly BrainStormerDBContext _brainStormerDBContext;
        public BrainStormHATEOSController(BrainStormerDBContext brainStormerDbContext, LinkGenerator linkGenerator)
        {
            _brainStormerDBContext = brainStormerDbContext;
            this._linkGenerator = linkGenerator;
        }



        [HttpGet("byIssueId/{id}", Name = nameof(GetAllBrainStormsByIssueId))]
        public async Task<IActionResult> GetAllBrainStormsByIssueId(int id)
        {
            var brainstorms = await _brainStormerDBContext.BrainStorms.Where(x => x.IssueId == id).ToListAsync();
            if (brainstorms == null)
            {
                return NotFound();
            }
            var brainstormDtos = brainstorms.Select(x => new BrainStormDto
            {
                Id = x.Id,
                IssueId = x.IssueId,
                Name = x.Name,
                IsChosen = x.IsChosen,
                Visibility = x.Visibility,
            }).ToList();

            var linkedBrainStormDtos = brainstormDtos.Select(createLinksForBrainStormDto);

            return Ok(linkedBrainStormDtos);
        }

        [HttpGet("{id}", Name = nameof(GetBrainStormById))]
        public async Task<IActionResult> GetBrainStormById(int id)
        {
            var brainstorm = await _brainStormerDBContext.BrainStorms.FirstOrDefaultAsync(x => x.Id == id);
            if (brainstorm == null)
            {
                return NotFound();
            }
            return Ok(brainstorm);
        }

        [HttpPatch("{id}", Name = nameof(SetToChosen))]
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



        [HttpPost(Name = nameof(CreateNewBrainstorm))]
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

        [HttpDelete("{id}", Name = nameof(HideBrainStorm))]
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

        [HttpPut("{id}",Name = nameof(EditBrainStorm))]
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

        private BrainStormDto createLinksForBrainStormDto(BrainStormDto brainStormDto)
        {
            var idObj = new { id = brainStormDto.Id };
            brainStormDto.Links.Add(new LinkDto(_linkGenerator.GetUriByAction(HttpContext, nameof(GetBrainStormById), values:idObj), "self", "GET"));
            brainStormDto.Links.Add(new LinkDto(_linkGenerator.GetUriByAction(HttpContext, nameof(SetToChosen), values: idObj), "setToChosen", "PATCH"));
            brainStormDto.Links.Add(new LinkDto(_linkGenerator.GetUriByAction(HttpContext, nameof(HideBrainStorm), values: idObj), "hideBrainStorm", "DELETE"));
            brainStormDto.Links.Add(new LinkDto(_linkGenerator.GetUriByAction(HttpContext, nameof(EditBrainStorm), values: idObj), "editBrainStorm", "PUT"));
            brainStormDto.Links.Add(new LinkDto(_linkGenerator.GetUriByAction(HttpContext, nameof(CreateNewBrainstorm), values: new {}), "createNewBrainStrom", "POST"));
            return brainStormDto;
        }





    }
}
