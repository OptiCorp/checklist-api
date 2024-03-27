using Api.Utilities;
using Application.Checklists.Commands;
using Application.Checklists.Commands.AddItem;
using Application.Checklists.Commands.AddPunch;
using Application.Checklists.Dtos;
using Application.Checklists.Queries;
using Application.Common.Exceptions;
using Application.Mobilizations.Queries;
using Application.Punches.Commands;
using Application.Punches.Dtos;
using Application.Punches.Queries.GetById;
using Application.Upload;
using Azure;
using Domain.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MobDeMob.Application.Checklists.Commands;
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
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPunches(Guid checklistId, CancellationToken cancellationToken)
    {
        try
        {
            var punches = await _sender.Send(new GetPunchesQuery { ChecklistId = checklistId }, cancellationToken);
            return Ok(punches);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }

    }

    [HttpPost("AddItem/{itemId}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> AddNewItem(Guid mobilizationId, [FromBody] AddItemCommand addItemCommand, CancellationToken cancellationToken = default)
    {
        try
        {
            var checklistId = await _sender.Send(addItemCommand, cancellationToken);
            return Ok(checklistId);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }

    }

    [HttpPost("AddPunch/{checklistId}/{itemId}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddPunch(Guid mobilizationId, AddPunchCommand addPunchCommand, CancellationToken cancellationToken = default)
    {
        try
        {
            var id = await _sender.Send(addPunchCommand, cancellationToken);
            return Ok(id);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }

    }

    [HttpPost("ChecklistQuestionCheckedUpdate/{checklistId}/{checklistQuestionId}/{value}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChecklistQuestionCheckedUpdate(SetChecklistQuestionCheckedCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }
        catch (ChecklistValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("ChecklistQuestionNotApplicableUpdate/{checklistId}/{checklistQuestionId}/{value}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChecklistQuestionNotApplicableUpdate(SetChecklistQuestionNotApplicableCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }
        catch (ChecklistValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("ChecklistStatus/{checklistId}/{status}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetChecklistStatus(Guid checklistId, ChecklistStatus status, CancellationToken cancellationToken)
    {
        try
        {
            await _sender.Send(new SetChecklistStatusCommand { ChecklistId = checklistId, Status = status }, cancellationToken);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e);
        }
    }

    [HttpGet("GetChecklists")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetChecklists(Guid mobilizationId, CancellationToken cancellationToken, int pageNumber = 1, int pageSize = 10)
    {
        try
        {
            var checklistItems = await _sender.Send(new GetChecklistsQuery { MobilizationId = mobilizationId, PageNumber = pageNumber, PageSize = pageSize }, cancellationToken);
            return Ok(checklistItems);
        }
        catch (NotFoundException e)
        {
            return NotFound(e);
        }
    }

    [HttpGet("GetChecklist/{checklistId}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetChecklist(Guid checklistId, CancellationToken cancellationToken)
    {
        var checklist = await _sender.Send(new GetChecklistQuery { ChecklistId = checklistId }, cancellationToken);
        return checklist is not null ? Ok(checklist) : NotFound();
    }

    [HttpDelete("DeleteChecklist/{checklistId}/{itemId}")]
    public async Task<IActionResult> DeleteChecklist(DeleteChecklistCommand command, CancellationToken cancellationToken)
    {
        await _sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPut("UpdatePunch/{punchId}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePunch(UpdatePunchCommand updatePunchCommand, CancellationToken cancellationToken = default)
    {
        try
        {
            await _sender.Send(updatePunchCommand, cancellationToken);
            return NoContent();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }

    }
}
