using Application.Mobilizations.Queries;
using Application.Templates.AddTemplate;
using Application.Templates.GetById;
using Application.Templates.Queries;
using Application.Templates.UpdateTemplate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemplatesController : ControllerBase
    {
        private readonly ILogger<TemplatesController> _logger;
        private readonly ISender _sender;

        public TemplatesController(ILogger<TemplatesController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpGet("{itemId}")]
        public async Task<IActionResult> GetTemplateById([FromRoute] string itemId, CancellationToken cancellationToken = default)
        {
            var id = await _sender.Send(new GetTemplateQuery { ItemId = itemId }, cancellationToken);
            return Ok(id);
        }

        [HttpPost("{itemId}")]
        public async Task<IActionResult> CreateTemplateForItem(string itemId, AddTemplateCommand addTemplateCommand, CancellationToken cancellationToken = default)
        {
            var id = await _sender.Send(addTemplateCommand, cancellationToken);
            return CreatedAtAction(nameof(GetTemplateById), new { templateId = id }, id);
        }

        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateTemplateForItem(string itemId, UpdateTemplateCommand updateTemplateCommand, CancellationToken cancellationToken = default)
        {
            await _sender.Send(updateTemplateCommand, cancellationToken);
            return NoContent();
        }

        [HttpGet("GetChecklistsForItem/{itemId}")]
        public async Task<IActionResult> GetChecklistsForItem([FromRoute] string itemId, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var checklists = await _sender.Send(new GetChecklistsForItemQuery { ItemId = itemId, PageNumber = pageNumber, PageSize = pageSize }, cancellationToken);
            return Ok(checklists);
        }

        [HttpGet("GetItemTemplatesExists")]
        public async Task<IActionResult> GetItemTemplatesExists([FromQuery] IEnumerable<string> itemIds, CancellationToken cancellationToken = default)
        {
            var itemTemplatesExist = await _sender.Send(new GetTemplatesExistsFromItemIdsQuery { ItemIds = itemIds }, cancellationToken);
            return Ok(itemTemplatesExist);
        }

        [HttpGet("GetCheckItemQuestionConflict/{itemTemplateId}")]
        public async Task<IActionResult> GetCheckItemQuestionConflict(Guid itemTemplateId, CancellationToken cancellationToken = default)
        {
            var checklistIdConflicts = await _sender.Send(new CheckConflictsOnUpdateItemTemplateQuestionsQuery{ItemTemplateId = itemTemplateId}, cancellationToken);
            return Ok(checklistIdConflicts);
        }
    }
}
