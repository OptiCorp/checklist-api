using Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobDeMob.Application.Punches.Commands;
using MobDeMob.Application.Punches.Queries;
using MobDeMob.Domain.Entities.ChecklistAggregate;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PunchesController : ControllerBase
{
    private readonly ILogger<PunchesController> _logger;
    private readonly ISender _sender;

    public PunchesController(ILogger<PunchesController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpPost("CreatePunch")]

    public async Task<ActionResult<string>> CreatePunch(CreatePunchCommand createPunchCommand, CancellationToken cancellationToken = default)
    {
        var punchId = await _sender.Send(createPunchCommand, cancellationToken);
        return Ok(punchId);
    }

    [HttpPost("UploadPunchFile")]

    public async Task<ActionResult<string>> UploadPunchFile([FromForm] FileModel fileModel, CancellationToken cancellationToken = default)
    {
        var absUri = await _sender.Send(new UploadFileCommand { PunchId = fileModel.id, ContentType = fileModel.file.ContentType, FileName = fileModel.file.FileName, Stream = fileModel.file.OpenReadStream() }, cancellationToken);
        return Ok(absUri);
    }

    [HttpGet("GetSinglePunch{punchId}")]

    public async Task<ActionResult<PunchDto>> GetSinglePunch(string punchId, CancellationToken cancellationToken)
    {
        var punch = await _sender.Send(new GetPunchQuery { punchId = punchId }, cancellationToken);
        return Ok(punch);
    }
}
