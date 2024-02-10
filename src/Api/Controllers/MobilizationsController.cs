using Application.Mobilizations.Queries.GetAll;
using Application.Mobilizations.Queries.GetMobilizationById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobDeMob.Application.Mobilizations.Commands;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MobilizationsController : ControllerBase
{
    private readonly ILogger<MobilizationsController> _logger;
    private readonly ISender _sender;

    public MobilizationsController(ILogger<MobilizationsController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMobilization(AddMobilizationCommand addMobilizationCommand, CancellationToken cancellationToken = default)
    {
        var id = await _sender.Send(addMobilizationCommand, cancellationToken);
        return Ok(id);// TODO change to createdataction
    }

    [HttpGet("{mobId}")]
    public async Task<IActionResult> GetMobilizationById([FromRoute] string mobId, CancellationToken cancellationToken = default)
    {
        var mob = await _sender.Send(new GetMobilizationByIdQuery { id = mobId }, cancellationToken);
        return mob is not null ? Ok(mob) : NotFound();
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllMobilizations(CancellationToken cancellationToken = default)
    {
        var mobs = await _sender.Send(new GetAllMobilizationsQuery(), cancellationToken);
        return mobs is not null ? Ok(mobs) : NotFound();
    }

    [HttpPut("{mobId}")]
    public async Task<IActionResult> UpdateMobilization([FromBody] UpdateMobilizationCommand updateMobilizationCommand, CancellationToken cancellationToken = default)
    {
        await _sender.Send(updateMobilizationCommand, cancellationToken);
        return NoContent();
    }
}
