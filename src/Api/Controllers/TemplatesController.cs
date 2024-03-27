using Application.Common.Exceptions;
using Application.Mobilizations.Queries;
using Application.Templates;
using Application.Templates.AddTemplate;
using Application.Templates.Commands;
using Application.Templates.GetById;
using Application.Templates.Queries;
using Application.Templates.UpdateTemplate;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/Templates/{itemTemplateId}")]
    public class TemplatesController : ControllerBase
    {
        private readonly ILogger<TemplatesController> _logger;
        private readonly ISender _sender;

        public TemplatesController(ILogger<TemplatesController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpGet("getItemTemplateById")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetItemTemplateById([FromRoute] string itemTemplateId, CancellationToken cancellationToken = default)
        {
            try
            {
                var itemTemplate = await _sender.Send(new GetItemTemplateQuery { ItemTemplateId = itemTemplateId }, cancellationToken);
                return itemTemplate is not null ? Ok(itemTemplate) : NotFound(itemTemplate);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Errors);
            }
        }

        [HttpGet("GetChecklistTemplate/{checklistTemplateId}", Name = "GetChecklistTemplate")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetChecklistTemplate([FromRoute] string itemTemplateId, Guid checklistTemplateId, CancellationToken cancellationToken = default)
        {
            var checklistTemplate = await _sender.Send(new GetChecklistTemplateQuery { checklistTemplateId = checklistTemplateId }, cancellationToken);
            return checklistTemplate is not null ? Ok(checklistTemplate) : NotFound(checklistTemplate);
        }

        [HttpPost("CreateChecklistTemplateForItemTemplate")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> CreateChecklistTemplateForItemTemplate(AddChecklistTemplateCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var itemTemplate = await _sender.Send(command, cancellationToken);
                // return CreatedAtAction(nameof(GetTemplateById), new { templateId = id }, id);
                return CreatedAtRoute(nameof(GetChecklistTemplate), new { itemTemplateId = itemTemplate.Id, checklistTemplateId = itemTemplate.ChecklistTemplate.Id }, itemTemplate);
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

        [HttpPost("AddQuestionForChecklistTemplate/{checklistTemplateId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddQuestionForTemplate(string itemTemplateId, Guid checklistTemplateId, [FromBody] string question, CancellationToken cancellationToken = default)
        {
            try
            {
                var id = await _sender.Send(new AddChecklistTemplateQuestionCommand { checklistTemplateId = checklistTemplateId, question = question }, cancellationToken);
                return Ok(id);
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

        [HttpPut("{checklistTemplateId}/{questionTemplateId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateChecklistTemplateForItemTemplate(string itemTemplateId, Guid checklistTemplateId, Guid questionTemplateId, [FromBody] string question, CancellationToken cancellationToken = default)
        {
            try
            {
                await _sender.Send(new UpdateTemplateCommand { questionTemplateId = questionTemplateId, question = question }, cancellationToken);
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


        [HttpGet("GetCheckItemQuestionConflict/{checklistTemplateId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCheckItemQuestionConflict([FromRoute] CheckConflictsOnUpdateChecklistTemplateQuestionsQuery query, CancellationToken cancellationToken = default)
        {
            var checklistIdConflicts = await _sender.Send(query, cancellationToken);
            return Ok(checklistIdConflicts);
        }

        [HttpDelete("DeleteQuestionTemplate/{checklistTemplateId}/{questionTemplateId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteQuestionTemplate(Guid checklistTemplateId, Guid questionTemplateId, CancellationToken cancellationToken = default)
        {
            try
            {
                await _sender.Send(new DeleteQuestionTemplateCommand { QuestionTemplateId = questionTemplateId }, cancellationToken);
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }

        }
    }
}
