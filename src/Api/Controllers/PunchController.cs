using Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobDeMob.Application.Mobilizations.Commands;
using MobDeMob.Application.Parts;
using MobDeMob.Application.Parts.Queries;
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
    [Route("UploadPunchFile")]

    public async Task<ActionResult<string>> GetItemById([FromForm] FileModel fileModel,CancellationToken cancellationToken)
    {
        var absUri = await _sender.Send(new UploadFileCommand {ContainerName = fileModel.ContainerName, ContentType = fileModel.file.ContentType, FileName = fileModel.file.FileName, Stream = fileModel.file.OpenReadStream()}, cancellationToken);
        return Ok(absUri);
    }
}
