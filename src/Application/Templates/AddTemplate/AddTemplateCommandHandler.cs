using Application.Common.Interfaces;
using Domain.Entities.TemplateAggregate;
using MediatR;
using MobDeMob.Domain.ItemAggregate;

namespace Application.Templates.AddTemplate;

public class AddTemplateCommandHandler : IRequestHandler<AddTemplateCommand, Guid>
{
    private readonly ITemplateRepository _templateRepository;

    public AddTemplateCommandHandler(ITemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task<Guid> Handle(AddTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = MapToItemTemplate(request);

        await _templateRepository.AddTemplate(template);

        template.Questions = request?.Questions
            ?.Select(q => new QuestionTemplate { Question = q })
            ?.ToList() ?? [];

        await _templateRepository.SaveChanges(cancellationToken);

        return template.Id;
    }

    private ItemTemplate MapToItemTemplate(AddTemplateCommand request)
    {
        return new ItemTemplate
        {
            ItemId = request.ItemId,
            Name = request.ItemName,
            Description = request.ItemDescription,
            Type = request.Type,
            Revision = request.Revision,
        };
    }
}
