using Api.Utilities;
using Application.Common.Exceptions;
using Application.Punches.Dtos;
using Application.Punches.Queries.GetById;
using Application.Upload;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("GetSinglePunch/{punchId}")]

    public async Task<IActionResult> GetSinglePunch(Guid punchId, CancellationToken cancellationToken)
    {
        var punch = await _sender.Send(new GetPunchQuery { PunchId = punchId }, cancellationToken);
        return Ok(punch);
    }

    [HttpPost("UploadFiles/{punchId}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadPunchFiles(Guid punchId, [FromForm] FileModel fileModel, CancellationToken cancellationToken)
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
        try
        {
            await _sender.Send(new PunchUploadFilesCommand { Id = punchId, Files = files }, cancellationToken);

            return NoContent();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }
}
