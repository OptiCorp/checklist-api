using Checklist.Application.Items.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.Entities.ItemAggregate;
using MobDeMob.Infrastructure;



namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ChecklistController : ControllerBase
{

    private readonly ILogger<ChecklistController> _logger;

    private readonly IMediator _mediator;

    public ChecklistController(ILogger<ChecklistController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet("{itemId: string}")]
    public async Task<ActionResult<IEnumerable<Item>>> GetItemById(string itemId ,CancellationToken cancellationToken)
    {
        var part = await _mediator.Send(new GetItemByIdQuery {Id = itemId}, cancellationToken);
        return part is not null ? Ok(part) : NotFound();
    }
}
