using Api.Utilities;
using Application.Checklists.Queries;
using Application.Common.Exceptions;
using Application.Mobilizations.Queries;
using Application.Punches.Dtos;
using Application.Punches.Queries.GetById;
using Application.Templates.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ILogger<ItemsController> _logger;
    private readonly ISender _sender;

    public ItemsController(ILogger<ItemsController> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [HttpGet("GetItemTemplatesExists")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetChecklistTemplatesExists([FromQuery] IEnumerable<string> itemIds, CancellationToken cancellationToken = default)
    {
        try
        {
            var itemsHasChecklistTemplate = await _sender.Send(new GetTemplatesExistsFromItemIdsQuery { ItemIds = itemIds }, cancellationToken);
            return Ok(itemsHasChecklistTemplate);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{itemId}/GetChecklistsForItem")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetChecklistsForItem(string itemId, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        try
        {
            var checklists = await _sender.Send(new GetChecklistsForItemQuery { ItemId = itemId, PageNumber = pageNumber, PageSize = pageSize }, cancellationToken);
            return Ok(checklists);
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

    [HttpGet("{itemId}/SearchChecklists")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchChecklists(string itemId, string checklistSearchId, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        try
        {
            var checklists = await _sender.Send(new GetChecklistsForItemBySearchQuery { itemId = itemId, pageNumber = pageNumber, pageSize = pageSize, checklistSearchId = checklistSearchId }, cancellationToken);
            return Ok(checklists);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Errors);
        }

    }
}
