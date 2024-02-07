using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Application.Mobilizations.Commands;
using MobDeMob.Application.Mobilizations.Queries;
using MobDeMob.Application.Parts;
using MobDeMob.Domain.Entities;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MobilizationController : ControllerBase
{
    private readonly ILogger<ChecklistController> _logger;


    private readonly ISender _sender;

    public MobilizationController(ILogger<ChecklistController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet()]
    [Route("GetSingle/{mobId}")]
    public async Task<ActionResult<MobilizationDto?>> GetMobilizationById(string mobId, CancellationToken cancellationToken)
    {
        var mob = await _sender.Send(new GetMobilizationByIdQuery { id = mobId }, cancellationToken);
        return mob is not null ? Ok(mob) : NotFound();
    }

    [HttpGet()]
    [Route("GetAll")]
    public async Task<ActionResult<IEnumerable<MobilizationDto>>> GetAllMobilizations(CancellationToken cancellationToken)
    {
        var mobs = await _sender.Send(new GetAllMobilizationsQuery { }, cancellationToken);
        return mobs is not null ? Ok(mobs) : NotFound();
    }

    [HttpPut("{mobId}", Name = "UpdateMobilization")]
    public async Task<ActionResult> UpdateMobilization(string mobId, MobilizationDto mobilizationDto, CancellationToken cancellationToken)
    {
        await _sender.Send(new UpdateMobilizationCommand { id = mobId, Title = mobilizationDto.Title, Description = mobilizationDto.Description }, cancellationToken);
        return NoContent();
    }

    [HttpPost(Name = "CreateMobilization")]
    public async Task<ActionResult<MobilizationDto>> CreateMobilization(MobilizationDto mobilizationDto, CancellationToken cancellationToken)
    {
        var id = await _sender.Send(new AddMobilizationCommand { Title = mobilizationDto.Title, Description = mobilizationDto.Description, MobilizationType = mobilizationDto.MobilizationType }, cancellationToken);
        return CreatedAtAction(nameof(GetMobilizationById), new { itemId = id }, mobilizationDto);
    }

    [HttpPut("AddPartToMobilization/{mobId}")]
    public async Task<ActionResult> AddPartToMoblization(string mobId, string partId, CancellationToken cancellationToken)
    {
        await _sender.Send(new AddPartToMobilizationCommand { id = mobId, partId = partId }, cancellationToken);
        return NoContent();
    }

    [HttpPut("RemovePartFromMobilization/{mobId}")]
    public async Task<ActionResult> RemovePartFromMoblization(string mobId, string partId, CancellationToken cancellationToken)
    {
        await _sender.Send(new RemovePartFromMobilizationCommand { id = mobId, partId = partId }, cancellationToken);
        return NoContent();
    }

    [HttpGet("GetAllPartsInMobilization/{mobId}")]
    public async Task<ActionResult<IEnumerable<PartDto>>> GetAllPartsInMobilization(string mobId, CancellationToken cancellationToken, bool includeChildren = false)
    {
        var parts = await _sender.Send(new GetAllPartsInMobilizationQuery { id = mobId, includeChildren = includeChildren }, cancellationToken);
        return Ok(parts);
    }
}
