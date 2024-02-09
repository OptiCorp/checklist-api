using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Application.Mobilizations.Commands;
using MobDeMob.Application.Mobilizations.Queries;
using MobDeMob.Application.Parts;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MobilizationsController : ControllerBase
{
    private readonly ILogger<ChecklistsController> _logger;
    private readonly ISender _sender;

    public MobilizationsController(ILogger<ChecklistsController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet("{mobId}")]
    public async Task<ActionResult<MobilizationDto?>> GetMobilizationById(string mobId, CancellationToken cancellationToken = default)
    {
        var mob = await _sender.Send(new GetMobilizationByIdQuery { id = mobId }, cancellationToken);
        return mob is not null ? Ok(mob) : NotFound();
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<MobilizationDto>>> GetAllMobilizations(CancellationToken cancellationToken = default)
    {
        var mobs = await _sender.Send(new GetAllMobilizationsQuery(), cancellationToken);
        return mobs is not null ? Ok(mobs) : NotFound();
    }

    [HttpGet("GetAllPartsInMobilization/{mobId}")]
    public async Task<ActionResult<IEnumerable<PartDto>>> GetAllPartsInMobilization(string mobId, bool includeChildren = false, CancellationToken cancellationToken = default)
    {
        var parts = await _sender.Send(new GetAllPartsInMobilizationQuery { id = mobId, includeChildren = includeChildren }, cancellationToken);
        return Ok(parts);
    }

    [HttpPost]
    public async Task<ActionResult<MobilizationDto>> CreateMobilization(AddMobilizationCommand addMobilizationCommand, CancellationToken cancellationToken = default)
    {
        var id = await _sender.Send(addMobilizationCommand, cancellationToken);
        return CreatedAtAction(nameof(GetMobilizationById), new { itemId = id });
    }

    [HttpPut("{mobId}")]
    public async Task<ActionResult> UpdateMobilization(UpdateMobilizationCommand updateMobilizationCommand, CancellationToken cancellationToken = default)
    {
        await _sender.Send(updateMobilizationCommand, cancellationToken);
        return NoContent();
    }

    [HttpPut("AddPartToMobilization/{mobId}")]
    public async Task<ActionResult> AddPartToMoblization(AddPartToMobilizationCommand addPartToMobilizationCommand, CancellationToken cancellationToken = default)
    {
        await _sender.Send(addPartToMobilizationCommand, cancellationToken);
        return NoContent();
    }

    [HttpPut("RemovePartFromMobilization/{mobId}")]
    public async Task<ActionResult> RemovePartFromMoblization(RemovePartFromMobilizationCommand removePartFromMobilizationCommand, CancellationToken cancellationToken)
    {
        await _sender.Send(removePartFromMobilizationCommand, cancellationToken);
        return NoContent();
    }
}
