using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<Mobilization>> GetMobilizationById(string itemId, CancellationToken cancellationToken)
    {
        var part = await _sender.Send(new GetMobilizationByIdQuery { id = itemId }, cancellationToken);
        return part is not null ? Ok(part) : NotFound();
    }

    [HttpGet(Name = "GetAll")]
    public async Task<ActionResult<Mobilization>> GetAllMobilizations(CancellationToken cancellationToken)
    {
        var part = await _sender.Send(new GetAllMobilizationsQuery { }, cancellationToken);
        return part is not null ? Ok(part) : NotFound();
    }

    [HttpPut("{mobId}", Name = "UpdateMobilization")]
    public async Task<ActionResult<Mobilization>> UpdateMibilization(string mobId, string? title, string? description, CancellationToken cancellationToken)
    {
        await _sender.Send(new UpdateMobilizationCommand { id = mobId, Title = title, Description = description }, cancellationToken);
        return NoContent();
    }
}
