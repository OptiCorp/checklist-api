using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobDeMob.Application.Parts.Queries;
using MobDeMob.Domain.ItemAggregate;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ILogger<ChecklistController> _logger;


    private readonly ISender _sender;

    public ItemsController(ILogger<ChecklistController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    
    [HttpGet("{itemId}")]
    public async Task<ActionResult<IEnumerable<Part>>> GetItemById(string itemId, CancellationToken cancellationToken)
    {
        var part = await _sender.Send(new GetItemByIdQuery { Id = itemId }, cancellationToken);
        return part is not null ? Ok(part) : NotFound();
    }
}
