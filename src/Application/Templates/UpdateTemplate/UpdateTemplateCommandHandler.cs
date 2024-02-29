using Application.Common.Exceptions;
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
        var template = await _templateRepository.GetTemplateByItemId(request.ItemId, cancellationToken) 
            ?? throw new NotFoundException(nameof(ItemTemplate), request.ItemId);

        UpdateTemplate(template, request);

        await _templateRepository.SaveChanges(cancellationToken);
    }

    private static ItemTemplate UpdateTemplate(ItemTemplate template, UpdateTemplateCommand request)
    {
        template.UpdateQuestions(request.Questions);

        return template;
    }
}
