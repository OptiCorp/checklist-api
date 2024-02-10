using Application.Common.Interfaces;
using Domain.Entities.TemplateAggregate;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.UpdateTemplate;


public class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand>
{
    private readonly ITemplateRepository _templateRepository;

    public UpdateTemplateCommandHandler(ITemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _templateRepository.GetTemplateById(request.ItemId) ?? throw new Exception("Not found");

        ChangeTemplate(template, request);

        await _templateRepository.SaveChanges(cancellationToken);
    }

    private ItemTemplate ChangeTemplate(ItemTemplate template, UpdateTemplateCommand request)
    {
        template.Name = request.ItemName;
        template.Description = request.ItemDescription;
        template.Type = request.Type;
        template.Revision = request.Revision;
        template.Questions = request?.Questions
            ?.Select(q => new QuestionTemplate { Question = q })
            ?.ToList() ?? [];

        return template;
    }
}
