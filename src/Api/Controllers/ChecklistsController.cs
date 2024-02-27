using Api.Utilities;
using Application.Checklists.Commands;
using Application.Checklists.Commands.AddItem;
using Application.Checklists.Commands.AddPunch;
using Application.Checklists.Dtos;
using Application.Checklists.Queries;
using Application.Punches.Commands;
using Application.Punches.Dtos;
using Application.Punches.Queries.GetById;
using Application.Upload;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobDeMob.Application.Mobilizations.Commands;

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

    [HttpGet("GetPunches/{checklistItemId}")]

    public async Task<ActionResult<IEnumerable<PunchDto>>> GetPunches(Guid checklistItemId, CancellationToken cancellationToken)
    {
        var punches = await _sender.Send(new GetPunchesQuery { ChecklistItemId = checklistItemId}, cancellationToken);
        return Ok(punches);
    }

    // [HttpGet("GetPunch/{punchId}")]
    // public async Task<ActionResult<PunchDto>> GetPunch(Guid punchId, CancellationToken cancellationToken)
    // {
    //     var punch = await _sender.Send(new GetPunchQuery { punchId = punchId }, cancellationToken);
    //     return Ok(punch);
    // }

    [HttpPost("AddItem/{itemId}")]
    public async Task<IActionResult> AddNewItem(Guid mobilizationId, [FromBody] AddItemCommand addItemCommand, CancellationToken cancellationToken = default)
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
        foreach (var file in fileModel.files)
        {
            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            files.Add(new PunchUploadFile()
            {
                ContentType = file.ContentType,
                FileName = file.FileName,
                Stream = stream
            });
        }
        // await _sender.Send(new PunchUploadFilesCommand { Id = punchId, ContentType = fileModel.file.ContentType, FileName = fileModel.file.FileName, Stream = fileModel.file.OpenReadStream() }, cancellationToken);
        await _sender.Send(new PunchUploadFilesCommand { Id = punchId, Files = files }, cancellationToken);

        return NoContent();
    }

    [HttpPatch("ChecklistQuestionCheckedUpdate/{checklistItemQuestionId}")]
    public async Task<IActionResult> ChecklistQuestionCheckedUpdate(SetChecklistItemQuestionPatchCheckedCommandRequest c, CancellationToken cancellationToken)
    {
        await _sender.Send(new SetChecklistItemQuestionPatchCheckedCommand{Id = c.Id, Patches = c.Patches, ModelState = ModelState}, cancellationToken);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return NoContent();
    }

    [HttpPatch("ChecklistQuestionNotApplicableUpdate/{checklistItemQuestionId}")]
    public async Task<IActionResult> ChecklistQuestionNotApplicableUpdate(SetChecklistItemQuestionPatchNotApplicableCommandRequest c, CancellationToken cancellationToken)
    {
        await _sender.Send(new SetChecklistItemQuestionPatchNotApplicableCommand{Id = c.Id, Patches = c.Patches, ModelState = ModelState}, cancellationToken);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return NoContent();
    }

    [HttpPatch("ChecklistItemUpdate/{checklistItemId}")]
    public async Task<IActionResult> ChecklistItemUpdate(SetChecklistItemPatchCommand setChecklistItemPatchCommand, CancellationToken cancellationToken)
    {
        await _sender.Send(setChecklistItemPatchCommand, cancellationToken);
        return NoContent();
    }

    [HttpGet("GetChecklists")]
    public async Task<IActionResult> GetChecklists(Guid mobilizationId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var checklistItems = await _sender.Send(new GetChecklistItemsQuery{MobilizationId = mobilizationId, PageNumber = pageNumber, PageSize = pageSize}, cancellationToken);
        return Ok(checklistItems);
    }

    [HttpGet("GetChecklistItem/{checklistItemId}")]
    public async Task<IActionResult> GetChecklistItem(Guid checklistItemId, CancellationToken cancellationToken)
    {
        var checklistItem = await _sender.Send(new GetChecklistItemQuery { ChecklistItemId = checklistItemId }, cancellationToken);
        return Ok(checklistItem);
    }

    [HttpDelete("DeletePartFromMobilization/{partId}")]
    public async Task<IActionResult> DeletePartFromMobilization(RemovePartFromMobilizationCommand removePartFromMobilizationCommand, CancellationToken cancellationToken)
    {
        await _sender.Send(removePartFromMobilizationCommand, cancellationToken);
        return NoContent();
    }

    [HttpPut("UpdatePunch/{punchId}")]
    public async Task<IActionResult> UpdatePunch(UpdatePunchCommand updatePunchCommand, CancellationToken cancellationToken = default)
    {
        await _sender.Send(updatePunchCommand, cancellationToken);
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
