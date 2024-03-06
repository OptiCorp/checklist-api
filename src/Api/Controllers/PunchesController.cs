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

    [HttpGet("GetSinglePunch/{punchId}")]

   public async Task<ActionResult<PunchDto>> GetSinglePunch(Guid punchId, CancellationToken cancellationToken)
   {
       var punch = await _sender.Send(new GetPunchQuery { PunchId = punchId }, cancellationToken);
       return Ok(punch);
   }
}
