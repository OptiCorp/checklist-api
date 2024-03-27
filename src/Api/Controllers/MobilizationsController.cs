using Application.Mobilizations.Commands;
using Application.Mobilizations.Queries;
using Application.Mobilizations.Queries.GetAll;
using Application.Mobilizations.Queries.GetMobilizationById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobDeMob.Application.Mobilizations.Commands;
using Application.Common.Exceptions;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MobilizationsController : ControllerBase
{
    private readonly ILogger<MobilizationsController> _logger;
    private readonly ISender _sender;

    public MobilizationsController(ILogger<MobilizationsController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpPost]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateMobilization(AddMobilizationCommand addMobilizationCommand, CancellationToken cancellationToken = default)
    {
        try
        {
            var id = await _sender.Send(addMobilizationCommand, cancellationToken);
            return CreatedAtAction(nameof(GetMobilizationById), new { mobId = id }, id);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }

    }

    [HttpGet("{mobId}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMobilizationById([FromRoute] Guid mobId, CancellationToken cancellationToken = default)
    {
        try
        {
            var mob = await _sender.Send(new GetMobilizationByIdQuery { Id = mobId }, cancellationToken);
            return mob is not null ? Ok(mob) : NotFound();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }

    }

    [HttpGet("GetAll")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllMobilizations([FromQuery] GetAllMobilizationsQuery query, CancellationToken cancellationToken = default)
    {
        var mobs = await _sender.Send(query, cancellationToken);
        return Ok(mobs);
    }

    [HttpPut("{mobId}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateMobilization([FromBody] UpdateMobilizationCommand updateMobilizationCommand, CancellationToken cancellationToken = default)
    {
        try
        {
            await _sender.Send(updateMobilizationCommand, cancellationToken);
            return NoContent();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }

    }

    //         [HttpPut("{mobId}")]
    //     public async Task<IActionResult> UpdateMobilization([FromBody] UpdateMobilizationCommand updateMobilizationCommand, CancellationToken cancellationToken = default)
    //     {
    //         try 
    //         {

    //         }catch(ChecklistValidation e) {
    // return BadRequest(e.Message
    //         }
    //         catch(ValidationException e) 
    //         {
    //          return barreques()  
    //         }
    //         await _sender.Send(updateMobilizationCommand, cancellationToken);
    //         return NoContent();
    //     }



    [HttpDelete("{mobId}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMobilization(DeleteMobilizationCommand deleteMobilizationCommand, CancellationToken cancellationToken = default)
    {
        try
        {
            await _sender.Send(deleteMobilizationCommand, cancellationToken);
            return NoContent();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }

    [HttpGet("GetBySearch")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchMobilizations([FromQuery] GetMobilizationBySearchQuery request, CancellationToken cancellationToken = default)
    {
        try
        {
            var mobs = await _sender.Send(request, cancellationToken);
            return Ok(mobs);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
    }

}
