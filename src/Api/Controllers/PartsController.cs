using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobDeMob.Application.Parts.Queries;
using MobDeMob.Domain.ItemAggregate;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartsController : ControllerBase
{
    private readonly ILogger<ChecklistController> _logger;


    private readonly ISender _sender;

    public PartsController(ILogger<ChecklistController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet()]
    [Route("{partId}/GetItemById")]

    public async Task<ActionResult<Part>> GetItemById(string partId, CancellationToken cancellationToken)
    {
        var part = await _sender.Send(new GetPartByIdQuery { Id = partId }, cancellationToken);
        return part is not null ? Ok(part) : NotFound();
    }

    [HttpGet()]
    [Route("GetAll")]
    public async Task<ActionResult<Part>> GetAll(CancellationToken cancellationToken)
    {
        var parts = await _sender.Send(new GetAllPartsQuery { }, cancellationToken);
        return Ok(parts);
    }
}
