using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobDeMob.Application.Checklists;
using MobDeMob.Application.Items.Queries;
using MobDeMob.Application.Parts.Queries;
using MobDeMob.Domain.Entities;
using MobDeMob.Domain.ItemAggregate;
using MobDeMob.Infrastructure;



namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChecklistController : ControllerBase
{

    private readonly ILogger<ChecklistController> _logger;


    private readonly ISender _sender;

    public ChecklistController(ILogger<ChecklistController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpPost()]
    [Route("{partId}/CreatePartChecklistQuestions")]
    public async Task<ActionResult> CreatePartChecklistQuestions(string partId, [FromBody] IEnumerable<string> questions, CancellationToken cancellationToken)
    {
        await _sender.Send(new CreatePartChecklistQuestionsCommand { partId = partId, questions = questions }, cancellationToken);
        return NoContent();
    }

    [HttpGet()]
    [Route("{partId}/GetAllQuestions")]
    public async Task<ActionResult<IEnumerable<string>>> GetAllQuestions(string partId, CancellationToken cancellationToken)
    {
        var questions = await _sender.Send(new GetPartQuestionsQuery { Id = partId}, cancellationToken);
        return Ok(questions);
    }
}
