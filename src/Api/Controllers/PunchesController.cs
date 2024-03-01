using Api.Utilities;
using Application.Punches.Dtos;
using Application.Punches.Queries.GetById;
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

//   [HttpPost("CreatePunch")]

//    public async Task<ActionResult<string>> CreatePunch(CreatePunchCommand createPunchCommand, CancellationToken cancellationToken = default)
//    {
//        var punchId = await _sender.Send(createPunchCommand, cancellationToken);
//        return Ok(punchId);
//    }

//    [HttpPost("UploadPunchFile")]

//    public async Task<ActionResult<string>> UploadPunchFile([FromForm] FileModel fileModel, CancellationToken cancellationToken = default)
//    {
//        var absUri = await _sender.Send(new UploadFileCommand { PunchId = fileModel.id, ContentType = fileModel.file.ContentType, FileName = fileModel.file.FileName, Stream = fileModel.file.OpenReadStream() }, cancellationToken);
//        return Ok(absUri);
//    }

    [HttpGet("GetSinglePunch/{punchId}")]

   public async Task<ActionResult<PunchDto>> GetSinglePunch(Guid punchId, CancellationToken cancellationToken)
   {
       var punch = await _sender.Send(new GetPunchQuery { PunchId = punchId }, cancellationToken);
       return Ok(punch);
   }
}
