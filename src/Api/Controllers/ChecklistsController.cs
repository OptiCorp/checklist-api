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
using MobDeMob.Domain.Enums;

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

    [HttpGet("GetPunches/{checklistId}")]

    public async Task<ActionResult<IEnumerable<PunchDto>>> GetPunches(Guid checklistId, CancellationToken cancellationToken)
    {
        var punches = await _sender.Send(new GetPunchesQuery { ChecklistId = checklistId}, cancellationToken);
        return Ok(punches);
    }

    [HttpPost("AddItem/{itemId}")]
    public async Task<IActionResult> AddNewItem(Guid mobilizationId, [FromBody] AddItemCommand addItemCommand, CancellationToken cancellationToken = default)
    {
        var checklistId = await _sender.Send(addItemCommand, cancellationToken);
        return Ok(checklistId);
    }

    [HttpPost("AddPunch/{itemId}")]
    public async Task<IActionResult> AddPunch([FromBody] AddPunchCommand addPunchCommand, CancellationToken cancellationToken = default)
    {
        var id = await _sender.Send(addPunchCommand, cancellationToken);
        return Ok(id);
    }

    [HttpPost("UploadFiles/{punchId}")]
    public async Task<ActionResult<string>> UploadPunchFiles(Guid punchId, [FromForm] FileModel fileModel, CancellationToken cancellationToken)
    {
        ICollection<PunchUploadFile> files = [];
        using var sharedStream = new MemoryStream();

        foreach (var file in fileModel.files)
        {
            
            sharedStream.Seek(0, SeekOrigin.Begin);
            await file.CopyToAsync(sharedStream);
            files.Add(new PunchUploadFile()
            {
                ContentType = file.ContentType,
                FileName = file.FileName,
                Stream = new MemoryStream(sharedStream.ToArray())
            });
        }
        // await _sender.Send(new PunchUploadFilesCommand { Id = punchId, ContentType = fileModel.file.ContentType, FileName = fileModel.file.FileName, Stream = fileModel.file.OpenReadStream() }, cancellationToken);
        await _sender.Send(new PunchUploadFilesCommand { Id = punchId, Files = files }, cancellationToken);

        return NoContent();
    }

    [HttpPost("ChecklistQuestionCheckedUpdate/{checklistQuestionId}/{value}")]
    public async Task<IActionResult> ChecklistQuestionCheckedUpdate(Guid checklistQuestionId, bool value, CancellationToken cancellationToken)
    {
        await _sender.Send(new SetChecklistQuestionCheckedCommand{ChecklistQuestionId = checklistQuestionId, Value = value}, cancellationToken);
        return NoContent();
    }

    [HttpPut("ChecklistStatus/{checklistId}/{status}")]
    public async Task<IActionResult> SetChecklistStatus(Guid checklistId, ChecklistStatus status, CancellationToken cancellationToken)
    {
        await _sender.Send(new SetChecklistStatusCommand{ChecklistId = checklistId, Status = status}, cancellationToken);
        return NoContent();
    }

    [HttpPost("ChecklistQuestionNotApplicableUpdate/{checklistQuestionId}/{value}")]
    public async Task<IActionResult> ChecklistQuestionNotApplicableUpdate(Guid checklistQuestionId, bool value, CancellationToken cancellationToken)
    {
        await _sender.Send(new SetChecklistQuestionNotApplicableCommand{ChecklistQuestionId = checklistQuestionId, Value = value}, cancellationToken);
        return NoContent();
    }

    [HttpGet("GetChecklists")]
    public async Task<IActionResult> GetChecklists(Guid mobilizationId, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10)
    {
        var checklistItems = await _sender.Send(new GetChecklistsQuery{MobilizationId = mobilizationId, PageNumber = pageNumber, PageSize = pageSize}, cancellationToken);
        return Ok(checklistItems);
    }

    [HttpGet("GetChecklist/{checklistId}")]
    public async Task<IActionResult> GetChecklistItem(Guid checklistId, CancellationToken cancellationToken)
    {
        var checklist = await _sender.Send(new GetChecklistQuery { ChecklistId = checklistId }, cancellationToken);
        return Ok(checklist);
    }

    [HttpDelete("DeleteItemFromMobilization/{itemId}")]
    public async Task<IActionResult> DeletePartFromMobilization(RemoveItemFromMobilizationCommand command, CancellationToken cancellationToken)
    {
        await _sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPut("UpdatePunch/{punchId}")]
    public async Task<IActionResult> UpdatePunch(UpdatePunchCommand updatePunchCommand, CancellationToken cancellationToken = default)
    {
        await _sender.Send(updatePunchCommand, cancellationToken);
        return NoContent();
    }
}
