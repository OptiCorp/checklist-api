using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobDeMob.Application.Mobilizations;
using MobDeMob.Application.Mobilizations.Commands;
using MobDeMob.Application.Mobilizations.Queries;
using MobDeMob.Domain.Entities.Mobilization;

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

    [HttpGet("{itemId}", Name = "GetSingle")]
    public async Task<ActionResult<MobilizationDto?>> GetMobilizationById(string itemId, CancellationToken cancellationToken)
    {
        var mob = await _sender.Send(new GetMobilizationByIdQuery { id = itemId }, cancellationToken);
        return mob is not null ? Ok(mob) : NotFound();
    }

    [HttpGet(Name = "GetAll")]
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
        var id = await _sender.Send(new AddMobilizationCommand { Title = mobilizationDto.Title, Description = mobilizationDto.Description }, cancellationToken);
        return CreatedAtAction(nameof(GetMobilizationById), new { itemId = id }, mobilizationDto);
    }
}