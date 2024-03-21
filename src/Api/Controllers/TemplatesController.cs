using Application.Mobilizations.Queries;
using Application.Templates;
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

        [HttpPost("{itemId}/CreateTemplateForItem")]
        public async Task<IActionResult> CreateTemplateForItem(AddTemplateCommand command, CancellationToken cancellationToken = default)
        {
            var id = await _sender.Send(command, cancellationToken);
            // return CreatedAtAction(nameof(GetTemplateById), new { templateId = id }, id);
            return Ok(id);
        }

        // [HttpPost("AddQuestionForTemplate/{itemTemplateId}")]
        // public async Task<IActionResult> AddQuestionForTemplate(Guid itemTemplateId, [FromBody] string question, CancellationToken cancellationToken = default)
        // {
        //     var id = await _sender.Send(new AddItemTemplateQuestionCommand { ItemTemplateId = itemTemplateId, Question = question }, cancellationToken);
        //     return Ok(id);
        // }

        [HttpPut("{itemId}/{questionTemplateId}")]
        public async Task<IActionResult> UpdateTemplateForItem(string itemId, Guid questionTemplateId, [FromBody] string question, CancellationToken cancellationToken = default)
        {
            await _sender.Send(new UpdateTemplateCommand { QuestionTemplateId = questionTemplateId, Question = question }, cancellationToken);
            return NoContent();
        }

        [HttpGet("{itemId}/GetChecklistsForItem")]
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

        [HttpGet("{itemId}/GetCheckItemQuestionConflict/{itemTemplateId}")]
        public async Task<IActionResult> GetCheckItemQuestionConflict(string itemId, Guid itemTemplateId, CancellationToken cancellationToken = default)
        {
            var checklistIdConflicts = await _sender.Send(new CheckConflictsOnUpdateItemTemplateQuestionsQuery { ItemTemplateId = itemTemplateId }, cancellationToken);
            return Ok(checklistIdConflicts);
        }

        [HttpDelete("{itemId}/DeleteQuestionTemplate/{questionTemplateId}")]
        public async Task<IActionResult> DeleteQuestionTemplate(string itemId, Guid questionTemplateId, CancellationToken cancellationToken = default)
        {
            await _sender.Send(new DeleteItemTemplateQuestionCommand { TemplateQuestionId = questionTemplateId }, cancellationToken);
            return NoContent();
        }
    }
}
