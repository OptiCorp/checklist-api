using Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobDeMob.Application.Mobilizations.Commands;
using MobDeMob.Application.Parts;
using MobDeMob.Application.Parts.Queries;
using MobDeMob.Application.Punches.Commands;
using MobDeMob.Application.Punches.Queries;
using MobDeMob.Domain.Entities.ChecklistAggregate;
using MobDeMob.Domain.ItemAggregate;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PunchController : ControllerBase
{
    private readonly ILogger<PunchController> _logger;


    private readonly ISender _sender;

    public PunchController(ILogger<PunchController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpPost()]
    [Route("CreatePunch")]

    public async Task<ActionResult<string>> CreatePunch(PunchDto punchDto, CancellationToken cancellationToken)
    {
        var punchId = await _sender.Send(new CreatePunchCommand { ChecklistSectionId = punchDto.ChecklistId, Title = punchDto.Title, Description = punchDto.Description }, cancellationToken);
        return Ok(punchId);
    }

    [HttpPost()]
    [Route("UploadPunchFile")]

    public async Task<ActionResult<string>> UploadPunchFile([FromForm] FileModel fileModel, CancellationToken cancellationToken)
    {
        var absUri = await _sender.Send(new UploadFileCommand { PunchId = fileModel.id, ContentType = fileModel.file.ContentType, FileName = fileModel.file.FileName, Stream = fileModel.file.OpenReadStream() }, cancellationToken);
        return Ok(absUri);
    }

    [HttpGet()]
    [Route("GetSinglePunch{punchId}")]

    public async Task<ActionResult<PunchDto>> GetSinglePunch(string punchId, CancellationToken cancellationToken)
    {
        var punch = await _sender.Send(new GetPunchQuery { punchId = punchId }, cancellationToken);
        return Ok(punch);
    }
}
