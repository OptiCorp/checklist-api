using Application.Templates.AddTemplate;
using Application.Templates.GetById;
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

        [HttpGet("{templateId}")]
        public async Task<IActionResult> GetTemplateById([FromRoute] string templateId, CancellationToken cancellationToken = default)
        {
            var id = await _sender.Send(new GetByIdCommand { TemplateId = templateId }, cancellationToken);
            return Ok(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTemplateForItem(AddTemplateCommand addTemplateCommand, CancellationToken cancellationToken = default)
        {
            var id = await _sender.Send(addTemplateCommand, cancellationToken);
            return Ok(id);// TODO change to createdataction
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTemplateForItem(AddTemplateCommand addTemplateCommand, CancellationToken cancellationToken = default)
        {
            await _sender.Send(addTemplateCommand, cancellationToken);
            return NoContent();
        }
    }
}
