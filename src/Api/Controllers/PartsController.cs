using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobDeMob.Application.Parts;
using MobDeMob.Application.Parts.Queries;
namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartsController : ControllerBase
{
    private readonly ILogger<ChecklistsController> _logger;
    private readonly ISender _sender;

    public PartsController(ILogger<ChecklistsController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet("{partId}/GetItemById")]

    public async Task<ActionResult<PartDto>> GetItemById(string partId, CancellationToken cancellationToken = default)
    {
        var part = await _sender.Send(new GetPartByIdQuery { Id = partId }, cancellationToken);
        return part is not null ? Ok(part) : NotFound();
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<PartDto>> GetAll(bool includeChildren = false, CancellationToken cancellationToken = default)
    {
        var parts = await _sender.Send(new GetAllPartsQuery { includeChildren = includeChildren }, cancellationToken);
        return Ok(parts);
    }
}
