using Application.Checklists.Commands.AddItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/mobilizations/{mobilizationId}")]
public class ChecklistsController : ControllerBase
{
    private readonly ILogger<ChecklistsController> _logger;
    private readonly ISender _sender;

    public ChecklistsController(ILogger<ChecklistsController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpPost("AddItem/{itemId}")]
    public async Task<IActionResult> AddNewItem([FromBody] AddItemCommand addItemCommand, CancellationToken cancellationToken = default)
    {
        await _sender.Send(addItemCommand, cancellationToken);
        return Ok(addItemCommand);
    }

    //[HttpPost("{partId}/CreatePartChecklistQuestions")]
    //public async Task<ActionResult> CreatePartChecklistQuestions(string partId, [FromBody] List<string> questions, CancellationToken cancellationToken)
    //{
    //    await _sender.Send(new CreatePartChecklistQuestionsCommand { partId = partId, questions = questions }, cancellationToken);
    //    return NoContent();
    //}

    //[HttpGet("{partId}/GetAllQuestions")]
    //public async Task<ActionResult<IEnumerable<string>>> GetAllQuestions(string partId, CancellationToken cancellationToken)
    //{
    //    var questions = await _sender.Send(new GetPartQuestionsQuery { Id = partId }, cancellationToken);
    //    return Ok(questions);
    //}
}
