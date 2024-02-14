using Api.Utilities;
using Application.Checklists.Commands.AddItem;
using Application.Checklists.Commands.AddPunch;
using Application.Checklists.Queries;
using Application.Punches.Dtos;
using Application.Punches.Queries.GetById;
using Application.Upload;
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

    [HttpGet("GetPunches")]

    public async Task<ActionResult<IEnumerable<PunchDto>>> GetPunches(Guid mobilizationId, CancellationToken cancellationToken)
    {
        var punches = await _sender.Send(new GetPunchesQuery { MobilizationId = mobilizationId }, cancellationToken);
        return Ok(punches);
    }

    [HttpGet("GetPunch/{punchId}")]
    public async Task<ActionResult<PunchDto>> GetPunch(Guid punchId, CancellationToken cancellationToken)
    {
        var punch = await _sender.Send(new GetPunchQuery { punchId = punchId }, cancellationToken);
        return Ok(punch);
    }

    [HttpPost("AddItem/{itemId}")]
    public async Task<IActionResult> AddNewItem([FromBody] AddItemCommand addItemCommand, CancellationToken cancellationToken = default)
    {
        await _sender.Send(addItemCommand, cancellationToken);
        return Ok(addItemCommand);
    }

    [HttpPost("AddPunch/{itemId}")]
    public async Task<IActionResult> AddPunch([FromBody] AddPunchCommand addPunchCommand, CancellationToken cancellationToken = default)
    {
        await _sender.Send(addPunchCommand, cancellationToken);
        return Ok(addPunchCommand);
    }

    [HttpPost("UploadFile/{punchId}")]
    public async Task<ActionResult<string>> UploadPunchFile(Guid punchId, [FromForm] FileModel fileModel, CancellationToken cancellationToken)
    {
        ICollection<PunchUploadFile> files = [];
        foreach(var file in fileModel.files)
        {
            files.Add(new PunchUploadFile()
            {
                ContentType = file.ContentType,
                FileName = file.FileName,
                Stream = file.OpenReadStream()
            });
        }
        // await _sender.Send(new PunchUploadFilesCommand { Id = punchId, ContentType = fileModel.file.ContentType, FileName = fileModel.file.FileName, Stream = fileModel.file.OpenReadStream() }, cancellationToken);
        await _sender.Send(new PunchUploadFilesCommand { Id = punchId, Files = files }, cancellationToken);

        return NoContent();
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
